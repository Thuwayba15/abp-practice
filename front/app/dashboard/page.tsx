'use client';

import { useAuthState } from '@/providers/auth';

export default function DashboardPage() {
  const { user } = useAuthState();

  return (
    <div>
      <h1 className="mb-2 text-2xl font-semibold text-zinc-900">Dashboard</h1>
      {user ? (
        <p className="text-zinc-600">
          Welcome,{' '}
          <span className="font-medium">
            {user.name} {user.surname}
          </span>{' '}
          &mdash; <span className="text-zinc-400">{user.userName}</span>
        </p>
      ) : (
        <p className="text-zinc-400">Loading user…</p>
      )}
    </div>
  );
}
