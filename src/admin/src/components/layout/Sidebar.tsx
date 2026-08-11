import { NavLink } from 'react-router-dom'

const NAV_ITEMS = [
  { to: '/', label: 'Dashboard', icon: 'ti-home', end: true },
  { to: '/profile', label: 'Profile & Strengths', icon: 'ti-user' },
  { to: '/experience', label: 'Experience', icon: 'ti-briefcase' },
  { to: '/projects', label: 'Projects', icon: 'ti-apps' },
  { to: '/skills', label: 'Skills', icon: 'ti-bulb' },
  { to: '/credentials', label: 'Certifications & Education', icon: 'ti-certificate' },
] as const

export function Sidebar() {
  return (
    <nav id="sidebar" className="sidebar-wrapper sidebar-dark">
      <div className="sidebar-content" data-simplebar style={{ height: 'calc(100% - 60px)' }}>
        <div className="sidebar-brand">
          <span className="fw-bold fs-4 text-primary">AH · Admin</span>
        </div>

        <ul className="sidebar-menu">
          {NAV_ITEMS.map((item) => (
            <li key={item.to}>
              <NavLink to={item.to} end={'end' in item ? item.end : false}>
                <i className={`ti ${item.icon} me-2`}></i>
                {item.label}
              </NavLink>
            </li>
          ))}
        </ul>
      </div>
    </nav>
  )
}
