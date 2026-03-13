import { redirect } from 'next/navigation';

// Root route - redirect straight to /login.
// The dashboard layout will verify auth and redirect back if needed.
export default function RootPage() {
  redirect('/login');
}