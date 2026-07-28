import type { Profile } from '../../types/portfolio'

interface ContactProps {
  profile: Profile
}

export function Contact({ profile }: ContactProps) {
  return (
    <section className="section bg-light" id="contact">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-lg-7 text-center">
            <div className="section-title mb-4 pb-2">
              <h4 className="title mb-4">Let's Talk</h4>
              <p className="text-muted para-desc mx-auto mb-0">
                Open to senior .NET / cloud engineering roles and freelance projects. Reach out directly —
                no forms, no middlemen.
              </p>
            </div>
          </div>
        </div>

        <div className="row justify-content-center">
          <div className="col-lg-4 col-md-6 mt-4 pt-2">
            <a href={`mailto:${profile.email}`} className="card contact-card border-0 shadow rounded text-center d-block">
              <div className="card-body">
                <div className="icon-circle mb-3">
                  <i className="uil uil-envelope"></i>
                </div>
                <h6 className="mb-1">Email</h6>
                <p className="text-muted small mb-0">{profile.email}</p>
              </div>
            </a>
          </div>

          <div className="col-lg-4 col-md-6 mt-4 pt-2">
            <a href={profile.phoneHref} className="card contact-card border-0 shadow rounded text-center d-block">
              <div className="card-body">
                <div className="icon-circle mb-3">
                  <i className="uil uil-phone"></i>
                </div>
                <h6 className="mb-1">Phone</h6>
                <p className="text-muted small mb-0">{profile.phone}</p>
              </div>
            </a>
          </div>

          <div className="col-lg-4 col-md-6 mt-4 pt-2">
            <div className="card contact-card border-0 shadow rounded text-center h-100">
              <div className="card-body">
                <div className="icon-circle mb-3">
                  <i className="uil uil-map-marker"></i>
                </div>
                <h6 className="mb-1">Location</h6>
                <p className="text-muted small mb-0">{profile.location}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}
