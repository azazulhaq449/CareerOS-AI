import { useNavigate } from 'react-router-dom'
import { useAuth } from '../../auth/AuthContext'

interface TopbarProps {
  onToggleSidebar: () => void
}

export function Topbar({ onToggleSidebar }: TopbarProps) {
  const { email, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = async () => {
    await logout()
    navigate('/login', { replace: true })
  }

  return (
    <div className="top-header">
      <div className="header-bar d-flex justify-content-between align-items-center">
        <div className="d-flex align-items-center">
          <a
            href="#!"
            id="close-sidebar"
            className="btn btn-icon btn-soft-light"
            onClick={(e) => {
              e.preventDefault()
              onToggleSidebar()
            }}
          >
            <i className="ti ti-menu-2"></i>
          </a>
        </div>

        <ul className="list-unstyled mb-0 d-flex align-items-center">
          <li className="dropdown">
            <button
              className="btn btn-soft-light dropdown-toggle d-flex align-items-center gap-2"
              data-bs-toggle="dropdown"
              type="button"
            >
              <span className="avatar avatar-ex-small rounded-circle bg-primary text-white d-flex align-items-center justify-content-center">
                <i className="ti ti-user"></i>
              </span>
              <span className="d-none d-md-inline">{email}</span>
            </button>
            <div className="dropdown-menu dropdown-menu-end shadow border-0 mt-3 py-3">
              <button className="dropdown-item text-danger" type="button" onClick={handleLogout}>
                <i className="ti ti-logout me-2"></i>Logout
              </button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  )
}
