import type { ExperienceEntry } from '../../types/portfolio'

interface ExperienceProps {
  experience: ExperienceEntry[]
}

export function Experience({ experience }: ExperienceProps) {
  return (
    <section className="section bg-light" id="experience">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-12 text-center">
            <div className="section-title mb-4 pb-2">
              <h4 className="title mb-4">Work Experience</h4>
              <p className="text-muted para-desc mx-auto mb-0">
                8+ years delivering .NET web and enterprise applications across product companies and
                consultancies.
              </p>
            </div>
          </div>
        </div>

        <div className="row">
          <div className="col-12">
            <ol className="timeline">
              {experience.map((entry) => (
                <li key={entry.company} className="timeline-item">
                  <div className="card border-0 shadow rounded p-4">
                    <div className="d-flex flex-wrap justify-content-between align-items-start mb-2">
                      <div>
                        <h5 className="mb-0">{entry.role}</h5>
                        <h6 className="text-primary mb-0">
                          {entry.company} &middot; {entry.location}
                        </h6>
                      </div>
                      <span className="badge bg-soft-primary text-primary rounded-pill mt-2 mt-md-0">
                        {entry.period}
                      </span>
                    </div>
                    <p className="text-muted small fst-italic mb-3">Projects: {entry.projects}</p>
                    <ul className="text-muted mb-0 ps-3">
                      {entry.highlights.map((point, i) => (
                        <li key={i} className="mb-2">
                          {point}
                        </li>
                      ))}
                    </ul>
                  </div>
                </li>
              ))}
            </ol>
          </div>
        </div>
      </div>
    </section>
  )
}
