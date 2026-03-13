import { handleActions } from 'redux-actions';
import { initialState } from './context';
import { ActionTypes } from './actions';
import type { AuthState, CurrentLoginInfo } from '@/types/auth';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const reducer = handleActions<AuthState, any>(
  {
    [ActionTypes.SET_LOADING]: (state, action) => ({
      ...state,
      isLoading: action.payload as boolean,
    }),

    [ActionTypes.LOGIN_SUCCESS]: (state, action) => ({
      ...state,
      token: action.payload as string,
      error: null,
    }),

    [ActionTypes.SET_USER]: (state, action) => ({
      ...state,
      user: action.payload as CurrentLoginInfo['user'],
    }),

    [ActionTypes.SET_ERROR]: (state, action) => ({
      ...state,
      error: action.payload as string | null,
      isLoading: false,
    }),

    [ActionTypes.LOGOUT]: () => ({ ...initialState }),
  },
  initialState
);

export default reducer;
