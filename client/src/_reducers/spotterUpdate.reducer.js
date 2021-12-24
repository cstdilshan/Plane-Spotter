import { userConstants } from '../_constants';

export function spotterUpdate(state = {}, action) {
  switch (action.type) {
    case userConstants.SPOTTER_GET_REQUEST:
      return {
        loading: true
      };
    case userConstants.SPOTTER_GET_SUCCESS:
      return {
        spotter: action.spotter
      };
    case userConstants.SPOTTER_GET_FAILURE:
      return { 
        error: action.error
      };
    default:
      return state
  }
}