import React from 'react';
import { get } from '../../helpers/request'


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

        get('topic/detailedtopic?id=' + id).then(res => res.json())
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

            <h5>Subtopics</h5>

            <table>
                <tbody>
                    { 
                    this.state.data.subTopicList.map((d) => {
                        const {id, name, description} = d
                        
                        return (
                            <tr key={"subtopic-list-item-" + id}>
                                <td> {name} </td>
                                <td> {description} </td>
                            </tr>
                        )
                    })
                    }
                </tbody>
            </table>

        </div>
        )
    }
}


