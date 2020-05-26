import React from 'react';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';
import { Redirect } from 'react-router-dom';
import TreeView from 'devextreme-react/tree-view';
import './TopicPage.css';

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
        if (this.state.redirect) {
            return <Redirect to={this.state.redirect} />
        }
        return (
            <div>

                <h3>Topics</h3>
                <Link className="btn btn-dark" to={{ pathname: `/createtopic` }} > Add New Topic </Link>
                {this.state.loading || !this.state.data ?
                    <div>
                        loading...
                    </div>
                    :
                    <div>
                        <TreeView
                            id="simple-treeview"
                            items={this.state.data}
                            displayExpr="name"
                            itemRender={this.renderTreeViewItem}
                            itemsExpr="subTopicList"
                            parentIdExpr="parentTopicId"
                            keyExpr="id"
                            searchMode="contains"
                            searchEnabled={true} />
                    </div>
                    
                }
                

            </div>
        );
    }


    renderTreeViewItem(item) {
        console.log(item);
        return (
            <Link to={{ pathname: `/topic/${item.id}` }} > {item.name} </Link>
        );
    }

}
