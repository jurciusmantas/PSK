import React from "react"
import './RestrictionsPage.css';

import { post } from '../../helpers/request'
import { get } from '../../helpers/request'
import { del } from '../../helpers/request'
import Select from 'react-select';
import Tooltip from 'react-tooltip'
import { connect } from 'react-redux';
import { notification } from '../../helpers/notification';
import '../Topic/TopicPage.css';

class RestrictionsPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading1: true,
            loading2: true,
            consecutiveDays: null,
            maxDaysPerMonth: null,
            maxDaysPerQuarter: null,
            maxDaysPerYear: null,
            restriction: null,
            restrictions: null,
            applyTo: 2,
            selectedEmployees: [],
            displaySelectField: false,
        }
        this.handleSelectChange = this.handleSelectChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.deleteRestriction = this.deleteRestriction.bind(this);
    }

    componentDidMount() {
        get(`restrictions/active?employeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({
                        restriction: res.data,
                        loading1: false
                    })
                }
                else {
                    notification('Cannot get restriction :(', 'error');
                    console.warn(`Cannot get restriction:`);
                    console.warn(res.message);
                }
            })
            .catch(reason => {
                console.error(reason);
            })
        get(`restrictions?employeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({
                        restrictions: res.data,
                        loading2: false
                    })
                }
                else {
                    notification('Cannot get restrictions :(', 'error');
                    console.warn(`Cannot get restrictions:`);
                    console.warn(res.message);
                }
            })
            .catch(reason => {
                console.error(reason);
            })
        get(`employees/${this.props.currentUser.id}/subordinates`)
            .then(res => res.json())
            .then(res => { this.setState({ users: res.data }); })
            .catch(reason => {
                console.error(`GET employees/${this.props.currentUser.id}/subordinates failed`)
                console.error(reason)
            });
    }

    showRestriction() {
        var restriction = this.state.restriction;
        if (this.state.loading1) {
            return <div>Loading...</div>
        }
        else if (restriction == null) {
            return <div>You do not have any current restriction</div>
        }
        else {
            return (
                <table>
                    <tr>
                        <td><label>Consecutive Days</label></td>
                        <td>{restriction.consecutiveDays}</td>
                    </tr>
                    <tr>
                        <td><label>Max Days per Month</label></td>
                        <td>{restriction.maxDaysPerMonth}</td>
                    </tr>
                    <tr>
                        <td><label>Max Days per Quarter</label></td>
                        <td>{restriction.maxDaysPerQuarter}</td>
                    </tr>
                    <tr>
                        <td><label>Max Days per Year</label></td>
                        <td>{restriction.maxDaysPerYear}</td>

                    </tr>
                </table>
             );
        }
    }
    showRestrictionsList() {
        var restrictions = this.state.restrictions || [];
        if (this.state.loading2) {
            return <div>Loading...</div>
        }
        else if (restrictions.length === 0) {
            return <div>You do not have any restrictions created</div>
        }
        else {
            return (
                <table>
                    <thead>
                        <tr>
                            <th>Consecutive Days</th>
                            <th>Max Days per Month</th>
                            <th>Max Days per Quarter</th>
                            <th>Max Days per Year</th>
                            <th>Use Count</th>
                        </tr>
                    </thead>
                    <tbody>
                        {restrictions.map((restriction, index) => {
                            return (
                                <tr key={index}>
                                    <td>{restriction.consecutiveDays}</td>
                                    <td>{restriction.maxDaysPerMonth}</td>
                                    <td>{restriction.maxDaysPerQuarter}</td>
                                    <td>{restriction.maxDaysPerYear}</td>
                                    <td>
                                        <div data-tip data-for={"ucntt" + index}>{restriction.useCount}</div>
                                        <Tooltip id={"ucntt" + index} place="right" arrow>{this.showUseCountNames(restriction.useCountNames)}</Tooltip>
                                    </td>
                                    <td>
                                        <button
                                            value={restriction.id}
                                            onClick={(e) => {
                                                if (window.confirm('Are you sure?'))
                                                    this.deleteRestriction(e)
                                            }}
                                        >Delete</button>
                                    </td>
                                </tr>
                            )
                        })
                        }
                    </tbody>
                </table>
            );
        }
    }

    showUseCountNames(useCountNames) {
        if (useCountNames == null || useCountNames.length === 0) {
            return (<p>None</p>);
        }
        else {
            return useCountNames.map((name, _) => {
                return (<p>{name}</p>);
            })
        }
        
    }

    deleteRestriction(e) {
        del(`restrictions?id=${e.target.value}&employeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    notification('Deleted');
                    window.location.reload();
                }
                else {
                    notification(res.message, 'error');
                }
            })
            .catch(error => {
                console.error(error);
            })
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    handleSubmit(e) {
        e.preventDefault();
        post('restrictions', {
            ConsecutiveDays: parseInt(this.state.consecutiveDays),
            MaxDaysPerMonth: parseInt(this.state.maxDaysPerMonth),
            MaxDaysPerQuarter: parseInt(this.state.maxDaysPerQuarter),
            MaxDaysPerYear: parseInt(this.state.maxDaysPerYear),
            CreatorId: this.props.currentUser.id,
            ApplyTo: parseInt(this.state.applyTo),
            UserIds: this.state.selectedEmployees.map((selectedEmployee) => {
                return selectedEmployee.value
            })
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    notification(res.message);
                    window.location.reload();
                }
                else {
                    notification(res.message, 'error');
                }
            })
            .catch(error => {
                console.log(error);
            })
        

    }

    showCreationForm() {
        let selectField = null;
        if (this.state.displaySelectField) {
            let options = this.state.users.map(function (user) {
                return { value: user.id, label: user.name };
            });
            selectField = (
                <tr>
                    <td>
                        <label>Select employees for restriction:</label>
                    </td>
                    <td>
                        <div id="selectField" className="">
                            <Select
                                id="employeesSelect"
                                options={options}
                                isMulti
                                value={this.state.selectedEmployees}
                                onChange={this.handleSelectChange}
                            />
                        </div>
                    </td>
                </tr>);
        }
        return (
            <form className="" onSubmit={this.handleSubmit}>
                <h6>Add a restriction: </h6>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <label>Consecutive learning days:</label>
                            </td>
                            <td>
                                <input
                                    type='number'
                                    min='1'
                                    max='366'
                                    onChange={e => this.setState({ consecutiveDays: e.target.value })}
                                    required
                                />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Max learning days per month:</label> 
                            </td>
                            <td>
                                <input
                                    type='number'
                                    min='1'
                                    max='31'
                                    onChange={e => this.setState({ maxDaysPerMonth: e.target.value })}
                                    required
                                />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Max learning days per quarter:</label> 
                            </td>
                            <td>
                                <input
                                    type='number'
                                    min={this.state.maxDaysPerMonth || '1'}
                                    max='93'
                                    onChange={e => this.setState({ maxDaysPerQuarter: e.target.value })}
                                    required
                                />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Max learning days per year:</label> 
                            </td>
                            <td>
                                <input
                                    type='number'
                                    min={this.state.maxDaysPerQuarter || '1'}
                                    max='366'
                                    onChange={e => this.setState({ maxDaysPerYear: e.target.value })}
                                    required
                                />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Restriction apply to:</label>
                            </td>
                            <td>
                                <select
                                    onChange={e => {
                                        this.setState({ applyTo: e.target.value });
                                        this.displaySelectedField(e.target.value === '0');    
                                    }}
                                >
                                    <option value='2'>Team</option>
                                    <option value='1'>All subordinates</option>
                                    <option value='0'>Group of employees</option>
                                </select>
                            </td>
                        </tr>
                        {selectField}
                    </tbody>
                </table>
                <input type="submit" value="Submit" />
            </form>
        )
    }

    render() {
        return (
            <div>
                <div className='topic-wrapper'>
                    <div className='topic-holder'>
                        <h6>Current Restriction</h6>
                        {this.showRestriction()}
                    </div>                   
                </div>
                <br/>
                <br />
                <div className='topic-wrapper'>
                    <div className='topic-holder'>
                        <h6>My created restrictions</h6>
                        {this.showRestrictionsList()}
                    </div>
                </div>
                <br/>
                <br/>
                <div className='topic-wrapper'>
                    <div className='topic-holder'>
                        {this.showCreationForm()}
                    </div>   
                </div>
            </div>
            )
       
    }
    

    displaySelectedField = (value) => {
        this.setState({
            displaySelectField: value
        })
    }

    handleSelectChange(option){
        this.setState(state => {
            return {
                selectedEmployees: option
            };
        });
    };
}

const mapStateToProps = (state, ownProps) => {
    return {
        currentUser: state.currentUser,
    }
}

const mapDispatchToProps = (dispatch, ownProps) => {
    return {

    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(RestrictionsPage);