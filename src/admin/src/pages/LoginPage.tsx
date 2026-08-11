import { useState, type FormEvent } from 'react'
import { Navigate } from 'react-router-dom'
import { useAuth } from '../auth/AuthContext'

export function LoginPage() {
  const { isAuthenticated, login } = useAuth()
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState<string | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)

  if (isAuthenticated) {
    return <Navigate to="/" replace />
  }

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault()
    setError(null)
    setIsSubmitting(true)
    try {
      await login(email, password)
    } catch {
      setError('Invalid email or password.')
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <section className="bg-home bg-circle-gradiant d-flex align-items-center" style={{ minHeight: '100vh' }}>
      <div className="bg-overlay bg-overlay-white"></div>
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-lg-4 col-md-6 col-12">
            <div className="card form-signin p-4 rounded shadow">
              <form onSubmit={handleSubmit}>
                <span className="d-block text-center mb-4 fw-bold fs-3 text-primary">AH</span>
                <h5 className="mb-3 text-center">CareerOS Admin — Please sign in</h5>

                {error && <div className="alert alert-danger py-2">{error}</div>}

                <div className="form-floating mb-2">
                  <input
                    type="email"
                    className="form-control"
                    id="floatingInput"
                    placeholder="name@example.com"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                  />
                  <label htmlFor="floatingInput">Email address</label>
                </div>
                <div className="form-floating mb-3">
                  <input
                    type="password"
                    className="form-control"
                    id="floatingPassword"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                  />
                  <label htmlFor="floatingPassword">Password</label>
                </div>

                <button className="btn btn-primary w-100" type="submit" disabled={isSubmitting}>
                  {isSubmitting ? 'Signing in…' : 'Sign in'}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}
