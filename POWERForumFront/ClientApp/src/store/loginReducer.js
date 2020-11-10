import { FETCH_USER, CREATE_USER } from "./types"

const initialState = {
    user: {}
}

export default function (state = initialState, action) {
    switch (action.type) {
        case FETCH_USER:
            return {
                ...state,
                user: action.payload
            };
        default:
            return state;
    }
}