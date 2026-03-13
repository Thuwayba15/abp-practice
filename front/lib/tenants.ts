import ENV from '@/config/env';
import apiClient from '@/lib/axios';
import type { AbpResponse, AvailableTenant } from '@/types/auth';

export const getActiveTenants = async (): Promise<AvailableTenant[]> => {
  const res = await apiClient.get<AbpResponse<AvailableTenant[]>>(
    ENV.TENANTS_ENDPOINT
  );
  return res.data.result;
};
