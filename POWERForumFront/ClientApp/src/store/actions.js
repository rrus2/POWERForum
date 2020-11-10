import { FETCH_USER, CREATE_USER } from './types';

const config = {
    headers: { 'content-type': 'multipart/form-data' }
}

export function fetchUser(loginFD) {
    return function (dispatch) {
        axios.post("https://localhost:44303/api/users", loginFD, config).then(user => dispatch({ type: FETCH_USER, payload: user }));
    }
}