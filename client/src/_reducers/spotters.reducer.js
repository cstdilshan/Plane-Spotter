import { userConstants } from '../_constants';

export function spotters(state = {}, action) {
  switch (action.type) {
    case userConstants.SPOTTER_GETALL_REQUEST:
      return {
        loading: true
      };
    case userConstants.SPOTTER_GETALL_SUCCESS:
      return {
        items: action.spotters
      };
    case userConstants.SPOTTER_GETALL_FAILURE:
      return { 
        error: action.error
      };
    default:
      return state
  }
}