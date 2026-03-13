# Copilot Prompt for Building a Next.js Frontend for an Existing ABP Backend

## Context

We already have an **existing ABP backend** based on **ASP.NET Boilerplate / ABP**.

Important project context:

- The **backend already exists** and is already connected to the database.
- We are **not using the default ABP MVC / Razor frontend**.
- We want to build a **separate Next.js frontend** that talks to the ABP backend over HTTP.
- The frontend has **not been started yet**.
- We want to use the **same provider pattern** used in previous projects like the **chat app** and **CRM**:
  - `context.ts`
  - `actions.ts`
  - `reducer.ts`
  - `index.tsx`
- We want the frontend to support:
  - **Login**
  - **Registration**
  - **Persisting auth state**
  - **Fetching current user after login**
  - **Protecting dashboard/private routes**
  - **Logout**

## Goal

Scaffold a **clean Next.js frontend** inside a new `frontend/` folder at the root of the ABP solution, and wire it to the existing ABP backend auth flow.

The frontend should:

1. Authenticate against the ABP backend
2. Use a token-based flow
3. Store the token on the frontend
4. Send the token on API requests
5. Support registration using the backend's registration endpoint
6. Fetch the current authenticated user after login
7. Redirect unauthenticated users away from protected pages

## Folder Placement

Create the frontend as a **separate folder at the root of the solution**, for example:

```text
MyAbpProject/
  src/
  test/
  MyAbpProject.sln
  frontend/
```

Do **not** place the Next.js app inside the ABP backend project folders unless specifically required.

## Tech / Architecture Requirements

Use:

- **Next.js** with **App Router**
- **TypeScript**
- **Axios**
- The existing **4-file provider pattern**
- Clean, modular structure
- Minimal styling for now
- No unnecessary dependencies
- Functional components with arrow functions
- Reusable and maintainable code

## Expected Frontend Structure

Create the following structure:

```text
frontend/
  src/
    app/
      layout.tsx
      page.tsx
      (auth)/
        login/
          page.tsx
        register/
          page.tsx
      (dashboard)/
        layout.tsx
        page.tsx
    providers/
      auth/
        actions.ts
        context.ts
        reducer.ts
        index.tsx
    lib/
      axios.ts
      storage.ts
    config/
      env.ts
    types/
      auth.ts
```

## Auth Requirements

Implement the auth flow like this:

### Login
- Submit username/email and password to the backend token endpoint
- Save the returned access token
- Attach the token to future requests
- Fetch the current user after successful login
- Redirect to `/dashboard`

### Register
- Submit the registration form to the backend registration endpoint
- If registration succeeds, auto-login the user if possible
- Redirect to `/dashboard`

### Current User
- Fetch the currently authenticated user from the backend after login or app refresh

### Logout
- Clear local auth storage
- Reset auth state
- Redirect to `/login`

### Hydration
- On app load, if a token already exists, restore the auth session and fetch the user

## Important Backend Notes

We are connecting to an **existing ABP backend**, so do not invent a fake backend.

Use environment variables for backend URLs.

The implementation should be flexible because the exact ABP endpoints may differ slightly depending on the template/modules. Where the exact endpoint might vary, add a clear comment telling us to confirm it in Swagger.

Assume these likely backend values, but keep them configurable:

- `NEXT_PUBLIC_API_BASE_URL`
- `NEXT_PUBLIC_TOKEN_ENDPOINT`
- `NEXT_PUBLIC_APP_CONFIG_ENDPOINT`

For registration, include a clear placeholder route and a comment saying to replace it with the actual registration endpoint from Swagger if needed.

## Files to Generate

Please generate the full contents for these files:

### Config
- `src/config/env.ts`

### Types
- `src/types/auth.ts`

### Lib
- `src/lib/storage.ts`
- `src/lib/axios.ts`

### Auth Provider
- `src/providers/auth/context.ts`
- `src/providers/auth/actions.ts`
- `src/providers/auth/reducer.ts`
- `src/providers/auth/index.tsx`

### App Router
- `src/app/layout.tsx`
- `src/app/page.tsx`
- `src/app/(auth)/login/page.tsx`
- `src/app/(auth)/register/page.tsx`
- `src/app/(dashboard)/layout.tsx`
- `src/app/(dashboard)/page.tsx`

Also provide a sample `.env.local` file.

## Provider Pattern Rules

Follow this provider pattern closely:

### `context.ts`
Contains:
- state interfaces
- action context interfaces
- initial state
- context creation

### `actions.ts`
Contains:
- enum of action types
- `createAction` action creators

### `reducer.ts`
Contains:
- reducer using `handleActions`

### `index.tsx`
Contains:
- provider component
- async auth methods
- hooks:
  - `useAuthState()`
  - `useAuthActions()`

## Coding Expectations

Please make the code:

- Type-safe
- Easy to understand
- Ready to paste into the project
- Minimal but working
- Structured for future expansion

Include comments where needed, especially around:
- token endpoint setup
- possible `client_id` / `scope` requirements
- registration endpoint replacement if needed
- current-user endpoint usage

## Output Format

Return the answer as:

1. A short explanation of what is being built
2. The full folder structure
3. The full contents of every required file
4. A short “How to run” section
5. A short “What to verify in Swagger” section

---

# How I actually connect the frontend to the ABP backend

Use this checklist after the frontend is scaffolded.

## 1. Create the frontend app

From the root of the ABP solution:

```bash
npx create-next-app@latest frontend
```

Recommended choices:
- TypeScript: Yes
- App Router: Yes
- ESLint: Yes
- `src/` directory: Yes

Then install dependencies:

```bash
cd frontend
npm install axios redux-actions
```

## 2. Set the backend URL in `.env.local`

Example:

```env
NEXT_PUBLIC_API_BASE_URL=https://localhost:44395
NEXT_PUBLIC_TOKEN_ENDPOINT=/connect/token
NEXT_PUBLIC_APP_CONFIG_ENDPOINT=/api/abp/application-configuration
```

Replace the base URL with the actual ABP backend host URL.

## 3. Make sure the backend is running

Before testing login/register, start the ABP backend and confirm:

- Swagger opens
- the API base URL is reachable
- the auth/token endpoint responds
- registration endpoint exists

## 4. Check Swagger before finalizing registration

In Swagger, confirm:

- token/auth endpoint
- registration endpoint
- current user or application configuration endpoint

The registration endpoint is the one most likely to differ depending on the ABP setup, so do not assume it blindly.

## 5. Handle CORS on the ABP backend

If your frontend runs on something like:

```text
http://localhost:3000
```

your ABP backend must allow that origin.

Without CORS configured correctly, login and registration requests may fail even if your code is correct.

So make sure the backend allows the frontend origin.

## 6. Test login first

This should be your first successful milestone:

- open `/login`
- enter a valid backend user
- submit login
- receive token
- store token
- fetch current user
- land on `/dashboard`

If login fails:
- inspect browser network tab
- inspect backend logs
- check endpoint URL
- check request body format
- check whether `client_id` or `scope` is required

## 7. Then test registration

Once login works:

- open `/register`
- submit a new user
- confirm the backend actually creates the account
- if registration succeeds, auto-login and redirect to dashboard

If register fails:
- confirm the real registration route in Swagger
- inspect validation errors in network tab
- check whether ABP expects different field names

## 8. Check if `/connect/token` needs extra parameters

Some ABP setups require additional form fields such as:

- `client_id`
- `scope`

If your token request fails with a backend auth/client error, inspect Swagger/docs/backend config and add them in the login request.

## 9. Verify current user fetch

After login, make sure the frontend can successfully fetch current user information and keep the session alive across refreshes.

## 10. Build the rest of the frontend only after auth works

Do not start building the whole application before:
- login works
- registration works
- auth state persists
- protected routes work

---

# What I need to inspect on the backend if something does not work

If auth does not work, check these backend items:

1. Backend base URL
2. Swagger auth endpoints
3. Registration endpoint path
4. CORS configuration
5. Whether HTTPS is required
6. Whether `/connect/token` needs `client_id`
7. Whether the app config/current-user endpoint is correct
8. Whether the backend returns standard ABP/OpenIddict token response fields

---

# Extra instruction to Copilot

When generating the code, do not give partial snippets only. Generate complete files that I can paste directly into the project.

Also do not replace the provider pattern with Zustand, Redux Toolkit, NextAuth, or any different architecture. Stay aligned with the existing 4-file provider pattern described above.