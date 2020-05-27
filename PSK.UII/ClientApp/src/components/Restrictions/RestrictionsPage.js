import React from "react"
import './RestrictionsPage.css';

import { post } from '../../helpers/request'
import { get } from '../../helpers/request'
import { del } from '../../helpers/request'
import Select from 'react-select';
import ReactDOM from "react-dom";

class RestrictionsPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading1: true,
            consecutiveDays: null,
            maxDaysPerMonth: null,
            maxDaysPerQuarter: null,
            maxDaysPerYear: null,
            creatorId: null,
            applyTo: 2,
            selectedEmployees: [],
            displaySelectField: false,
        }
        this.handleSelectChange = this.handleSelectChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.deleteRestriction = this.deleteRestriction.bind(this);
    }

    componentDidMount() {
        let employeeId = 1;
        get(`restrictions?id=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({
                        restriction: res.data.restriction,
                        restrictions: res.data.restrictionsList,
                        users: res.data.teamMembers,
                        loading1: false
                    })
                    console.log(res.data);
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(reason => {
                console.error(reason);
            })
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
                        <td>{restriction.maxDaysPerYear}</td>
                    </tr>
                    <tr>
                        <td><label>Max Days per Year</label></td>
                        <td>{restriction.maxDaysPerQuarter}</td>

                    </tr>
                </table>
             );
        }
    }
    showRestrictionsList() {
        var restrictions = this.state.restrictions || [];
        if (restrictions != null && restrictions.length > 0) {
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
                                    <td>{restriction.useCount}</td>
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
        else {
            return <div>You do not have any restrictions created</div>
        }   
    }

    deleteRestriction(e) {
        del(`restrictions?id=${e.target.value}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert(res.message);
                }
                else {
                    alert(res.message);
                }
            })
            .catch(error => {
                console.log(error);
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
            CreatorId: 1,
            ApplyTo: parseInt(this.state.applyTo),
            UserNames: this.state.selectedEmployees.map((selectedEmployee) => {
                return selectedEmployee.value
            })
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert(res.message);
                }
                else {
                    alert(res.message);
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
                return { value: user.name, label: user.name };
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
                                    min='1'
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
                                    min='1'
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
                <div>
                    <h6>Current Restriction</h6>
                    {this.showRestriction()}
                </div>
                <br/>
                <br />
                <div>
                    <h6>My restrictions</h6>
                    {this.showRestrictionsList()}
                </div>
                <br/>
                <br/>
                <div>{this.showCreationForm()}</div>
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

export default RestrictionsPage;