import { createContext, useContext, useEffect, useState, type ReactNode } from 'react'
import { authApi } from '../api/authApi'
import { UNAUTHORIZED_EVENT } from '../api/client'

interface AuthState {
  isAuthenticated: boolean | null // null = still checking the existing session
  email: string | null
  login: (email: string, password: string) => Promise<void>
  logout: () => Promise<void>
}

const AuthContext = createContext<AuthState | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean | null>(null)
  const [email, setEmail] = useState<string | null>(null)

  useEffect(() => {
    authApi
      .me()
      .then((user) => {
        setEmail(user.email)
        setIsAuthenticated(true)
      })
      .catch(() => setIsAuthenticated(false))
  }, [])

  useEffect(() => {
    const handleUnauthorized = () => {
      setIsAuthenticated(false)
      setEmail(null)
    }
    window.addEventListener(UNAUTHORIZED_EVENT, handleUnauthorized)
    return () => window.removeEventListener(UNAUTHORIZED_EVENT, handleUnauthorized)
  }, [])

  const login = async (loginEmail: string, password: string) => {
    await authApi.login({ email: loginEmail, password })
    setEmail(loginEmail)
    setIsAuthenticated(true)
  }

  const logout = async () => {
    await authApi.logout()
    setIsAuthenticated(false)
    setEmail(null)
  }

  return <AuthContext.Provider value={{ isAuthenticated, email, login, logout }}>{children}</AuthContext.Provider>
}

export function useAuth(): AuthState {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }
  return context
}
