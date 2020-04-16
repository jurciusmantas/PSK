import React from 'react';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';

export default class DetailedTopicPage extends React.Component {
    constructor(props) {
        super();
        this.state = {
            loading: true,
            data: null           
        }
    }

    componentDidMount() {

        let id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1); //Takes id from the URL
       
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

    assign() {
        alert("Assigned");
      }

    showSubtopics() {
        if (!this.state.data.subTopicList)
        {
            console.log(this.state.data.subTopicList)
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
                <h5>Subtopics</h5>
                <table>
                    <tbody>
                        { 
                        this.state.data.subTopicList.map((d) => {
                            const {id, name} = d

                            return (
                                <tr key={ `subtopic-list-item-${id}` }>
                                    <td> 
                                        <Link to={{ pathname: `/topic/${id}` }} > {name} </Link>
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
                <div>
                    loading...
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
            <div>
            <h3>{ this.state.data.name }</h3>

            <h5>Description</h5>
            <p>{ this.state.data.description }</p>
            <button onClick={ this.assign }>Assign!</button>

            {this.showSubtopics()}
        </div>
        )
    }
}


