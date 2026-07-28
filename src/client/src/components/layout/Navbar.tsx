import { useState } from 'react'
import { useActiveSection } from '../../hooks/useActiveSection'
import { useScrollPosition } from '../../hooks/useScrollPosition'

interface NavbarProps {
  initials: string
}

export const NAV_ITEMS = [
  { id: 'home', label: 'Home' },
  { id: 'about', label: 'About' },
  { id: 'experience', label: 'Experience' },
  { id: 'projects', label: 'Projects' },
  { id: 'skills', label: 'Skills' },
  { id: 'credentials', label: 'Credentials' },
  { id: 'contact', label: 'Contact' },
] as const

const NAV_SECTION_IDS = NAV_ITEMS.map((item) => item.id)

const STICKY_THRESHOLD = 50

export function Navbar({ initials }: NavbarProps) {
  const [isMenuOpen, setIsMenuOpen] = useState(false)
  const scrollY = useScrollPosition()
  const activeId = useActiveSection(NAV_SECTION_IDS)
  const isSticky = scrollY >= STICKY_THRESHOLD

  const handleNavClick = () => setIsMenuOpen(false)

  return (
    <header id="topnav" className={`defaultscroll sticky${isSticky ? ' nav-sticky' : ''}`}>
      <div className="container">
        <a className="logo" href="#home">
          <span className="fw-bold text-primary fs-4">{initials}</span>
        </a>

        <div className="menu-extras">
          <div className="menu-item">
            <a
              href="#!"
              className={`navbar-toggle${isMenuOpen ? ' open' : ''}`}
              onClick={(e) => {
                e.preventDefault()
                setIsMenuOpen((open) => !open)
              }}
            >
              <div className="lines">
                <span></span>
                <span></span>
                <span></span>
              </div>
            </a>
          </div>
        </div>

        <div id="navigation" style={{ display: isMenuOpen ? 'block' : undefined }}>
          <ul className="navigation-menu">
            {NAV_ITEMS.map((item) => (
              <li key={item.id} className={activeId === item.id ? 'active' : undefined}>
                <a href={`#${item.id}`} className="sub-menu-item" onClick={handleNavClick}>
                  {item.label}
                </a>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </header>
  )
}
