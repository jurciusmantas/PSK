import React from 'react';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';

export default class TopicPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            data: null
        }
    }

    componentDidMount() {
        get('topic/topic').then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ data: res.data, loading: false })
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    topicList() {
        return this.state.data.map((d) => {
            const {id, name} = d          
            return (
                <tr key={ `topic-list-item-${id}` }>
                    <td>
                        <Link to={{ pathname: `/topic/${id}` }} > {name} </Link>
                    </td>
                </tr>
            )
        })
    }

    render() {
        return (
            <div>
                <h3>Topics</h3>
                { this.state.loading || !this.state.data ?
                    <div>
                        loading...
                    </div>
                    :
                    <table>
                        <tbody>
                            { this.topicList() }
                        </tbody>
                    </table>
                }
            </div>
        );
    }
}
