import React from 'react';
import { 
    Button, 
    Modal, 
    ModalHeader, 
    ModalBody, 
    ModalFooter,
} from 'reactstrap';
import Loader from '../Loader/loader';

export default class CompletionModal extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            loading: false,
        }
    }

    markAsCompleted(){
        this.setState({ loading: true })
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