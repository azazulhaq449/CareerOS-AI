import { apiClient } from './client'
import type {
  Certification,
  CertificationRequest,
  CredentialsResponse,
  EducationEntryRequest,
  ExperienceEntry,
  ExperienceEntryRequest,
  Profile,
  ProfileRequest,
  ProjectEntry,
  ProjectEntryRequest,
  SkillGroup,
  SkillGroupRequest,
  Strength,
  StrengthRequest,
} from '../types/portfolio'

export const portfolioApi = {
  // Profile (singleton) + Strengths
  getProfile: () => apiClient.get<Profile>('/api/profile'),
  updateProfile: (request: ProfileRequest) => apiClient.put<void>('/api/profile', request),
  getStrengths: () => apiClient.get<Strength[]>('/api/profile/strengths'),
  createStrength: (request: StrengthRequest) => apiClient.post<Strength>('/api/profile/strengths', request),
  updateStrength: (id: string, request: StrengthRequest) =>
    apiClient.put<void>(`/api/profile/strengths/${id}`, request),
  deleteStrength: (id: string) => apiClient.delete(`/api/profile/strengths/${id}`),

  // Experience
  getExperience: () => apiClient.get<ExperienceEntry[]>('/api/experience'),
  createExperience: (request: ExperienceEntryRequest) => apiClient.post<ExperienceEntry>('/api/experience', request),
  updateExperience: (id: string, request: ExperienceEntryRequest) =>
    apiClient.put<void>(`/api/experience/${id}`, request),
  deleteExperience: (id: string) => apiClient.delete(`/api/experience/${id}`),

  // Projects
  getProjects: () => apiClient.get<ProjectEntry[]>('/api/projects'),
  createProject: (request: ProjectEntryRequest) => apiClient.post<ProjectEntry>('/api/projects', request),
  updateProject: (id: string, request: ProjectEntryRequest) => apiClient.put<void>(`/api/projects/${id}`, request),
  deleteProject: (id: string) => apiClient.delete(`/api/projects/${id}`),

  // Skills
  getSkills: () => apiClient.get<SkillGroup[]>('/api/skills'),
  createSkillGroup: (request: SkillGroupRequest) => apiClient.post<SkillGroup>('/api/skills', request),
  updateSkillGroup: (id: string, request: SkillGroupRequest) => apiClient.put<void>(`/api/skills/${id}`, request),
  deleteSkillGroup: (id: string) => apiClient.delete(`/api/skills/${id}`),

  // Credentials: Certifications + Education
  getCredentials: () => apiClient.get<CredentialsResponse>('/api/credentials'),
  createCertification: (request: CertificationRequest) =>
    apiClient.post<Certification>('/api/credentials/certifications', request),
  updateCertification: (id: string, request: CertificationRequest) =>
    apiClient.put<void>(`/api/credentials/certifications/${id}`, request),
  deleteCertification: (id: string) => apiClient.delete(`/api/credentials/certifications/${id}`),
  updateEducation: (request: EducationEntryRequest) => apiClient.put<void>('/api/credentials/education', request),
}
