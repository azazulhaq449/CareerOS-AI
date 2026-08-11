import { apiClient } from './client'
import type { CurrentUserResponse, LoginRequest, LoginResponse } from '../types/auth'

export const authApi = {
  login: (request: LoginRequest) => apiClient.post<LoginResponse>('/api/auth/login', request),
  logout: () => apiClient.post<void>('/api/auth/logout', undefined),
  me: () => apiClient.get<CurrentUserResponse>('/api/auth/me'),
}
