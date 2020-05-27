import React from 'react';
import './TopicPage.css';
import { get } from '../../helpers/request'
import { Link } from 'react-router-dom';
import { Redirect } from 'react-router-dom';
import TreeView from 'devextreme-react/tree-view';
import './TopicPage.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'

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
            })
            .catch(error => {
                console.log(error);
            })
    }


    render() {
        if (this.state.redirect) {
            return <Redirect to={this.state.redirect} />
        }
        return (
            <div className="topic-wrapper">
                <div className="topic-holder">

                    <h2>Topics</h2>
                    <Link className="btn btn-dark" to={{ pathname: `/add-topic` }} > Add New Topic </Link>
                    {this.state.loading || !this.state.data ?
                        <div className="loader">
                            <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
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
            </div>
        );
    }


    renderTreeViewItem(item) {
        console.log(item);
        return (
            <Link to={{ pathname: "/topic", search: `?id=${item.id}` }} > {item.name} </Link>
        );
    }

}