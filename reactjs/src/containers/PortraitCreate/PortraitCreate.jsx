import React from 'react';
import { connect } from 'react-redux';
import { createPortrait } from '../../actions';
import PortraitForm from '../../components/PortraitForm';

class PortraitCreate extends React.Component {
  onSubmit = formValues => {
    this.props.createPortrait(formValues);
  };

  render() {
    return (
      <div>
        <h3>Create a Portrait</h3>
        <PortraitForm onSubmit={this.onSubmit} />
      </div>
    );
  }
}

export default connect(
  null,
  { createPortrait }
)(PortraitCreate);
