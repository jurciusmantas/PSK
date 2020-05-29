import React from 'react';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';
import TreeView from 'devextreme-react/tree-view';
import './TopicPage.css';
import Loader from '../Loader/loader';
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
        if (this.props.data)
            this.setState({ 
                data: this.props.data,
                loading: false,
            });

        else
            get('topics')
                .then(res => res.json())
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

    render() {
        if (this.state.loading || !this.state.data)
            return <Loader />

        return (
            <div className="topic-wrapper">
                <div className="topic-holder">
                    { this.props.data === undefined &&
                        <React.Fragment>
                            <h2>Topics</h2>
                            <Link 
                                className="btn btn-dark" 
                                to={{ pathname: `/add-topic` }} 
                            >
                                Add New Topic 
                            </Link>
                        </React.Fragment>
                    }
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
                </div>
            </div>
        );
    }

    renderTreeViewItem(item) {
        return (
            <Link to={{ pathname: "/topic", search: `?id=${item.id}` }} > {item.name} </Link>
        );
    }
}