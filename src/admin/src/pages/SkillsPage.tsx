import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import { CrudTable, type CrudColumn } from '../components/crud/CrudTable'
import { CrudForm, type FieldSchema } from '../components/crud/CrudForm'
import type { SkillGroup, SkillGroupRequest } from '../types/portfolio'

const EMPTY: SkillGroupRequest = {
  category: '',
  icon: 'uil-server',
  items: [],
  sortOrder: 0,
}

const SCHEMA: FieldSchema<SkillGroupRequest>[] = [
  { key: 'category', label: 'Category', type: 'text' },
  { key: 'icon', label: 'Icon (Unicons class, e.g. uil-server)', type: 'text' },
  { key: 'items', label: 'Skills', type: 'array' },
  { key: 'sortOrder', label: 'Sort Order', type: 'number' },
]

const COLUMNS: CrudColumn<SkillGroup>[] = [
  { header: 'Category', render: (row) => row.category },
  { header: 'Skills', render: (row) => row.items.join(', ') },
]

export function SkillsPage() {
  const [entries, setEntries] = useState<SkillGroup[] | null>(null)
  const [editingId, setEditingId] = useState<string | null>(null)
  const [formValue, setFormValue] = useState<SkillGroupRequest | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const load = () => portfolioApi.getSkills().then(setEntries)

  useEffect(() => {
    load()
  }, [])

  const startCreate = () => {
    setEditingId('new')
    setFormValue(EMPTY)
    setError(null)
  }

  const startEdit = (entry: SkillGroup) => {
    setEditingId(entry.id)
    setFormValue({ category: entry.category, icon: entry.icon, items: entry.items, sortOrder: 0 })
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
        await portfolioApi.createSkillGroup(formValue)
      } else if (editingId) {
        await portfolioApi.updateSkillGroup(editingId, formValue)
      }
      cancel()
      await load()
    } catch {
      setError('Failed to save. Please check the fields and try again.')
    } finally {
      setIsSubmitting(false)
    }
  }

  const handleDelete = async (entry: SkillGroup) => {
    if (!window.confirm(`Delete "${entry.category}"?`)) return
    await portfolioApi.deleteSkillGroup(entry.id)
    await load()
  }

  if (entries === null) {
    return <div className="spinner-border text-primary" role="status" />
  }

  return (
    <>
      <div className="d-md-flex justify-content-between align-items-center mb-4">
        <h5 className="mb-0">Skills</h5>
        {!editingId && (
          <button className="btn btn-primary mt-2 mt-md-0" onClick={startCreate}>
            <i className="uil uil-plus"></i> Add Skill Group
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
