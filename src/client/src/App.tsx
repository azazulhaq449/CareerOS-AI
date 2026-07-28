import { Navbar } from './components/layout/Navbar'
import { Footer } from './components/layout/Footer'
import { BackToTop } from './components/layout/BackToTop'
import { Hero } from './components/sections/Hero'
import { About } from './components/sections/About'
import { Experience } from './components/sections/Experience'
import { Projects } from './components/sections/Projects'
import { Skills } from './components/sections/Skills'
import { Credentials } from './components/sections/Credentials'
import { Contact } from './components/sections/Contact'
import { usePortfolioData } from './hooks/usePortfolioData'
import './styles/portfolio.css'

function App() {
  const { data, isLoading, error } = usePortfolioData()

  if (isLoading) {
    return (
      <div className="loading-screen">
        <div className="spinner-border text-primary" role="status">
          <span className="visually-hidden">Loading…</span>
        </div>
      </div>
    )
  }

  if (error || !data) {
    return (
      <div className="loading-screen">
        <div className="text-center">
          <p className="text-danger fw-bold mb-2">Unable to load portfolio content.</p>
          <p className="text-muted mb-0">{error ?? 'Unknown error.'}</p>
          <p className="text-muted small mt-3">Is the CareerOS API running on the configured URL?</p>
        </div>
      </div>
    )
  }

  return (
    <>
      <Navbar initials={data.profile.initials} />
      <Hero profile={data.profile} />
      <About profile={data.profile} strengths={data.strengths} />
      <Experience experience={data.experience} />
      <Projects projects={data.projects} />
      <Skills skillGroups={data.skillGroups} />
      <Credentials credentials={data.credentials} />
      <Contact profile={data.profile} />
      <Footer profile={data.profile} />
      <BackToTop />
    </>
  )
}

export default App
