import React from "react"
import './RestrictionsPage.css';

import { post } from '../../helpers/request'
import { get } from '../../helpers/request'

class RestrictionsPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
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
        employeeId = 1;
        get('restrictions/restriction/' + employeeId)
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
        get('restrictions/restrictions/' + employeeId)
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
        get('restrictions/users/' + employeeId)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ users: res.data, loading3: false })
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
        return this.state.restriction.map((restriction, index) => {
            return (
                <tr key={index}>
                    <td>{restriction.consecutiveDays}</td>
                    <td>{restriction.maxDaysPerYear}</td>
                    <td>{restriction.maxDaysPerQuarter}</td>
                    <td>{restriction.maxDaysPerMonth}</td>
                </tr>
            )
        })
    }
    showRestrictionsList() {
        return this.state.restrictions.map((restriction, index) => {
            return (
                <tr key={index}>
                    <td>{restriction.consecutiveDays}</td>
                    <td>{restriction.maxDaysPerYear}</td>
                    <td>{restriction.maxDaysPerQuarter}</td>
                    <td>{restriction.maxDaysPerMonth}</td>
                    <td>{restriction.creationDate}</td>
                    <td>{restriction.useCount}</td>
                    <td><Link to={'delete/' + restriction.id}>Delete</Link></td>
                </tr>
            )
        })
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
        var creatorId = 1;

        post('restriction/create', {
            consecutiveDays: consecutiveDays,
            maxDaysPerMonth: maxDaysPerMonth,
            maxDaysPerQuarter: maxDaysPerQuarter,
            maxDaysPerYear: maxDaysPerYear,
            creatorId: creatorId,
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
                <h3>Add a restriction: </h3>
                <div className="">
                    <label>Consecutive learning days:</label>
                    <input
                        type='number'
                        min='0'
                        onChange={e => this.setState({ consecutiveDays: e.target.value })}
                    />
                </div>
                <div className="">
                    <label>Max learning days per month:</label> 
                    <input
                        type='number'
                        min='0'
                        onChange={e => this.setState({ maxDaysPerMonth: e.target.value })}
                    />
                </div>
                <div className="">
                    <label>Max learning days per quarter:</label> 
                    <input
                        type='number'
                        min='0'
                        onChange={e => this.setState({ maxDaysPerQuarter: e.target.value })}
                    />
                </div>
                <div className="">
                    <label>Max learning days per year:</label> 
                    <input
                        type='number'
                        min='0'
                        onChange={e => this.setState({ maxDaysPerYear: e.target.value })}
                    />
                </div>
                <div className="">
                    <label>Restriction apply to:</label>
                    <select
                        onChange={e => {
                            this.setState({ applyTo: e.target.value });
                            const employeeSelectionField = (
                                <div id="employeeSelectionField">
                                    <div className="">
                                        {this.state.selectedUsers.map((item) => (
                                            <Item item={item} />
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
                            if (e.target.value == 0) {
                                ReactDOM.render(employeeSelectionField, document.getElementById('employeeSelectionField'));
                            }

                        }}
                    >
                        <option value='0'>Group of employees</option>
                        <option value='1'>Team</option>
                        <option value='2'>All subordinates></option>
                    </select>
                    <div id="employeeSelectionField"></div>
                </div>
                <input type="submit" value="Submit" />
            </form>
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