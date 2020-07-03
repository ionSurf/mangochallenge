import React from "react";
import { Link } from 'react-router-dom';

const PortraitListItemButtons = ({ portraitId, portraitUserId, currentUserId }) => 
   //(portraitUserId === currentUserId) ?
     (
      <div className="right floated content">
        <Link
          to={`/portraits/edit/${portraitId}`}
          className="ui button primary"
        >
          Edit
        </Link>
        <Link
          to={`/portraits/delete/${portraitId}`}
          className="ui button negative"
        >
          Delete
        </Link>
      </div>
    ) //: '';

export default PortraitListItemButtons;