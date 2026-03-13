const ENV = {
  API_BASE_URL:
    process.env.NEXT_PUBLIC_API_BASE_URL ?? 'https://localhost:44311',
  TOKEN_ENDPOINT:
    process.env.NEXT_PUBLIC_TOKEN_ENDPOINT ?? '/api/TokenAuth/Authenticate',
  CURRENT_USER_ENDPOINT:
    process.env.NEXT_PUBLIC_CURRENT_USER_ENDPOINT ??
    '/api/services/app/Session/GetCurrentLoginInformations',
  // Verify this route in Swagger — it may differ depending on ABP modules enabled
  REGISTER_ENDPOINT:
    process.env.NEXT_PUBLIC_REGISTER_ENDPOINT ??
    '/api/services/app/Account/Register',
} as const;

export default ENV;
