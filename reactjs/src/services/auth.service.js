const login = (UserName, Password) => {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ UserName, Password }),
  };

  //return fetch(`${config.apiUrl}/users/authenticate`, requestOptions)
  let authURL = process.env.REACT_APP_API_URL; //"https://localhost:5001/";
  return fetch(`${authURL}api/auth/login`, requestOptions)
    .then(handleResponse)
    .then((user) => {
      console.log(user);
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.setItem("user", JSON.stringify(user));

      return user;
    });
}

const logout = () => {
  // remove user from local storage to log user out
  localStorage.removeItem("user");
}

function handleResponse(response) {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        window.location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}

export const authService = {
  login,
  logout
};