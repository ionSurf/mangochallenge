import React from "react";
import { Link } from "react-router-dom";
import PortraitListItemButtons from "./PortraitListItemButtons";
import { PortraitImageContainer } from "./PortraitImageContainer";

const PortraitListItem = ({ portrait, currentUserId, keyIndex }) => {
  return (
    <div className="item ui fluid card" key={keyIndex}>
      <PortraitImageContainer
          //src={(portrait.url)}
          src={( process.env.REACT_APP_API_URL + portrait.url)}
          // process.env.REACT_APP_PUBLIC_URL
          title={portrait.title}
          />
        <PortraitListItemButtons
        portraitId={portrait.id}
        portraitUserId={portrait.userId}
        currentUserId={currentUserId}
      />
      <div className="content">
        <Link to={`/portraits/${portrait.id}`} className="header">
            {portrait.title}
          </Link>
        <div className="description">{portrait.description}</div>
      </div>
    </div>
  );
};

export default PortraitListItem;
