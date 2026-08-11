import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { portfolioApi } from '../api/portfolioApi'

interface Stat {
  label: string
  icon: string
  to: string
  count: number | null
}

export function DashboardHomePage() {
  const [stats, setStats] = useState<Stat[]>([
    { label: 'Experience Entries', icon: 'uil-briefcase-alt', to: '/experience', count: null },
    { label: 'Projects', icon: 'uil-apps', to: '/projects', count: null },
    { label: 'Skill Groups', icon: 'uil-bulb', to: '/skills', count: null },
    { label: 'Certifications', icon: 'uil-award', to: '/credentials', count: null },
  ])

  useEffect(() => {
    Promise.all([
      portfolioApi.getExperience(),
      portfolioApi.getProjects(),
      portfolioApi.getSkills(),
      portfolioApi.getCredentials(),
    ]).then(([experience, projects, skills, credentials]) => {
      setStats([
        { label: 'Experience Entries', icon: 'uil-briefcase-alt', to: '/experience', count: experience.length },
        { label: 'Projects', icon: 'uil-apps', to: '/projects', count: projects.length },
        { label: 'Skill Groups', icon: 'uil-bulb', to: '/skills', count: skills.length },
        { label: 'Certifications', icon: 'uil-award', to: '/credentials', count: credentials.certifications.length },
      ])
    })
  }, [])

  return (
    <>
      <h5 className="mb-4">Dashboard</h5>
      <div className="row row-cols-xl-4 row-cols-md-2 row-cols-1">
        {stats.map((stat) => (
          <div className="col mt-4" key={stat.label}>
            <Link
              to={stat.to}
              className="features feature-primary d-flex align-items-center rounded shadow p-3 text-decoration-none"
            >
              <div className="icon text-center rounded-pill bg-soft-primary flex-shrink-0">
                <i className={`uil ${stat.icon} fs-4 mb-0`}></i>
              </div>
              <div className="flex-1 ms-3">
                <h6 className="mb-0 text-muted">{stat.label}</h6>
                <p className="fs-5 text-dark fw-bold mb-0">{stat.count ?? '…'}</p>
              </div>
            </Link>
          </div>
        ))}
      </div>

      <div className="row mt-4">
        <div className="col-12">
          <div className="card border-0 shadow rounded p-4">
            <h6 className="mb-2">Welcome</h6>
            <p className="text-muted mb-0">
              Use the sidebar to manage your profile, work experience, projects, skills, and credentials.
              Changes here update the public portfolio site immediately.
            </p>
          </div>
        </div>
      </div>
    </>
  )
}
