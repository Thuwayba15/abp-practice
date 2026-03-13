'use client';

import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { useAuthState, useAuthActions } from '@/providers/auth';

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { token } = useAuthState();
  const { logout } = useAuthActions();
  const router = useRouter();

  // Redirect to login whenever the token is gone (logout or stale token cleared on hydration)
  useEffect(() => {
    if (token === null) {
      router.replace('/login');
    }
  }, [token, router]);

  // Render nothing while we decide — avoids a flash of protected content
  if (!token) return null;

  const handleLogout = () => {
    logout();
    router.push('/login');
  };

  return (
    <div className="min-h-screen bg-zinc-50">
      <header className="flex items-center justify-between border-b border-zinc-200 bg-white px-6 py-4">
        <span className="font-semibold text-zinc-900">ABP Practice</span>
        <button
          onClick={handleLogout}
          className="text-sm text-zinc-500 underline hover:text-zinc-900"
        >
          Sign Out
        </button>
      </header>
      <main className="p-6">{children}</main>
    </div>
  );
}
