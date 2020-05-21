import React from 'react';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'

export default class DetailedTopicPage extends React.Component {
    constructor(props) {
        super();
        this.state = {
            loading: true,
            data: null,
            id: null
        }
    }

    componentDidMount() {

        this.setDetails();
    }

    assign() {
        alert("Assigned");
      }

    setDetails() {
        let id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1); //Takes id from the URL
        this.setState({
            id: id
        });

        get(`topic/topic/${id}`).then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ data: res.data, loading: false })
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    componentWillReceiveProps(){
        this.setDetails();
        this.forceUpdate();  
    }

    showSubtopics() {
        if (!this.state.data.subTopicList)
        {
            return (
                <div>
                    Subtopic not found
                </div>
            )
        }
        else if (Array.isArray(this.state.data.subTopicList) && this.state.data.subTopicList.length === 0)
        {
            return (
                <div>
                    No subtopics
                </div>
            )
        }

        return (
            <div>
                <table>
                    <tbody>
                        { 
                        this.state.data.subTopicList.map((d) => {
                            const {id, name} = d

                            return (
                                <tr key={ `subtopic-list-item-${id}` }>
                                    <td> 
                                        <Link to={{ pathname: `/topic/${id}`}} > {name} </Link>
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
        {
            return (
                <div className="loader">
                    <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                </div>
            )
        }
        else if (!this.state.loading && !this.state.data)
        {
            return (
                <div>
                    Not found
                </div>
            )
        }
        return (
            <div className="topic-wrapper">
                <div className="topic-holder">
                    <h2>{this.state.data.name}</h2>
                    <h5>Description</h5>
                <p>{ this.state.data.description }</p>
                    <button className="btn btn-custom" onClick={this.assign}>Assign!</button>
                    <hr/>
                    <h5>Subtopics</h5>
                    <div>
                        <Link className="btn btn-custom" to={{ pathname: `/createtopic/${this.state.id}` }} > Add New Subtopic </Link>
                    </div>
                    {this.showSubtopics()}
                </div>
            </div>
        )
      }
}


