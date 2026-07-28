import { fetchJson } from './client'
import type {
  Certification,
  CredentialsResponse,
  EducationEntry,
  ExperienceEntry,
  Profile,
  ProjectEntry,
  SkillGroup,
  Strength,
} from '../types/portfolio'

export const portfolioApi = {
  getProfile: () => fetchJson<Profile>('/api/profile'),
  getStrengths: () => fetchJson<Strength[]>('/api/profile/strengths'),
  getExperience: () => fetchJson<ExperienceEntry[]>('/api/experience'),
  getProjects: () => fetchJson<ProjectEntry[]>('/api/projects'),
  getSkills: () => fetchJson<SkillGroup[]>('/api/skills'),
  getCredentials: () => fetchJson<CredentialsResponse>('/api/credentials'),
}

export type { Certification, CredentialsResponse, EducationEntry, ExperienceEntry, Profile, ProjectEntry, SkillGroup, Strength }
