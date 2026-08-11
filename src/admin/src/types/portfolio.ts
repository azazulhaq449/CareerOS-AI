// Mirrors the JSON contracts returned by the CareerOS.Server portfolio API
// (src/server/Models, src/server/Models/Requests). The API is the single
// source of truth for this content.

export interface Profile {
  id: string
  name: string
  initials: string
  title: string
  roles: string[]
  location: string
  email: string
  phone: string
  phoneHref: string
  linkedInUrl: string | null
  summary: string[]
}

export type ProfileRequest = Omit<Profile, 'id'>

export interface Strength {
  id: string
  icon: string
  title: string
  description: string
}

export type StrengthRequest = Omit<Strength, 'id'> & { sortOrder: number }

export interface ExperienceEntry {
  id: string
  company: string
  location: string
  role: string
  period: string
  projects: string
  highlights: string[]
}

export type ExperienceEntryRequest = Omit<ExperienceEntry, 'id'> & { sortOrder: number }

export interface ProjectEntry {
  id: string
  name: string
  organisation: string
  icon: string
  description: string
  tags: string[]
}

export type ProjectEntryRequest = Omit<ProjectEntry, 'id'> & { sortOrder: number }

export interface SkillGroup {
  id: string
  category: string
  icon: string
  items: string[]
}

export type SkillGroupRequest = Omit<SkillGroup, 'id'> & { sortOrder: number }

export interface Certification {
  id: string
  name: string
  date: string
}

export type CertificationRequest = Omit<Certification, 'id'> & { sortOrder: number }

export interface EducationEntry {
  school: string
  degree: string
  location: string
  date: string
}

export type EducationEntryRequest = EducationEntry

export interface CredentialsResponse {
  certifications: Certification[]
  education: EducationEntry
}
