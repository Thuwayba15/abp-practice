import axios from 'axios';
import ENV from '@/config/env';
import { storage } from '@/lib/storage';

const apiClient = axios.create({
  baseURL: ENV.API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Attach the Bearer token to every request when one is stored
apiClient.interceptors.request.use((config) => {
  const token = storage.getToken();
  const tenantId = storage.getTenantId();

  config.headers = config.headers ?? {};

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  if (tenantId) {
    // ABP resolves tenant context from this header for API requests.
    (config.headers as Record<string, string>)['Abp-TenantId'] = tenantId;
  }

  return config;
});

export default apiClient;
