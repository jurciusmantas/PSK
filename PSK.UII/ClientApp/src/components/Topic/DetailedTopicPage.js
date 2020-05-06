import React from 'react';
import { get } from '../../helpers/request';
import { Link } from 'react-router-dom';

export default class DetailedTopicPage extends React.Component {
    constructor(props) {
        super(props);
        const queryParams = new URLSearchParams(window.location.search);
        console.log(queryParams.get("id"));
        this.state = {
            loading: true,
            data: null,
            id: queryParams.get("id"),
        }
    }

    componentDidMount() {
        this.setDetails();
    }

    assign() {
        alert("Assigned");
    }

    setDetails() {
        get(`topics/${this.state.id}`).then(res => {
            console.log(res);
            return res.json()
        })
            .then(res => {
                if (res.success) {
                    this.setState({ data: res.data, loading: false });
                }
            })
            .catch(error => {
                console.log(error);
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
                <h5>Subtopics</h5>


                <table>
                    <tbody>
                        {
                            this.state.data.subTopicList.map((d) => {
                                const { id, name } = d

                                return (
                                    <tr key={`subtopic-list-item-${id}`}>
                                        <td>
                                            <Link to={{ pathname: `/topic?id=${id}` }} > {name} </Link>
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
        if (this.state.loading) {
            return (
                <div>loading...</div>
            )
        }
        if (!this.state.data || this.state.id === null) {
            return (
                <div>Not found</div>
            )
        }
        return (
            <div>
                <h3>{this.state.data.name}</h3>

                <h5>Description</h5>
                <p>{this.state.data.description}</p>
                <button className="btn btn-dark" onClick={this.assign}>Assign!</button>
                <div>
                    <Link className="btn btn-dark" to={{ pathname: `/add-topic?parent=${this.state.id}` }} > Add New Subtopic </Link>
                </div>

                {this.showSubtopics()}
            </div>
        )
    }
}
