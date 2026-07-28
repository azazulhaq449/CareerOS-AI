import type { CredentialsResponse } from '../../types/portfolio'

interface CredentialsProps {
  credentials: CredentialsResponse
}

export function Credentials({ credentials: { certifications, education } }: CredentialsProps) {
  return (
    <section className="section" id="credentials">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-12 text-center">
            <div className="section-title mb-4 pb-2">
              <h4 className="title mb-4">Certifications &amp; Education</h4>
            </div>
          </div>
        </div>

        <div className="row">
          {certifications.map((cert) => (
            <div key={cert.name} className="col-lg-4 col-md-6 mt-4 pt-2">
              <div className="card border-0 shadow rounded h-100">
                <div className="card-body">
                  <div className="icon-circle mb-3">
                    <i className="uil uil-award"></i>
                  </div>
                  <h6 className="mb-1">{cert.name}</h6>
                  <p className="text-muted small mb-0">{cert.date}</p>
                </div>
              </div>
            </div>
          ))}

          <div className="col-lg-4 col-md-6 mt-4 pt-2">
            <div className="card border-0 shadow rounded h-100">
              <div className="card-body">
                <div className="icon-circle mb-3">
                  <i className="uil uil-graduation-cap"></i>
                </div>
                <h6 className="mb-1">{education.degree}</h6>
                <p className="text-muted small mb-0">
                  {education.school}, {education.location}
                  <br />
                  {education.date}
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}
