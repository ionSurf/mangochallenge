import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchPortraits } from '../../actions';
import PortraitListItem from './PortraitListItem';
import Header from '../Header/Header';

class PortraitList extends React.Component {
  componentDidMount() {
    this.props.fetchPortraits();
    //console.log(this.props);
  }

  renderList() {
    //console.log(this.props.portraits);
    return this.props.portraits.map((portrait, i) =>
      <PortraitListItem
        key={i}
        keyIndex={i}
        portrait={portrait}
        currentUserId={this.props.currentUserId}
      >
      </PortraitListItem>);
  }

  renderCreate() {
    if (this.props.user) {
      return (
        <div style={{ textAlign: 'right' }}>
          <Link to="/portraits/new" className="ui button primary">
            Create Portrait
          </Link>
        </div>
      );
    }
  }

  render() {
    return (
      <div>
        <Header />
        <h2>Portraits</h2>
        <div className="ui celled list three column doubling stackable masonry grid">

          {this.renderList()}

        </div>
        {this.renderCreate()}
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    portraits: Object.values(state.portraits),
    user: state.authentication.user,
  };
};

export default connect(
  mapStateToProps,
  { fetchPortraits }
)(PortraitList);
