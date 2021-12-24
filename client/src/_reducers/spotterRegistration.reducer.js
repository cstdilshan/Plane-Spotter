import { userConstants } from '../_constants';

export function spotterRegistration(state = {}, action) {
  switch (action.type) {
    case userConstants.SPOTTER_REGISTER_REQUEST:
      return { spotterRegistering: true };
    case userConstants.SPOTTER_REGISTER_SUCCESS:
      return {};
    case userConstants.SPOTTER_REGISTER_FAILURE:
      return {};
    default:
      return state
  }
}