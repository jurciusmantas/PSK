import React from 'react';
import { 
    Button, 
    Modal, 
    ModalHeader, 
    ModalBody, 
    ModalFooter,
} from 'reactstrap';
import Loader from '../Loader/loader';
import { post } from '../../helpers/request';
import { notification } from '../../helpers/notification';
import { connect } from 'react-redux';

class CompletionModal extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            loading: false,
        }
    }

    markAsCompleted(){
        this.setState({ loading: true })
        post('topics/completed', {
            topicId: this.props.topicId,
            employeeId: this.props.currentUser.id,
        })
            .then(res => res.json())
            .then(res => {
                this.props.close();
                if (res.success)
                    notification('Marked as completed successfuly');
                else
                    notification('Error while marking as completed - ' + res.message, 'error');

                this.setState({ loading: false });
            })
            .catch(reason => {
                console.error(`POST topics/completed failed`);
                console.error(reason);
            });
    }

    render(){
        return (
            <Modal
                isOpen={this.props.isOpen}
                toggle={() => this.props.close()}
                centered
            >
                <ModalHeader>Mark as completed</ModalHeader>
                { this.state.loading &&
                    <Loader/>
                }
                { !this.state.loading &&
                    <React.Fragment>
                        <ModalBody>
                            Are you sure you want to mark topic "{this.props.topicName}" as completed at day {this.props.yearMonth}-{this.props.monthDay}?
                        </ModalBody>
                        <ModalFooter>
                            <Button
                                className='btn-custom'
                                onClick={() => this.markAsCompleted()}
                            >
                                Ok
                            </Button>
                            <Button
                                onClick={() => this.props.close()}
                            >
                                Cancel
                            </Button>
                        </ModalFooter>
                    </React.Fragment>
                }
            </Modal>
        )
    }
}

const mapStateToProps = (state) => ({
    currentUser: state.currentUser
});

const mapDispatchToProps = () => ({})

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CompletionModal);