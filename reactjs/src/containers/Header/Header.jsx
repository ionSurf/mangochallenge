import React from 'react';
import { Link } from 'react-router-dom';
//  import {createPortraitButton} from '../../components/createPortraitButton';

const Header = () => {
  return (
    <div className="ui secondary pointing menu">
      <Link to="/" className="item">
        Portraity
      </Link>
      <div className="right menu">
        <Link to="/" className="item">
          All Portraits
        </Link>
        <div style={{ textAlign: 'right' }}>
            <Link to="/portraits/new" className="ui button primary">
                Create Portrait
            </Link>
        </div>
      </div>
    </div>
  );
};

export default Header;
