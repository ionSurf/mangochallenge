import { combineReducers } from 'redux';
import { reducer as formReducer } from 'redux-form';
import { authentication } from './authentication.reducer';
import { portraits } from './portrait.reducer';
import { alert } from './alert.reducer';

const rootReducer = combineReducers({
  authentication,
  // auth: authentication
  // portraits: portraits
  portraits,
  alert,
  form : formReducer
});

export default rootReducer;