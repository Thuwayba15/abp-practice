import { createContext, useContext } from 'react';
import type { AuthState } from '@/types/auth';

export const initialState: AuthState = {
  token: null,
  user: null,
  isLoading: false,
  error: null,
};

export interface AuthActions {
  login: (userNameOrEmailAddress: string, password: string) => Promise<void>;
  register: (
    name: string,
    surname: string,
    userName: string,
    emailAddress: string,
    password: string
  ) => Promise<void>;
  logout: () => void;
}

export const AuthStateContext = createContext<AuthState>(initialState);
export const AuthActionsContext = createContext<AuthActions | null>(null);

export const useAuthState = (): AuthState => useContext(AuthStateContext);

export const useAuthActions = (): AuthActions => {
  const ctx = useContext(AuthActionsContext);
  if (!ctx) throw new Error('useAuthActions must be used inside AuthProvider');
  return ctx;
};
