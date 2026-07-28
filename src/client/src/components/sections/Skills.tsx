import type { SkillGroup } from '../../types/portfolio'

interface SkillsProps {
  skillGroups: SkillGroup[]
}

export function Skills({ skillGroups }: SkillsProps) {
  return (
    <section className="section bg-light" id="skills">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-12 text-center">
            <div className="section-title mb-4 pb-2">
              <h4 className="title mb-4">Skills</h4>
              <p className="text-muted para-desc mx-auto mb-0">
                Technical toolkit spanning backend, cloud, frontend, data and platform automation.
              </p>
            </div>
          </div>
        </div>

        <div className="row">
          {skillGroups.map((group) => (
            <div key={group.category} className="col-lg-4 col-md-6 mt-4 pt-2">
              <div className="card border-0 shadow rounded h-100">
                <div className="card-body">
                  <div className="d-flex align-items-center mb-3">
                    <div className="icon-circle icon-circle-sm me-3">
                      <i className={`uil ${group.icon}`}></i>
                    </div>
                    <h6 className="mb-0">{group.category}</h6>
                  </div>
                  <ul className="list-unstyled skill-badge-list mb-0">
                    {group.items.map((item) => (
                      <li key={item} className="badge bg-soft-primary text-primary me-1 mb-2">
                        {item}
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
