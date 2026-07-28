import type { Profile, Strength } from '../../types/portfolio'

interface AboutProps {
  profile: Profile
  strengths: Strength[]
}

export function About({ profile, strengths }: AboutProps) {
  return (
    <section className="section" id="about">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-lg-8 text-center">
            <div className="section-title mb-4 pb-2">
              <h4 className="title mb-4">About Me</h4>
              {profile.summary.map((paragraph, i) => (
                <p key={i} className="text-muted para-desc mx-auto mb-3">
                  {paragraph}
                </p>
              ))}
            </div>
          </div>
        </div>

        <div className="row">
          {strengths.map((strength) => (
            <div key={strength.title} className="col-md-4 mt-4 pt-2">
              <div className="card work-process border-0 rounded shadow h-100">
                <div className="card-body">
                  <div className="icon-circle mb-3">
                    <i className={`uil ${strength.icon}`}></i>
                  </div>
                  <h5 className="title">{strength.title}</h5>
                  <p className="text-muted para mb-0">{strength.description}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  )
}
