import React from 'react';
import './TopicPage.css';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'
import { notification } from '../../helpers/notification';

export default class TopicPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            data: null
        }
    }

    componentDidMount() {
        get('topics').then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ data: res.data, loading: false })
                }
                else {
                    notification('Cannot get topics :(', 'error');
                    console.warn(`Cannot get topics:`);
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error('GET topics failed:')
                console.error(error);
            })
    }

    topicList() {
        return this.state.data.map((d) => {
            const {id, name} = d          
            return (
                <tr key={ `topic-list-item-${id}` }>
                    <td>
                        <Link to={{ pathname: "/topic", search: `?id=${id}` }} > {name} </Link>
                    </td>
                </tr>
            )
        })
    }

    render() {
        return (
            <div className="topic-wrapper">
                <div className="topic-holder">
                    <h2>Topics</h2>
                    <Link className="btn btn-dark" to={{ pathname: `/add-topic` }} > Add New Topic </Link>
                    {this.state.loading || !this.state.data ?
                        <div className="loader">
                            <FontAwesomeIcon icon={faSpinner} height="20px" />
                        </div>
                        :
                        <table>
                            <tbody>
                                {this.topicList()}
                            </tbody>
                        </table>
                    }
                </div>
            </div>
        );
    }
}
