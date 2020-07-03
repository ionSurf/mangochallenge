import { authHeader } from "../helpers";
import { authService } from '.';

const baseURL = process.env.REACT_APP_API_URL; //"https://localhost:5001/api/";
const portraitURI = "api/portrait/";

const getAll = (_) => {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };

  return fetch(`${baseURL + portraitURI}`, requestOptions).then(handleResponse);
};

const getById = id => {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };

  return fetch(`${baseURL + portraitURI}${id}`, requestOptions).then(
    handleResponse
  );
};

const create = portrait => {
  let auth = authHeader();
  const requestOptions = {
    method: "POST",
    headers: {...auth,"Content-Type": "application/json",},
    body: JSON.stringify(portrait),
  }; //console.log(requestOptions)

  return fetch(`${baseURL + portraitURI}`, requestOptions).then(handleResponse);
};

const update = (id, portrait) => {
  let auth = authHeader();
  const requestOptions = {
    method: "PUT",
    headers: {...auth,"Content-Type": "application/json",},
    body: JSON.stringify(portrait),
  };

  return fetch(`${baseURL + portraitURI}${id}`, requestOptions).then(handleResponse);
};

const remove = id => {
    const requestOptions = {
      method: "DELETE",
      headers: authHeader(),
    };
  
    return fetch(`${baseURL + portraitURI}${id}`, requestOptions).then(handleResponse);
  };

const upload = data => {
  const requestOptions = {
    method: "POST",
    headers: authHeader(),
    body: data
  };
  return fetch(`${baseURL}api/upload`, requestOptions).then(handleResponse);
}

const handleResponse = response => {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        authService.logout();
        window.location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
};

export const portraitService = {
    getAll,
    getById,
    create,
    update,
    remove,
    upload
  };