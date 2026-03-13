// Generic ABP response wrapper (all ABP endpoints return this shape)
export interface AbpResponse<T> {
  result: T;
  success: boolean;
  error: { message: string; details: string } | null;
  unAuthorizedRequest: boolean;
}

// POST /api/TokenAuth/Authenticate — response.result shape
// NOTE: ABP classic serialises these as PascalCase (e.g. "AccessToken", not "accessToken").
// If requests succeed but you can't read the token, log res.data.result and adjust field names.
export interface TokenResult {
  accessToken: string;
  expireInSeconds: number;
  encryptedAccessToken: string;
  userId: number;
}

// GET /api/services/app/Session/GetCurrentLoginInformations — response.result shape
export interface CurrentLoginInfo {
  user: {
    id: number;
    name: string;
    surname: string;
    userName: string;
    emailAddress: string;
  } | null;
  tenant: {
    id: number;
    name: string;
  } | null;
}

// Auth state stored in the React context
export interface AuthState {
  token: string | null;
  user: CurrentLoginInfo['user'] | null;
  isLoading: boolean;
  error: string | null;
}

// Login request body
export interface LoginCredentials {
  userNameOrEmailAddress: string;
  password: string;
  rememberClient: boolean;
}

// Registration request body — verify field names match your backend in Swagger
export interface RegisterPayload {
  name: string;
  surname: string;
  userName: string;
  emailAddress: string;
  password: string;
}
