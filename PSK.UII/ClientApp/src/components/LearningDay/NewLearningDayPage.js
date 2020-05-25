import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from 'moment';
import { get, post } from '../../helpers/request';

import './NewLearningDayPage.css';

class NewLearningDayPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedDate: moment().format("YYYY-MM-DD"),
            topics: [],
            selectedTopicId: 0,
            recommendations: [],
        };
        this.changeDate = this.changeDate.bind(this);
        this.changeTopic = this.changeTopic.bind(this);
        this.createDay = this.createDay.bind(this);
        this.makeTopicOptionList = this.makeTopicOptionList.bind(this);
    }

    componentDidMount() {
        get("topics").then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ topics: res.data, selectedTopicId: res.data[0].id });
        }).catch(err => {
            console.error(`GET /api/topics failed: ${err}`);
            this.setState({ topics: [] });
        });
        get(`recommendations?receiverId=${this.props.currentUser.id}`).then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ recommendations: res.data });
        }).catch(err => {
            console.error(`GET /api/recommendations failed: ${err}`);
        })
    }

    changeDate(newDate) {
        this.setState({ selectedDate: newDate.target.value });
    }

    changeTopic(e) {
        console.log(e.target.value);
        this.setState({ selectedTopicId: parseInt(e.target.value) });
    }

    createDay() {
        post('days', {
            date: this.state.selectedDate,
            employeeId: this.props.currentUser.id,
            topicId: this.state.selectedTopicId,
        })
            //.then(r => r.json())
            //.then(res => console.log(res))
            .then(() => {
                this.props.history.push('/home');
            })
            .catch(reason => {
                console.error(`POST days failed`);
                console.error(reason);
            });
    }

    makeTopicOptionList() {
        const recommended = this.state.topics
            .filter(t => this.state.recommendations.find(r => r.topicId === t.id) !== undefined)
            .map(t => <option key={`topic-selection-${t.id}`} value={t.id}>{"\u2606"} {t.name}</option>);
        const other = this.state.topics
            .filter(t => this.state.recommendations.find(r => r.topicId === t.id) === undefined)
            .map(t => <option key={`topic-selection-${t.id}`} value={t.id}>{t.name}</option>);
        return [...recommended, ...other];
    }

    render() {
        return (
            <div>
                <h1>Create new learning day</h1>
                <form>
                    <div>
                        <label htmlFor="learn-date">Date:</label>
                        <input
                            type="date"
                            id="learn-date"
                            defaultValue={this.state.selectedDate}
                            onChange={this.changeDate}
                            min={moment().format("YYYY-MM-DD")}
                            pattern="d{4}-d{2}-d{2}"
                        />
                    </div>
                    <div>
                        <label htmlFor="topics">Topic:</label>
                        <select
                            id="topics"
                            onChange={this.changeTopic}
                        >
                            {this.makeTopicOptionList()}
                        </select>
                    </div>
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