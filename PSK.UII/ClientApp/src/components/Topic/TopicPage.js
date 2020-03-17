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
        const listItems = this.state.data.map((d) => <li key={d.name}>{d.name} - {d.description}</li>);

        return (
            <ol>
                { listItems }
            </ol>
        )
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
                    <div>
                        { this.topicList() }
                    </div>
                }
            </div>
        );
    }
}
