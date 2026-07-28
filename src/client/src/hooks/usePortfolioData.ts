import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import type {
  CredentialsResponse,
  ExperienceEntry,
  Profile,
  ProjectEntry,
  SkillGroup,
  Strength,
} from '../types/portfolio'

export interface PortfolioData {
  profile: Profile
  strengths: Strength[]
  experience: ExperienceEntry[]
  projects: ProjectEntry[]
  skillGroups: SkillGroup[]
  credentials: CredentialsResponse
}

interface PortfolioDataState {
  data: PortfolioData | null
  isLoading: boolean
  error: string | null
}

/** Fetches all portfolio content from the CareerOS API in parallel on mount. */
export function usePortfolioData(): PortfolioDataState {
  const [state, setState] = useState<PortfolioDataState>({
    data: null,
    isLoading: true,
    error: null,
  })

  useEffect(() => {
    let cancelled = false

    async function load() {
      try {
        const [profile, strengths, experience, projects, skillGroups, credentials] = await Promise.all([
          portfolioApi.getProfile(),
          portfolioApi.getStrengths(),
          portfolioApi.getExperience(),
          portfolioApi.getProjects(),
          portfolioApi.getSkills(),
          portfolioApi.getCredentials(),
        ])

        if (!cancelled) {
          setState({
            data: { profile, strengths, experience, projects, skillGroups, credentials },
            isLoading: false,
            error: null,
          })
        }
      } catch (err) {
        if (!cancelled) {
          setState({
            data: null,
            isLoading: false,
            error: err instanceof Error ? err.message : 'Failed to load portfolio content.',
          })
        }
      }
    }

    load()
    return () => {
      cancelled = true
    }
  }, [])

  return state
}
