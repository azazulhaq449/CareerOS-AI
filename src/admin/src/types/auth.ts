export interface LoginRequest {
  email: string
  password: string
}

export interface LoginResponse {
  accessToken: string
  expiresAtUtc: string
}

export interface CurrentUserResponse {
  email: string
}
