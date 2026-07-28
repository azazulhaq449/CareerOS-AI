// Mirrors the JSON contracts returned by the CareerOS.Server portfolio API
// (src/server/Models). The API is the single source of truth for this
// content — these are type-only shapes, not data.

export interface Profile {
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

export interface Strength {
  icon: string
  title: string
  description: string
}

export interface ExperienceEntry {
  company: string
  location: string
  role: string
  period: string
  projects: string
  highlights: string[]
}

export interface ProjectEntry {
  name: string
  organisation: string
  icon: string
  description: string
  tags: string[]
}

export interface SkillGroup {
  category: string
  icon: string
  items: string[]
}

export interface Certification {
  name: string
  date: string
}

export interface EducationEntry {
  school: string
  degree: string
  location: string
  date: string
}

export interface CredentialsResponse {
  certifications: Certification[]
  education: EducationEntry
}
