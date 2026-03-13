const TOKEN_KEY = 'abp_access_token';
const TENANT_ID_KEY = 'abp_tenant_id';

export const storage = {
  getToken: (): string | null => {
    if (typeof window === 'undefined') return null;
    return localStorage.getItem(TOKEN_KEY);
  },
  setToken: (token: string): void => {
    localStorage.setItem(TOKEN_KEY, token);
  },
  clearToken: (): void => {
    localStorage.removeItem(TOKEN_KEY);
  },
  getTenantId: (): string | null => {
    if (typeof window === 'undefined') return null;
    return localStorage.getItem(TENANT_ID_KEY);
  },
  setTenantId: (tenantId: number | string): void => {
    localStorage.setItem(TENANT_ID_KEY, String(tenantId));
  },
  clearTenantId: (): void => {
    localStorage.removeItem(TENANT_ID_KEY);
  },
};
