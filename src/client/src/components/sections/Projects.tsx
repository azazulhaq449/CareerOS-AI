import type { ProjectEntry } from '../../types/portfolio'

interface ProjectsProps {
  projects: ProjectEntry[]
}

export function Projects({ projects }: ProjectsProps) {
  return (
    <section className="section" id="projects">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-12 text-center">
            <div className="section-title mb-4 pb-2">
              <h4 className="title mb-4">Projects</h4>
              <p className="text-muted para-desc mx-auto mb-0">
                A selection of platforms and products built across product companies, consultancies and
                freelance engagements.
              </p>
            </div>
          </div>
        </div>

        <div className="row">
          {projects.map((project) => (
            <div key={project.name} className="col-lg-4 col-md-6 mt-4 pt-2">
              <div className="card project-card border-0 shadow rounded h-100">
                <div className="card-body d-flex flex-column">
                  <div className="icon-circle mb-3">
                    <i className={`uil ${project.icon}`}></i>
                  </div>
                  <h5 className="mb-1">{project.name}</h5>
                  <h6 className="text-primary small text-uppercase mb-3">{project.organisation}</h6>
                  <p className="text-muted flex-grow-1">{project.description}</p>
                  <ul className="list-unstyled tag-list mb-0">
                    {project.tags.map((tag) => (
                      <li key={tag} className="badge bg-soft-primary text-primary me-1 mb-1">
                        {tag}
                      </li>
                    ))}
                  </ul>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  )
}
