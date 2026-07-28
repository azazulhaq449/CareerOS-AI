import type { Profile } from '../../types/portfolio'
import { useTypewriter } from '../../hooks/useTypewriter'

interface HeroProps {
  profile: Profile
}

export function Hero({ profile }: HeroProps) {
  const typedRole = useTypewriter(profile.roles)

  return (
    <section className="bg-home d-flex align-items-center hero-section" id="home">
      <div className="container">
        <div className="row align-items-center">
          <div className="col-lg-9 col-md-11">
            <div className="title-heading">
              <p className="text-primary fw-bold text-uppercase mb-3">{profile.location}</p>
              <h1 className="display-4 fw-bold mb-3">
                {profile.name}
                <br />
                <span className="text-primary">
                  {typedRole}
                  <span className="type-cursor">|</span>
                </span>
              </h1>
              <p className="para-desc text-muted fs-5">
                {profile.title} with 8+ years delivering .NET-based web and enterprise applications,
                Azure cloud-native solutions and AI-powered features.
              </p>
              <div className="mt-4 pt-2">
                <a href="#projects" className="btn btn-primary mt-2 me-2">
                  <i className="uil uil-briefcase-alt"></i> View Projects
                </a>
                <a href="#contact" className="btn btn-outline-primary mt-2">
                  <i className="uil uil-phone"></i> Get In Touch
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}
