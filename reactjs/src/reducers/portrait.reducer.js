import _ from "lodash";
import { portraitConstants } from "../constants";

export function portraits(state = {}, action) {
  switch (action.type) {
    case portraitConstants.GETALL_REQUEST:
      return []; //{ loading: true };
    case portraitConstants.GETALL_SUCCESS:
      return { ...state, ..._.mapKeys(action.portraits, "id") };
    case portraitConstants.GETALL_FAILURE:
      return {
        error: action.error,
      };

    case portraitConstants.GETBYID_REQUEST:
      return []; //{ loading: true };
    case portraitConstants.GETBYID_SUCCESS:
      return { ...state, [action.portrait.id]: action.portrait };
    case portraitConstants.GETBYID_FAILURE:
      return {
        error: action.error,
      };

    case portraitConstants.CREATE_REQUEST:
      return []; //{ loading: true };
    case portraitConstants.CREATE_SUCCESS:
      return { ...state, [action.portrait.id]: action.portrait };
    case portraitConstants.CREATE_FAILURE:
      return {
        error: action.error,
      };

    case portraitConstants.UPDATE_REQUEST:
      return []; //{ loading: true };
    case portraitConstants.UPDATE_SUCCESS:
      return { ...state, [action.portrait.id]: action.portrait };
    case portraitConstants.UPDATE_FAILURE:
      return {
        error: action.error,
      };

    case portraitConstants.UPLOAD_REQUEST:
      return []; //{ loading: true };
    case portraitConstants.UPLOAD_SUCCESS:
      return { ...state, imageUrl : action.imageUrl };
    case portraitConstants.UPLOAD_FAILURE:
      return {
        error: action.error,
      };

    case portraitConstants.DELETE_REQUEST:
      return []; //{ loading: true };
    case portraitConstants.DELETE_SUCCESS:
      return _.omit(state, action.portraitId);
    case portraitConstants.DELETE_FAILURE:
      return {
        error: action.error,
      };
    default:
      return state;
  }
}
