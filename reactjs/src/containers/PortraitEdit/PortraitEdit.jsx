import _ from 'lodash';
import React from 'react';
import { connect } from 'react-redux';
import { fetchPortrait, updatePortrait } from '../../actions';
import PortraitForm from '../../components/PortraitForm';

class PortraitEdit extends React.Component {
  componentDidMount() {
    this.props.fetchPortrait(this.props.match.params.id);
  }

  onSubmit = formValues => {
    this.props.updatePortrait(this.props.match.params.id, formValues);
  };

  render() {
    if (!this.props.portrait) {
      return <div>Loading...</div>;
    }

    return (
      <div>
        <h3>Edit a Portrait</h3>
        <PortraitForm
          initialValues={_.pick(this.props.portrait, 'title', 'description','url')}
          onSubmit={this.onSubmit}
        />
      </div>
    );
  }
}

const mapStateToProps = (state, ownProps) => {
  return { portrait: state.portraits[ownProps.match.params.id] };
};

export default connect(
  mapStateToProps,
  { fetchPortrait, updatePortrait }
)(PortraitEdit);
