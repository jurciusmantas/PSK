import {toastr} from 'react-redux-toastr';
import 'react-redux-toastr/lib/css/react-redux-toastr.min.css'

export function notification(message, position = 'bottom-center', type='success'){
    let options = {
        closeOnToastrClick: true,
        position: position,
        icon: null,
    };

    switch(type){
        case 'error':
            toastr.error('', message, options);
            break;
        case 'info':
            toastr.info('', message, options);
            break;
        case 'warning':
            toastr.warning('', message, options);
            break;
        case 'success':
        default:
            toastr.success('', message, options);
            break;
    }
}