import React from 'react';
import './NotFoundPage.css';

const NotFoundPage = () => {
    return (
        <div className="error-container">
            <div className="error-wrapper">
                <h1>404</h1>
                <hr />
                <h3>Page not found</h3>
            </div>
        </div>
    )
};

export default NotFoundPage;