import React from 'react';
import { Spinner } from 'react-bootstrap';
import './loader.css'

const Loader = () => (
    <div className='spinner-holder'>
        <Spinner animation="border"/>
    </div>
)

export default Loader;