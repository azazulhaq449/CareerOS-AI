import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import { CrudTable, type CrudColumn } from '../components/crud/CrudTable'
import { CrudForm, type FieldSchema } from '../components/crud/CrudForm'
import type { ProjectEntry, ProjectEntryRequest } from '../types/portfolio'

const EMPTY: ProjectEntryRequest = {
  name: '',
  organisation: '',
  icon: 'uil-briefcase-alt',
  description: '',
  tags: [],
  sortOrder: 0,
}

const SCHEMA: FieldSchema<ProjectEntryRequest>[] = [
  { key: 'name', label: 'Project Name', type: 'text' },
  { key: 'organisation', label: 'Organisation', type: 'text' },
  { key: 'icon', label: 'Icon (Unicons class, e.g. uil-ticket)', type: 'text' },
  { key: 'description', label: 'Description', type: 'textarea' },
  { key: 'tags', label: 'Tags', type: 'array' },
  { key: 'sortOrder', label: 'Sort Order', type: 'number' },
]

const COLUMNS: CrudColumn<ProjectEntry>[] = [
  { header: 'Name', render: (row) => row.name },
  { header: 'Organisation', render: (row) => row.organisation },
  { header: 'Tags', render: (row) => row.tags.join(', ') },
]

export function ProjectsPage() {
  const [entries, setEntries] = useState<ProjectEntry[] | null>(null)
  const [editingId, setEditingId] = useState<string | null>(null)
  const [formValue, setFormValue] = useState<ProjectEntryRequest | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const load = () => portfolioApi.getProjects().then(setEntries)

  useEffect(() => {
    load()
  }, [])

  const startCreate = () => {
    setEditingId('new')
    setFormValue(EMPTY)
    setError(null)
  }

  const startEdit = (entry: ProjectEntry) => {
    setEditingId(entry.id)
    setFormValue({
      name: entry.name,
      organisation: entry.organisation,
      icon: entry.icon,
      description: entry.description,
      tags: entry.tags,
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
        await portfolioApi.createProject(formValue)
      } else if (editingId) {
        await portfolioApi.updateProject(editingId, formValue)
      }
      cancel()
      await load()
    } catch {
      setError('Failed to save. Please check the fields and try again.')
    } finally {
      setIsSubmitting(false)
    }
  }

  const handleDelete = async (entry: ProjectEntry) => {
    if (!window.confirm(`Delete "${entry.name}"?`)) return
    await portfolioApi.deleteProject(entry.id)
    await load()
  }

  if (entries === null) {
    return <div className="spinner-border text-primary" role="status" />
  }

  return (
    <>
      <div className="d-md-flex justify-content-between align-items-center mb-4">
        <h5 className="mb-0">Projects</h5>
        {!editingId && (
          <button className="btn btn-primary mt-2 mt-md-0" onClick={startCreate}>
            <i className="uil uil-plus"></i> Add Project
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
