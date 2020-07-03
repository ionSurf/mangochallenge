import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { Modal } from '../Modal';
import {history} from '../../helpers/history';
import { fetchPortrait, deletePortrait } from '../../actions';

class PortraitDelete extends React.Component {
  componentDidMount() {
    this.props.fetchPortrait(this.props.match.params.id);
  }

  renderActions() {
    const { id } = this.props.match.params;

    return (
      <React.Fragment>
        <button
          onClick={() => this.props.deletePortrait(id)}
          className="ui button negative"
        >
          Delete
        </button>
        <Link to="/" className="ui button">
          Cancel
        </Link>
      </React.Fragment>
    );
  }

  renderContent() {
    if (!this.props.portrait) {
      return 'Are you sure you want to delete this portrait?';
    }

    return `Are you sure you want to delete the portrait with title: ${
      this.props.portrait.title
    }`;
  }

  render() {
    return (
      <Modal
        title="Delete Portrait"
        content={this.renderContent()}
        actions={this.renderActions()}
        onDismiss={() => history.push('/')}
      />
    );
  }
}

const mapStateToProps = (state, ownProps) => {
  return { portrait: state.portraits[ownProps.match.params.id] };
};

export default connect(
  mapStateToProps,
  { fetchPortrait, deletePortrait }
)(PortraitDelete);
