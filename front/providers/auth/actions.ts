import { createAction } from 'redux-actions';
import type { CurrentLoginInfo } from '@/types/auth';

export enum ActionTypes {
  SET_LOADING = 'auth/SET_LOADING',
  LOGIN_SUCCESS = 'auth/LOGIN_SUCCESS',
  SET_USER = 'auth/SET_USER',
  SET_ERROR = 'auth/SET_ERROR',
  LOGOUT = 'auth/LOGOUT',
}

export const setLoading = createAction<boolean>(ActionTypes.SET_LOADING);
export const loginSuccess = createAction<string>(ActionTypes.LOGIN_SUCCESS);
export const setUser = createAction<CurrentLoginInfo['user']>(ActionTypes.SET_USER);
export const setError = createAction<string | null>(ActionTypes.SET_ERROR);
export const logout = createAction(ActionTypes.LOGOUT);
