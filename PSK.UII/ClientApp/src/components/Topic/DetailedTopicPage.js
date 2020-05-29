import React from 'react';
import './TopicPage.css';
import { get } from '../../helpers/request';
import { Link } from 'react-router-dom';
import Loader from '../Loader/loader'

export default class DetailedTopicPage extends React.Component {
    constructor(props) {
        super(props);
        const queryParams = new URLSearchParams(window.location.search);
        this.state = {
            loading: true,
            data: null,
            id: queryParams.get("id"),
        }
    }

    componentDidMount() {
        this.setDetails();
    }

    setDetails() {
        get(`topics/${this.state.id}`).then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ data: res.data, loading: false });
                }
            })
            .catch(error => {
                console.error(error);
            })
    }


    showSubtopics() {
        if (!this.state.data.subTopicList) {
            return (
                <div>Subtopics not found</div>
            )
        }
        else if (Array.isArray(this.state.data.subTopicList) && this.state.data.subTopicList.length === 0) {
            return (
                <div>No subtopics</div>
            )
        }

        return (
            <div>
                <table>
                    <tbody>
                        {
                            this.state.data.subTopicList.map((d) => {
                                const { id, name } = d

                                return (
                                    <tr key={`subtopic-list-item-${id}`}>
                                        <td>
                                            <Link onClick={this.forceUpdate} to={{ pathname: "/topic", search: `?id=${id}` }}>{name}</Link>
                                        </td>
                                    </tr>
                                )
                            })
                        }
                    </tbody>
                </table>
            </div>
        )
    }

    render() {
        if (this.state.loading)
            return <Loader/>
        
        if (!this.state.data || this.state.id === null)
            return <div>Not found</div>
        
        return (
            <div className="topic-wrapper">
                <div className="topic-holder">
                    <h2>{this.state.data.name}</h2>
                    <h5>Description</h5>
                    <p>{this.state.data.description}</p>
                    <hr />
                    <h5>Subtopics</h5>
                    <div>
                        <Link className="btn btn-dark" to={{ pathname: "/add-topic", search: `?parent=${this.state.id}` }}>Add New Subtopic</Link>
                    </div>
                    {this.showSubtopics()}
                </div>
            </div>
        )
    }
}
