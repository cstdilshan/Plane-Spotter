import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { registration } from './registration.reducer';
import { users } from './users.reducer';
import { alert } from './alert.reducer';
import { spotterRegistration } from './spotterRegistration.reducer';
import { spotters } from './spotters.reducer';
import { spotterUpdate } from './spotterUpdate.reducer';

const rootReducer = combineReducers({
  authentication,
  registration,
  users,
  alert,
  spotterRegistration,
  spotters,
  spotterUpdate
});

export default rootReducer;