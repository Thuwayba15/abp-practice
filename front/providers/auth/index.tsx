'use client';

import React, { useReducer, useEffect, useCallback } from 'react';
import { AuthStateContext, AuthActionsContext, initialState } from './context';
import type { AuthActions } from './context';
import {
  setLoading,
  loginSuccess,
  setUser,
  setError,
  logout as logoutAction,
} from './actions';
import reducer from './reducer';
import { storage } from '@/lib/storage';
import apiClient from '@/lib/axios';
import ENV from '@/config/env';
import type { AbpResponse, TokenResult, CurrentLoginInfo } from '@/types/auth';

// Re-export hooks so consumers only import from '@/providers/auth'
export { useAuthState, useAuthActions } from './context';

const fetchCurrentUser = async (): Promise<CurrentLoginInfo['user']> => {
  const res = await apiClient.get<AbpResponse<CurrentLoginInfo>>(
    ENV.CURRENT_USER_ENDPOINT
  );
  return res.data.result.user;
};

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [state, dispatch] = useReducer(
    reducer as React.Reducer<typeof initialState, any>, // eslint-disable-line @typescript-eslint/no-explicit-any
    initialState
  );

  // Hydrate auth session from localStorage on first mount
  useEffect(() => {
    const token = storage.getToken();
    if (token) {
      dispatch(loginSuccess(token));
      fetchCurrentUser()
        .then((user) => dispatch(setUser(user)))
        .catch(() => {
          // Token is stale — clear it so user is redirected to login
          storage.clearToken();
          dispatch(logoutAction());
        });
    }
  }, []);

  const login = useCallback(
    async (userNameOrEmailAddress: string, password: string) => {
      dispatch(setLoading(true));
      dispatch(setError(null));
      try {
        // POST /api/TokenAuth/Authenticate
        // If this fails with a client/scope error, check Swagger for required extra fields
        // (some ABP setups need client_id / scope in the body)
        const res = await apiClient.post<AbpResponse<TokenResult>>(
          ENV.TOKEN_ENDPOINT,
          { userNameOrEmailAddress, password, rememberClient: true }
        );

        // NOTE: ABP classic may return the field as "accessToken" or "AccessToken".
        // If token is undefined, log res.data.result and adjust the field name below.
        const token =
          res.data.result.accessToken ??
          (res.data.result as any).AccessToken; // eslint-disable-line @typescript-eslint/no-explicit-any

        storage.setToken(token);
        dispatch(loginSuccess(token));

        const user = await fetchCurrentUser();
        dispatch(setUser(user));
      } catch (err: any) { // eslint-disable-line @typescript-eslint/no-explicit-any
        const msg =
          err?.response?.data?.error?.message ??
          err?.message ??
          'Login failed';
        dispatch(setError(msg));
        throw err;
      } finally {
        dispatch(setLoading(false));
      }
    },
    []
  );

  const register = useCallback(
    async (
      name: string,
      surname: string,
      userName: string,
      emailAddress: string,
      password: string
    ) => {
      dispatch(setLoading(true));
      dispatch(setError(null));
      try {
        // POST /api/services/app/Account/Register
        // Verify this route in Swagger — it is the one most likely to differ
        await apiClient.post(ENV.REGISTER_ENDPOINT, {
          name,
          surname,
          userName,
          emailAddress,
          password,
        });

        // Auto-login after successful registration
        await login(userName, password);
      } catch (err: any) { // eslint-disable-line @typescript-eslint/no-explicit-any
        const msg =
          err?.response?.data?.error?.message ??
          err?.message ??
          'Registration failed';
        dispatch(setError(msg));
        throw err;
      } finally {
        dispatch(setLoading(false));
      }
    },
    [login]
  );

  const logoutFn = useCallback(() => {
    storage.clearToken();
    dispatch(logoutAction());
  }, []);

  const actions: AuthActions = { login, register, logout: logoutFn };

  return (
    <AuthStateContext.Provider value={state}>
      <AuthActionsContext.Provider value={actions}>
        {children}
      </AuthActionsContext.Provider>
    </AuthStateContext.Provider>
  );
};
