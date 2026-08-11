# Postman Guide — CareerOS API

This guide covers getting a JWT from the API and using it to call the
admin (write) endpoints directly, outside the admin panel UI.

## 1. Import the collection

Import `docs/CareerOS.postman_collection.json` into Postman (**Import** →
select the file). It ships with:

- A collection variable `baseUrl` set to `http://localhost:5257` — update
  this if your API runs elsewhere.
- A collection variable `accessToken`, left blank — the **Login** request
  fills it in automatically (see below).
- One folder per resource, each with example requests for every mutating
  endpoint.

## 2. Get a token

Run **Auth → Login** with this JSON body:

```json
{
  "email": "your-admin-email",
  "password": "your-admin-password"
}
```

The response looks like:

```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAtUtc": "2026-08-09T05:38:48.35Z"
}
```

The **Login** request already has a **Tests** script attached:

```javascript
const body = pm.response.json();
pm.collectionVariables.set('accessToken', body.accessToken);
```

This automatically saves the token into the `accessToken` collection
variable — you don't need to copy/paste it anywhere. Every other request in
the collection is pre-configured to send it as `Authorization: Bearer
{{accessToken}}` (Postman's **Bearer Token** auth type, inherited from the
collection). Just run Login once per session, then everything else works.

Tokens are valid for **8 hours**. When one expires, requests start
returning `401 Unauthorized` — just re-run Login.

## 3. Calling protected endpoints manually (without the collection)

If you're building a request from scratch instead of using the collection:

1. Go to the request's **Authorization** tab.
2. Type: **Bearer Token**.
3. Token: paste the `accessToken` value from the Login response.

Or set the header directly: `Authorization: Bearer <token>`.

## 4. Public vs. protected endpoints

All `GET` endpoints are public (no token needed) — they're what the
portfolio website itself calls. Only `POST`, `PUT` and `DELETE` endpoints
require the Bearer token:

| Resource | Public (no auth) | Protected (Bearer token) |
|---|---|---|
| Profile | `GET /api/profile` | `PUT /api/profile` |
| Strengths | `GET /api/profile/strengths` | `POST` / `PUT /{id}` / `DELETE /{id}` under `/api/profile/strengths` |
| Experience | `GET /api/experience`, `GET /api/experience/{id}` | `POST` / `PUT /{id}` / `DELETE /{id}` under `/api/experience` |
| Projects | `GET /api/projects`, `GET /api/projects/{id}` | `POST` / `PUT /{id}` / `DELETE /{id}` under `/api/projects` |
| Skills | `GET /api/skills`, `GET /api/skills/{id}` | `POST` / `PUT /{id}` / `DELETE /{id}` under `/api/skills` |
| Credentials | `GET /api/credentials` | `POST /api/credentials/certifications`, `PUT`/`DELETE /{id}`, `PUT /api/credentials/education` |
| Auth | — | `GET /api/auth/me`, `POST /api/auth/logout` |

## 5. Example request bodies

**Create an experience entry** — `POST {{baseUrl}}/api/experience`:

```json
{
  "company": "Example Ltd",
  "location": "Remote",
  "role": "Senior Engineer",
  "period": "2026 – Present",
  "projects": "Project A, Project B",
  "highlights": ["Did a thing", "Did another thing"],
  "sortOrder": 0
}
```

**Update a project** — `PUT {{baseUrl}}/api/projects/{id}`:

```json
{
  "name": "Example Project",
  "organisation": "Example Ltd",
  "icon": "uil-star",
  "description": "What it does.",
  "tags": ["React", ".NET"],
  "sortOrder": 0
}
```

All resource `id` values are GUIDs, visible in each `GET` response.

## 6. Also documented in Swagger

Every endpoint (including request/response shapes) is also browsable at
`{{baseUrl}}/swagger` while the API is running in Development — useful for
checking exact field names without leaving the browser.
