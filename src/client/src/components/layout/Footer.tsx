import type { Profile } from '../../types/portfolio'

interface FooterProps {
  profile: Profile
}

export function Footer({ profile }: FooterProps) {
  const year = new Date().getFullYear()

  return (
    <footer className="footer footer-bar">
      <div className="footer-py-30">
        <div className="container text-center">
          <div className="row align-items-center">
            <div className="col-sm-6">
              <div className="text-sm-start">
                <p className="mb-0">
                  &copy; {year} {profile.name}. Built with React &amp; .NET.
                </p>
              </div>
            </div>

            <div className="col-sm-6 mt-4 mt-sm-0 pt-2 pt-sm-0">
              <ul className="list-unstyled social-icon foot-social-icon text-sm-end mb-0">
                <li className="list-inline-item mb-0">
                  <a href={`mailto:${profile.email}`} className="rounded">
                    <i className="uil uil-envelope align-middle" title="Email"></i>
                  </a>
                </li>
                <li className="list-inline-item mb-0">
                  <a href={profile.phoneHref} className="rounded">
                    <i className="uil uil-phone align-middle" title="Phone"></i>
                  </a>
                </li>
                {profile.linkedInUrl && (
                  <li className="list-inline-item mb-0">
                    <a href={profile.linkedInUrl} target="_blank" rel="noreferrer" className="rounded">
                      <i className="uil uil-linkedin align-middle" title="LinkedIn"></i>
                    </a>
                  </li>
                )}
              </ul>
            </div>
          </div>
        </div>
      </div>
    </footer>
  )
}
