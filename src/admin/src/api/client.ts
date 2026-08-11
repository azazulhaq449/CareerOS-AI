const API_BASE_URL = import.meta.env.VITE_API_BASE_URL as string

export const UNAUTHORIZED_EVENT = 'careeros:unauthorized'

interface RequestOptions {
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE'
  body?: unknown
}

async function request<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: options.method ?? 'GET',
    credentials: 'include',
    headers: options.body ? { 'Content-Type': 'application/json' } : undefined,
    body: options.body ? JSON.stringify(options.body) : undefined,
  })

  if (response.status === 401) {
    window.dispatchEvent(new CustomEvent(UNAUTHORIZED_EVENT))
    throw new Error('Unauthorized')
  }

  if (!response.ok) {
    const detail = await response.text().catch(() => '')
    throw new Error(`Request to ${path} failed with status ${response.status}${detail ? `: ${detail}` : ''}`)
  }

  if (response.status === 204) {
    return undefined as T
  }

  return (await response.json()) as T
}

export const apiClient = {
  get: <T>(path: string) => request<T>(path),
  post: <T>(path: string, body: unknown) => request<T>(path, { method: 'POST', body }),
  put: <T>(path: string, body: unknown) => request<T>(path, { method: 'PUT', body }),
  delete: (path: string) => request<void>(path, { method: 'DELETE' }),
}
