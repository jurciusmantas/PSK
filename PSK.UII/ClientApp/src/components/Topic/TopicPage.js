import React from 'react';
import { get } from '../../helpers/request'

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
        return this.state.data.map((d, index) => {
            const {name, description} = d
            return (
                <tr key={"topic-list-item-" + index}>
                    <td>{name}</td>
                    <td>{description}</td>
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
