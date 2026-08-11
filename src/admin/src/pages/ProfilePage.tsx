import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import { CrudTable, type CrudColumn } from '../components/crud/CrudTable'
import { CrudForm, type FieldSchema } from '../components/crud/CrudForm'
import type { Profile, ProfileRequest, Strength, StrengthRequest } from '../types/portfolio'

const PROFILE_SCHEMA: FieldSchema<ProfileRequest>[] = [
  { key: 'name', label: 'Name', type: 'text' },
  { key: 'initials', label: 'Initials', type: 'text' },
  { key: 'title', label: 'Title', type: 'text' },
  { key: 'roles', label: 'Rotating Roles (hero typewriter)', type: 'array' },
  { key: 'location', label: 'Location', type: 'text' },
  { key: 'email', label: 'Email', type: 'email' },
  { key: 'phone', label: 'Phone (display)', type: 'text' },
  { key: 'phoneHref', label: 'Phone (tel: link)', type: 'text', placeholder: 'tel:+447774031733' },
  { key: 'linkedInUrl', label: 'LinkedIn URL', type: 'text' },
  { key: 'summary', label: 'Summary Paragraphs', type: 'array-multiline' },
]

const STRENGTH_SCHEMA: FieldSchema<StrengthRequest>[] = [
  { key: 'title', label: 'Title', type: 'text' },
  { key: 'icon', label: 'Icon (Unicons class, e.g. uil-server)', type: 'text' },
  { key: 'description', label: 'Description', type: 'textarea' },
  { key: 'sortOrder', label: 'Sort Order', type: 'number' },
]

const STRENGTH_COLUMNS: CrudColumn<Strength>[] = [
  { header: 'Title', render: (row) => row.title },
  { header: 'Icon', render: (row) => row.icon },
]

export function ProfilePage() {
  const [profile, setProfile] = useState<Profile | null>(null)
  const [profileForm, setProfileForm] = useState<ProfileRequest | null>(null)
  const [profileSaving, setProfileSaving] = useState(false)
  const [profileError, setProfileError] = useState<string | null>(null)
  const [profileSaved, setProfileSaved] = useState(false)

  const [strengths, setStrengths] = useState<Strength[] | null>(null)
  const [editingId, setEditingId] = useState<string | null>(null)
  const [strengthForm, setStrengthForm] = useState<StrengthRequest | null>(null)
  const [strengthSaving, setStrengthSaving] = useState(false)
  const [strengthError, setStrengthError] = useState<string | null>(null)

  const loadProfile = () =>
    portfolioApi.getProfile().then((p) => {
      setProfile(p)
      setProfileForm({
        name: p.name,
        initials: p.initials,
        title: p.title,
        roles: p.roles,
        location: p.location,
        email: p.email,
        phone: p.phone,
        phoneHref: p.phoneHref,
        linkedInUrl: p.linkedInUrl,
        summary: p.summary,
      })
    })

  const loadStrengths = () => portfolioApi.getStrengths().then(setStrengths)

  useEffect(() => {
    loadProfile()
    loadStrengths()
  }, [])

  const handleProfileSubmit = async () => {
    if (!profileForm) return
    setProfileSaving(true)
    setProfileError(null)
    setProfileSaved(false)
    try {
      await portfolioApi.updateProfile(profileForm)
      await loadProfile()
      setProfileSaved(true)
    } catch {
      setProfileError('Failed to save profile.')
    } finally {
      setProfileSaving(false)
    }
  }

  const startCreateStrength = () => {
    setEditingId('new')
    setStrengthForm({ icon: 'uil-server', title: '', description: '', sortOrder: 0 })
    setStrengthError(null)
  }

  const startEditStrength = (s: Strength) => {
    setEditingId(s.id)
    setStrengthForm({ icon: s.icon, title: s.title, description: s.description, sortOrder: 0 })
    setStrengthError(null)
  }

  const cancelStrength = () => {
    setEditingId(null)
    setStrengthForm(null)
  }

  const handleStrengthSubmit = async () => {
    if (!strengthForm) return
    setStrengthSaving(true)
    setStrengthError(null)
    try {
      if (editingId === 'new') {
        await portfolioApi.createStrength(strengthForm)
      } else if (editingId) {
        await portfolioApi.updateStrength(editingId, strengthForm)
      }
      cancelStrength()
      await loadStrengths()
    } catch {
      setStrengthError('Failed to save. Please check the fields and try again.')
    } finally {
      setStrengthSaving(false)
    }
  }

  const handleStrengthDelete = async (s: Strength) => {
    if (!window.confirm(`Delete "${s.title}"?`)) return
    await portfolioApi.deleteStrength(s.id)
    await loadStrengths()
  }

  if (!profile || !profileForm || strengths === null) {
    return <div className="spinner-border text-primary" role="status" />
  }

  return (
    <>
      <h5 className="mb-4">Profile</h5>
      <div className="card border-0 shadow rounded p-4 mb-5">
        {profileSaved && <div className="alert alert-success py-2">Profile updated.</div>}
        <CrudForm
          schema={PROFILE_SCHEMA}
          value={profileForm}
          onChange={(v) => {
            setProfileForm(v)
            setProfileSaved(false)
          }}
          onSubmit={handleProfileSubmit}
          isSubmitting={profileSaving}
          error={profileError}
          submitLabel="Save Profile"
        />
      </div>

      <div className="d-md-flex justify-content-between align-items-center mb-4">
        <h5 className="mb-0">Strengths</h5>
        {!editingId && (
          <button className="btn btn-primary mt-2 mt-md-0" onClick={startCreateStrength}>
            <i className="uil uil-plus"></i> Add Strength
          </button>
        )}
      </div>

      {editingId ? (
        <div className="card border-0 shadow rounded p-4">
          <CrudForm
            schema={STRENGTH_SCHEMA}
            value={strengthForm!}
            onChange={setStrengthForm}
            onSubmit={handleStrengthSubmit}
            isSubmitting={strengthSaving}
            error={strengthError}
            submitLabel={editingId === 'new' ? 'Create' : 'Update'}
          />
          <button className="btn btn-outline-secondary mt-2" type="button" onClick={cancelStrength}>
            Cancel
          </button>
        </div>
      ) : (
        <CrudTable
          columns={STRENGTH_COLUMNS}
          rows={strengths}
          onEdit={startEditStrength}
          onDelete={handleStrengthDelete}
        />
      )}
    </>
  )
}
