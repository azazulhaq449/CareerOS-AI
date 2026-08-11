import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import { CrudTable, type CrudColumn } from '../components/crud/CrudTable'
import { CrudForm, type FieldSchema } from '../components/crud/CrudForm'
import type { ExperienceEntry, ExperienceEntryRequest } from '../types/portfolio'

const EMPTY: ExperienceEntryRequest = {
  company: '',
  location: '',
  role: '',
  period: '',
  projects: '',
  highlights: [],
  sortOrder: 0,
}

const SCHEMA: FieldSchema<ExperienceEntryRequest>[] = [
  { key: 'role', label: 'Role', type: 'text' },
  { key: 'company', label: 'Company', type: 'text' },
  { key: 'location', label: 'Location', type: 'text' },
  { key: 'period', label: 'Period', type: 'text', placeholder: 'e.g. March 2024 – Present' },
  { key: 'projects', label: 'Projects', type: 'text' },
  { key: 'highlights', label: 'Highlights', type: 'array-multiline' },
  { key: 'sortOrder', label: 'Sort Order', type: 'number' },
]

const COLUMNS: CrudColumn<ExperienceEntry>[] = [
  { header: 'Role', render: (row) => row.role },
  { header: 'Company', render: (row) => row.company },
  { header: 'Period', render: (row) => row.period },
]

export function ExperiencePage() {
  const [entries, setEntries] = useState<ExperienceEntry[] | null>(null)
  const [editingId, setEditingId] = useState<string | null>(null)
  const [formValue, setFormValue] = useState<ExperienceEntryRequest | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const load = () => portfolioApi.getExperience().then(setEntries)

  useEffect(() => {
    load()
  }, [])

  const startCreate = () => {
    setEditingId('new')
    setFormValue(EMPTY)
    setError(null)
  }

  const startEdit = (entry: ExperienceEntry) => {
    setEditingId(entry.id)
    setFormValue({
      company: entry.company,
      location: entry.location,
      role: entry.role,
      period: entry.period,
      projects: entry.projects,
      highlights: entry.highlights,
      sortOrder: 0,
    })
    setError(null)
  }

  const cancel = () => {
    setEditingId(null)
    setFormValue(null)
  }

  const handleSubmit = async () => {
    if (!formValue) return
    setIsSubmitting(true)
    setError(null)
    try {
      if (editingId === 'new') {
        await portfolioApi.createExperience(formValue)
      } else if (editingId) {
        await portfolioApi.updateExperience(editingId, formValue)
      }
      cancel()
      await load()
    } catch {
      setError('Failed to save. Please check the fields and try again.')
    } finally {
      setIsSubmitting(false)
    }
  }

  const handleDelete = async (entry: ExperienceEntry) => {
    if (!window.confirm(`Delete "${entry.role} at ${entry.company}"?`)) return
    await portfolioApi.deleteExperience(entry.id)
    await load()
  }

  if (entries === null) {
    return <div className="spinner-border text-primary" role="status" />
  }

  return (
    <>
      <div className="d-md-flex justify-content-between align-items-center mb-4">
        <h5 className="mb-0">Work Experience</h5>
        {!editingId && (
          <button className="btn btn-primary mt-2 mt-md-0" onClick={startCreate}>
            <i className="uil uil-plus"></i> Add Experience
          </button>
        )}
      </div>

      {editingId ? (
        <div className="card border-0 shadow rounded p-4">
          <CrudForm
            schema={SCHEMA}
            value={formValue!}
            onChange={setFormValue}
            onSubmit={handleSubmit}
            isSubmitting={isSubmitting}
            error={error}
            submitLabel={editingId === 'new' ? 'Create' : 'Update'}
          />
          <button className="btn btn-outline-secondary mt-2" type="button" onClick={cancel}>
            Cancel
          </button>
        </div>
      ) : (
        <CrudTable columns={COLUMNS} rows={entries} onEdit={startEdit} onDelete={handleDelete} />
      )}
    </>
  )
}
