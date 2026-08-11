import { useState } from 'react'
import { Outlet } from 'react-router-dom'
import { Sidebar } from './Sidebar'
import { Topbar } from './Topbar'

export function AdminLayout() {
  const [sidebarHidden, setSidebarHidden] = useState(false)

  return (
    <div className={`page-wrapper${sidebarHidden ? ' sidebar-hidden' : ''}`}>
      <Sidebar />
      <main className="page-content bg-light">
        <Topbar onToggleSidebar={() => setSidebarHidden((v) => !v)} />
        <div className="container-fluid">
          <div className="layout-specing">
            <Outlet />
          </div>
        </div>
        <footer className="shadow py-3">
          <div className="container-fluid">
            <div className="text-center">
              <p className="mb-0 text-muted">CareerOS Admin</p>
            </div>
          </div>
        </footer>
      </main>
    </div>
  )
}
