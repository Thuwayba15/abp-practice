'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { useAuthActions, useAuthState } from '@/providers/auth';

export default function LoginPage() {
  const { login } = useAuthActions();
  const { isLoading, error } = useAuthState();
  const router = useRouter();

  const [form, setForm] = useState({
    userNameOrEmailAddress: '',
    password: '',
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await login(form.userNameOrEmailAddress, form.password);
      router.push('/dashboard');
    } catch {
      // error is already set in auth state and displayed below
    }
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-zinc-50">
      <form
        onSubmit={handleSubmit}
        className="w-full max-w-sm rounded-lg bg-white p-8 shadow"
      >
        <h1 className="mb-6 text-2xl font-semibold text-zinc-900">Sign In</h1>

        {error && (
          <p className="mb-4 rounded bg-red-50 px-3 py-2 text-sm text-red-600">
            {error}
          </p>
        )}

        <div className="mb-4">
          <label className="block text-sm font-medium text-zinc-700">
            Username or Email
          </label>
          <input
            type="text"
            required
            autoComplete="username"
            value={form.userNameOrEmailAddress}
            onChange={(e) =>
              setForm({ ...form, userNameOrEmailAddress: e.target.value })
            }
            className="mt-1 w-full rounded border border-zinc-300 px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-zinc-500"
          />
        </div>

        <div className="mb-6">
          <label className="block text-sm font-medium text-zinc-700">
            Password
          </label>
          <input
            type="password"
            required
            autoComplete="current-password"
            value={form.password}
            onChange={(e) => setForm({ ...form, password: e.target.value })}
            className="mt-1 w-full rounded border border-zinc-300 px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-zinc-500"
          />
        </div>

        <button
          type="submit"
          disabled={isLoading}
          className="w-full rounded bg-zinc-900 px-4 py-2 text-sm font-medium text-white hover:bg-zinc-700 disabled:opacity-50"
        >
          {isLoading ? 'Signing in…' : 'Sign In'}
        </button>

        <p className="mt-4 text-center text-sm text-zinc-600">
          Don&apos;t have an account?{' '}
          <a href="/register" className="font-medium text-zinc-900 underline">
            Register
          </a>
        </p>
      </form>
    </div>
  );
}
