'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { useAuthActions, useAuthState } from '@/providers/auth';

export default function RegisterPage() {
  const { register } = useAuthActions();
  const { isLoading, error } = useAuthState();
  const router = useRouter();

  const [form, setForm] = useState({
    name: '',
    surname: '',
    userName: '',
    emailAddress: '',
    password: '',
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      // Verify POST /api/services/app/Account/Register in Swagger if this fails.
      // Some ABP setups have registration disabled or under a different route.
      await register(
        form.name,
        form.surname,
        form.userName,
        form.emailAddress,
        form.password
      );
      router.push('/dashboard');
    } catch {
      // error is already set in auth state and displayed below
    }
  };

  const field = (
    key: keyof typeof form,
    label: string,
    type = 'text',
    autoComplete = ''
  ) => (
    <div className="mb-4">
      <label className="block text-sm font-medium text-zinc-700">{label}</label>
      <input
        type={type}
        required
        autoComplete={autoComplete || undefined}
        value={form[key]}
        onChange={(e) => setForm({ ...form, [key]: e.target.value })}
        className="mt-1 w-full rounded border border-zinc-300 px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-zinc-500"
      />
    </div>
  );

  return (
    <div className="flex min-h-screen items-center justify-center bg-zinc-50">
      <form
        onSubmit={handleSubmit}
        className="w-full max-w-sm rounded-lg bg-white p-8 shadow"
      >
        <h1 className="mb-6 text-2xl font-semibold text-zinc-900">
          Create Account
        </h1>

        {error && (
          <p className="mb-4 rounded bg-red-50 px-3 py-2 text-sm text-red-600">
            {error}
          </p>
        )}

        {field('name', 'First Name')}
        {field('surname', 'Last Name')}
        {field('userName', 'Username', 'text', 'username')}
        {field('emailAddress', 'Email', 'email', 'email')}
        {field('password', 'Password', 'password', 'new-password')}

        <button
          type="submit"
          disabled={isLoading}
          className="w-full rounded bg-zinc-900 px-4 py-2 text-sm font-medium text-white hover:bg-zinc-700 disabled:opacity-50"
        >
          {isLoading ? 'Creating account…' : 'Register'}
        </button>

        <p className="mt-4 text-center text-sm text-zinc-600">
          Already have an account?{' '}
          <a href="/login" className="font-medium text-zinc-900 underline">
            Sign In
          </a>
        </p>
      </form>
    </div>
  );
}
