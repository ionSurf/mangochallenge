import { portraitConstants } from '../constants';
import { portraitService } from '../services';
import { alertActions } from '.';
import { history } from '../helpers';

export const fetchPortraits = _ => async dispatch => {
    const request = _         => ({ type: portraitConstants.GETALL_REQUEST });
    const success = portraits => ({ type: portraitConstants.GETALL_SUCCESS, portraits });
    const failure = error     => ({ type: portraitConstants.GETALL_FAILURE, error });

    dispatch( request() );
    await portraitService.getAll().then(
        portraits => {
            dispatch( success(portraits) );
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}

export const fetchPortrait = portraitId => async dispatch => {
    const request = _         => ({ type: portraitConstants.GETBYID_REQUEST });
    const success = portrait  => ({ type: portraitConstants.GETBYID_SUCCESS, portrait });
    const failure = error     => ({ type: portraitConstants.GETBYID_FAILURE, error });

    dispatch( request() );
    portraitService.getById( portraitId ).then(
        portrait => {
            dispatch( success(portrait) );
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}

/*export const createPortrait = formValues => async (dispatch, getState) => {
    const request = _         => ({ type: portraitConstants.CREATE_REQUEST });
    const success = portrait  => ({ type: portraitConstants.CREATE_SUCCESS, portrait });
    const failure = error     => ({ type: portraitConstants.CREATE_FAILURE, error });

    dispatch( request() );
    portraitService.create( formValues ).then(
        portrait => {
            dispatch( success(portrait) );
            history.push("/");
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}*/

//export const uploadImage = formValues => async (dispatch, getState) => {
export const createPortrait = formValues => async (dispatch, getState) => {
    const request = _         => ({ type: portraitConstants.UPLOAD_REQUEST });
    const success = image     => ({ type: portraitConstants.UPLOAD_SUCCESS, image });
    const failure = error     => ({ type: portraitConstants.UPLOAD_FAILURE, error });
    
    const data = new FormData();
    data.append("file", formValues.image);

    dispatch(request());
    portraitService.upload( data ).then(
        dbPathObject => {
            dispatch(success( dbPathObject ));
            const chainedRequest = _         => ({ type: portraitConstants.CREATE_REQUEST });
            const chainedSuccess = portrait  => ({ type: portraitConstants.CREATE_SUCCESS, portrait });
            const chainedFailure = error     => ({ type: portraitConstants.CREATE_FAILURE, error });

            let portraitData = {
                title       : formValues.title,
                description : formValues.description,
                url         : dbPathObject.dbPath,
            }
            //console.log(jsonData);

            dispatch( chainedRequest() );
            portraitService.create( portraitData ).then(
                portrait => {
                    dispatch( chainedSuccess(portrait) );
                    history.push("/");
                },
                error => {
                    dispatch(chainedFailure(error));
                    dispatch(alertActions.error(error));
                }
            );
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}

export const uploadPortrait = formValues => async (dispatch, getState) => {
    const request = _         => ({ type: portraitConstants.UPLOAD_REQUEST });
    const success = imageUrl  => ({ type: portraitConstants.UPLOAD_SUCCESS, imageUrl });
    const failure = error     => ({ type: portraitConstants.UPLOAD_FAILURE, error });
    
    const data = new FormData();
    data.append("file", formValues.image);

    dispatch(request());
    portraitService.upload( data ).then(
        dbPathObject => {
            dispatch(success( dbPathObject.dbPath ));
            return dbPathObject;
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}

export const updatePortrait = (portraitId, formValues) => async (dispatch, getState) => {

    let imageURL = '';
    if ( formValues.hasOwnProperty("image") ) {
        const request = _         => ({ type: portraitConstants.UPLOAD_REQUEST });
        const success = imageUrl  => ({ type: portraitConstants.UPLOAD_SUCCESS, imageUrl });
        const failure = error     => ({ type: portraitConstants.UPLOAD_FAILURE, error });
        
        const data = new FormData();
        data.append("file", formValues.image);

        dispatch(request());
        portraitService.upload( data ).then(
            dbPathObject => {
                dispatch(success( dbPathObject.dbPath ));
                const chainedRequest = portraitId   => ({ type: portraitConstants.UPDATE_REQUEST, portraitId });
                const chainedSuccess = portrait     => ({ type: portraitConstants.UPDATE_SUCCESS, portrait });
                const chainedFailure = error        => ({ type: portraitConstants.UPDATE_FAILURE, error });

                let portraitData = {
                    title       : formValues.title,
                    description : formValues.description,
                    url         : dbPathObject.dbPath,
                }
                //console.log(jsonData);

                dispatch( chainedRequest() );
                portraitService.update( portraitId, portraitData ).then(
                    portrait => {
                        dispatch( chainedSuccess(portrait) );
                        history.push("/");
                    },
                    error => {
                        dispatch(chainedFailure(error));
                        dispatch(alertActions.error(error));
                    }
                );
            },
            error => {
                dispatch(failure(error));
                dispatch(alertActions.error(error));
            }
        );

    } else {
        const request = portraitId    => ({ type: portraitConstants.UPDATE_REQUEST, portraitId });
        const success = portrait      => ({ type: portraitConstants.UPDATE_SUCCESS, portrait });
        const failure = error         => ({ type: portraitConstants.UPDATE_FAILURE, error });

        imageURL = formValues.url;
        let portrait = { title : formValues.title, description : formValues.description, url : imageURL };

        dispatch( request(portraitId) );
        portraitService.update( portraitId, portrait ).then(
            portrait => {
                dispatch( success(portrait) );
                history.push("/");
            },
            error => {
                dispatch(failure(error));
                dispatch(alertActions.error(error));
            }
        );
    }
}

export const deletePortrait = portraitId => async dispatch => {
    const request = portraitId    => ({ type: portraitConstants.DELETE_REQUEST, portraitId });
    const success = portrait      => ({ type: portraitConstants.DELETE_SUCCESS, portrait });
    const failure = error         => ({ type: portraitConstants.DELETE_FAILURE, error });

    dispatch( request(portraitId) );
    portraitService.remove( portraitId ).then(
        portrait => {
            dispatch( success(portrait) );
            history.push("/");
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}