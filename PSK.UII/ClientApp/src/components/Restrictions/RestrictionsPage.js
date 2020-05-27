import React from "react"
import './RestrictionsPage.css';

import { post } from '../../helpers/request'
import { get } from '../../helpers/request'
import { Link } from "react-router-dom";
import Select from 'react-select';
import ReactDOM from "react-dom";

class RestrictionsPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading1: true,
            loading2: true,
            loading3: true,
            consecutiveDays: null,
            maxDaysPerMonth: null,
            maxDaysPerQuarter: null,
            maxDaysPerYear: null,
            selectedUser: null,
            creatorId: null,
            applyTo: null,
            selectedUsers: []
        }
    }

    componentDidMount() {
        var employeeId = 1;
        get(`restrictions/current_restriction?id=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ restriction: res.data, loading1: false })
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })
        get(`restrictions/restrictions?id=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ restrictions: res.data, loading2: false })
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
               console.log(error);
            })
    }

    getUsers() {
        var employeeId = 1;
        get(`restrictions/users?id=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ users: res.data, loading3: false })
                    console.log(res.data)
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    showRestriction() {
        var restriction = this.state.restriction;
        if (this.state.loading1) {
            return <tr><td>Loading...</td></tr>
        }
        else if (restriction == null) {
            return <tr><td>No restriction is applied</td></tr>
        }
        else {
            return (
                <tr>
                    <td>{restriction.consecutiveDays}</td>
                    <td>{restriction.maxDaysPerYear}</td>
                    <td>{restriction.maxDaysPerQuarter}</td>
                    <td>{restriction.maxDaysPerMonth}</td>
                </tr>);
        }
    }
    showRestrictionsList() {
        var restrictions = this.state.restrictions;
        if (this.state.loading2) {
            return <tr><td>Loading...</td></tr>
        }
        else if (restrictions == null) {
            return <tr><td>You do not have any restrictions created</td></tr>
        }
        else {
            return (
                restrictions.map((restriction, index) => {
                    return (
                        <tr key={index}>
                            <td>{restriction.consecutiveDays}</td>
                            <td>{restriction.maxDaysPerYear}</td>
                            <td>{restriction.maxDaysPerQuarter}</td>
                            <td>{restriction.maxDaysPerMonth}</td>
                            <td>{restriction.creationDate}</td>
                            <td>{restriction.useCount}</td>
                            <td><Link to={`delete?id=${restriction.id}`}>Delete</Link></td>
                        </tr>
                    )
                })
             );
        }   
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    handleSubmit(e) {
        e.preventDefault();
        const { consecutiveDays,
            maxDaysPerMonth,
            maxDaysPerQuarter,
            maxDaysPerYear,
            creatorId,
            applyTo,
            userNames
        } = this.state

        post('restriction/create', {
            consecutiveDays: consecutiveDays,
            maxDaysPerMonth: maxDaysPerMonth,
            maxDaysPerQuarter: maxDaysPerQuarter,
            maxDaysPerYear: maxDaysPerYear,
            creatorId: 1,
            applyTo: applyTo,
            userNames: userNames
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
        return (
            <form className="" onSubmit={this.handleSubmit}>
                <h5>Add a restriction: </h5>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <label>Consecutive learning days:</label>
                            </td>
                            <td>
                                <input
                                    type='number'
                                    min='0'
                                    onChange={e => this.setState({ consecutiveDays: e.target.value })}
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
                                    min='0'
                                    onChange={e => this.setState({ maxDaysPerMonth: e.target.value })}
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
                                    min='0'
                                    onChange={e => this.setState({ maxDaysPerQuarter: e.target.value })}
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
                                    min='0'
                                    onChange={e => this.setState({ maxDaysPerYear: e.target.value })}
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
                                        this.getUsers();
                                        console.log(this.state.users);
                                        const employeeSelectionLabelCol = (
                                            <label>Select employees for restriction:</label>
                                        );
                                        const employeeSelectionActionsCol = (
                                            <div>
                                                <div className="">
                                                    {this.state.selectedUsers.map((item) => (
                                                        <li>{item}</li>
                                                    ))}
                                                </div>
                                                <div id="selectField" className="">
                                                    <Select
                                                        name="form-field-name"
                                                        value={this.state.value}
                                                        onChange={this.handleSelectChange}
                                                        clearable={this.state.clearable}
                                                        searchable={this.state.searchable}
                                                        labelKey='name'
                                                        valueKey='name'
                                                        options={this.state.users}
                                                    />
                                                    <button onClick={this.handleAddEmployeeChange}>Add Employee</button>
                                                </div>
                                            </div>
                                        );
                                        if (e.target.value === '0') {
                                            ReactDOM.render(employeeSelectionLabelCol, document.getElementById('employeeSelectionLabelCol'));
                                            ReactDOM.render(employeeSelectionActionsCol, document.getElementById('employeeSelectionActionsCol'));
                                        }

                                    }}
                                >
                                    <option value='1'>Team</option>
                                    <option value='2'>All subordinates</option>
                                    <option value='0'>Group of employees</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td id="employeeSelectionLabelCol"></td>
                            <td id="employeeSelectionActionsCol"></td>
                        </tr>
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
                    <table>
                        <tbody>{this.showRestriction()}</tbody>
                    </table>
                </div>
                <div>
                    <table>
                        <tbody>{this.showRestrictionsList()}</tbody>
                    </table>
                </div>
                <div>
                    <div>{this.showCreationForm()}</div>
                </div>
            </div>
            )
       
        

    }
    handleSelectChange(selectedOption) {
        this.setState({ selectedUser: selectedOption });
    }

    handleAddEmployeeChange() {
        this.setState(state => {
            const list = state.selectedUsers.concat(state.selectedUser);
                return {
                    list,
                    value: '',
                };
            });
    }
}

export default RestrictionsPage;