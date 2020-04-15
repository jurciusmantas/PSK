import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import queryString from 'query-string';
import { get } from '../../helpers/request';

import Select from "react-select";
import DatePicker from "react-datepicker";

import "react-datepicker/dist/react-datepicker.css";

class NewLearningDayPage extends React.Component {
    constructor(props) {
        super(props);
        console.log(this.props.location.search);
        this.queryArgs = queryString.parse(this.props.location.search);
        this.state = {
            selectedDate: new Date(this.queryArgs.date),
            topics: [],
            selectedTopic: {},
        };
        this.changeDate = this.changeDate.bind(this);
        this.changeTopic = this.changeTopic.bind(this);
        this.createDay = this.createDay.bind(this);
    }

    componentDidMount() {
        get("topics").then(res => res.json()).then(res => {
            console.log(`${res.success} : ${res.data}`);
            if (res.success)
                this.setState({ topics: res.data, loading: false });
        }).catch(err => {
            console.log(`GET failed: ${err}`);
            this.setState({ topics: [], loading: false });
        });
    }

    changeDate(newDate) {
        this.setState({ selectedDate: newDate });
    }

    changeTopic(selectedTopic) {
        this.setState({ selectedTopic });
    }

    createDay() {
        console.log(this.state);
        alert("Will post, but does not because database is bad");
    }

    render() {
        return (
            <div>
                <h1>Create new learning day</h1>
                <form>
                    <label>Date</label>
                    <DatePicker
                        selected={this.state.selectedDate}
                        onChange={this.changeDate}
                        dateFormat="yyyy-MM-dd"
                        minDate={new Date()}
                    />
                    <label>Topic:</label>
                    <Select options={this.state.topics}
                        defaultValue={this.state.topics[0]}
                        getOptionLabel={topic => topic.name}
                        getOptionValue={topic => topic.id}
                        onChange={this.changeTopic}
                    />
                    <button type="button" className="btn btn-dark" onClick={this.createDay}>Create</button>
                </form>
            </div>
        )
    }
}

function mapStateToProps(state, ownProps) {
    return {
        currentUser: state.currentUser
    };
}

function mapDispatchToProps() {
    return {};
}

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(NewLearningDayPage));