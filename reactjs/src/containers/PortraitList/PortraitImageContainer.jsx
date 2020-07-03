import React from "react";
// <img src={require(`../${src}`)} alt={title} />
export const PortraitImageContainer = ({src, title}) => (
    <div className="portrait-container">
        <img src={src} alt={title} />
    </div>
)