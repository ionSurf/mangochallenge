import React from 'react';
import { Link } from 'react-router-dom';

const createPortraitButton = () => {
    //if (this.props.user) {
    return (
        <div style={{ textAlign: 'right' }}>
            <Link to="/portraits/new" className="ui button primary">
                Create Portrait
            </Link>
        </div>
    );
    //}
}

export default createPortraitButton;