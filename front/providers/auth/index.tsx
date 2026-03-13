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

const toTenantHeaders = (tenantId?: number | null): Record<string, string> => {
  if (!tenantId || tenantId <= 0) {
    return {};
  }

  return {
    'Abp-TenantId': String(tenantId),
  };
};

const requireEndpoint = (
  endpoint: string | undefined,
  envName: string
): string => {
  if (!endpoint) {
    throw new Error(`Missing required endpoint configuration: ${envName}`);
  }

  return endpoint;
};

const fetchCurrentUser = async (): Promise<CurrentLoginInfo['user']> => {
  const res = await apiClient.get<AbpResponse<CurrentLoginInfo>>(
    requireEndpoint(
      ENV.CURRENT_USER_ENDPOINT,
      'NEXT_PUBLIC_CURRENT_USER_ENDPOINT'
    )
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
    async (
      userNameOrEmailAddress: string,
      password: string,
      tenantId?: number | null
    ) => {
      dispatch(setLoading(true));
      dispatch(setError(null));
      try {
        if (tenantId && tenantId > 0) {
          storage.setTenantId(tenantId);
        } else if (tenantId === null) {
          storage.clearTenantId();
        }

        // POST /api/TokenAuth/Authenticate
        // If this fails with a client/scope error, check Swagger for required extra fields
        // (some ABP setups need client_id / scope in the body)
        const res = await apiClient.post<AbpResponse<TokenResult>>(
          requireEndpoint(ENV.TOKEN_ENDPOINT, 'NEXT_PUBLIC_TOKEN_ENDPOINT'),
          { userNameOrEmailAddress, password, rememberClient: true },
          { headers: toTenantHeaders(tenantId) }
        );

        // NOTE: ABP classic may return the field as "accessToken" or "AccessToken".
        // If token is undefined, log res.data.result and adjust the field name below.
        const token =
          res.data.result.accessToken ??
          (res.data.result as any).AccessToken; // eslint-disable-line @typescript-eslint/no-explicit-any

        if (!token || typeof token !== 'string') {
          throw new Error('Authentication response did not include a valid access token.');
        }

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
      password: string,
      tenantId: number
    ) => {
      dispatch(setLoading(true));
      dispatch(setError(null));
      try {
        if (!tenantId || Number.isNaN(tenantId)) {
          throw new Error('Please select a tenant before registering.');
        }

        storage.setTenantId(tenantId);

        // POST /api/services/app/Account/Register
        // Verify this route in Swagger — it is the one most likely to differ
        await apiClient.post(
          requireEndpoint(
            ENV.REGISTER_ENDPOINT,
            'NEXT_PUBLIC_REGISTER_ENDPOINT'
          ),
          {
            name,
            surname,
            userName,
            emailAddress,
            password,
          },
          {
            headers: toTenantHeaders(tenantId),
          }
        );

        // Auto-login after successful registration
        await login(userName, password, tenantId);
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
