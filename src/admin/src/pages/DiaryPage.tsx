import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import { CrudTable, type CrudColumn } from '../components/crud/CrudTable'
import { CrudForm, type FieldSchema } from '../components/crud/CrudForm'
import type { DiaryEntry, DiaryEntryRequest, DiaryQuery } from '../types/portfolio'

const todayIso = () => new Date().toISOString().slice(0, 10)

const EMPTY: DiaryEntryRequest = {
  title: '',
  content: '',
  category: '',
  tags: [],
  entryDate: todayIso(),
}

const EMPTY_FILTERS: DiaryQuery = { search: '', category: '', tag: '' }

const SCHEMA: FieldSchema<DiaryEntryRequest>[] = [
  { key: 'title', label: 'Title', type: 'text' },
  { key: 'category', label: 'Category', type: 'text', placeholder: 'e.g. Bug Fix, Architecture, Learning' },
  { key: 'tags', label: 'Tags', type: 'array' },
  { key: 'entryDate', label: 'Entry Date', type: 'date' },
  { key: 'content', label: 'Content', type: 'textarea' },
]

const COLUMNS: CrudColumn<DiaryEntry>[] = [
  { header: 'Title', render: (row) => row.title },
  { header: 'Category', render: (row) => row.category },
  { header: 'Tags', render: (row) => row.tags.join(', ') },
  { header: 'Entry Date', render: (row) => row.entryDate },
]

export function DiaryPage() {
  const [entries, setEntries] = useState<DiaryEntry[] | null>(null)
  const [filters, setFilters] = useState<DiaryQuery>(EMPTY_FILTERS)
  const [editingId, setEditingId] = useState<string | null>(null)
  const [formValue, setFormValue] = useState<DiaryEntryRequest | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const load = (query?: DiaryQuery) => portfolioApi.getDiaryEntries(query ?? filters).then(setEntries)

  useEffect(() => {
    load(EMPTY_FILTERS)
  }, [])

  const handleSearch = () => load(filters)

  const clearFilters = () => {
    setFilters(EMPTY_FILTERS)
    load(EMPTY_FILTERS)
  }

  const startCreate = () => {
    setEditingId('new')
    setFormValue(EMPTY)
    setError(null)
  }

  const startEdit = (entry: DiaryEntry) => {
    setEditingId(entry.id)
    setFormValue({
      title: entry.title,
      content: entry.content,
      category: entry.category,
      tags: entry.tags,
      entryDate: entry.entryDate,
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
        await portfolioApi.createDiaryEntry(formValue)
      } else if (editingId) {
        await portfolioApi.updateDiaryEntry(editingId, formValue)
      }
      cancel()
      await load()
    } catch {
      setError('Failed to save. Please check the fields and try again.')
    } finally {
      setIsSubmitting(false)
    }
  }

  const handleDelete = async (entry: DiaryEntry) => {
    if (!window.confirm(`Delete "${entry.title}"?`)) return
    await portfolioApi.deleteDiaryEntry(entry.id)
    await load()
  }

  if (entries === null) {
    return <div className="spinner-border text-primary" role="status" />
  }

  return (
    <>
      <div className="d-md-flex justify-content-between align-items-center mb-4">
        <h5 className="mb-0">Engineering Diary</h5>
        {!editingId && (
          <button className="btn btn-primary mt-2 mt-md-0" onClick={startCreate}>
            <i className="uil uil-plus"></i> Add Entry
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
        <>
          <div className="row g-2 mb-3">
            <div className="col-md-4">
              <input
                className="form-control"
                placeholder="Search title & content"
                value={filters.search}
                onChange={(e) => setFilters({ ...filters, search: e.target.value })}
              />
            </div>
            <div className="col-md-3">
              <input
                className="form-control"
                placeholder="Category"
                value={filters.category}
                onChange={(e) => setFilters({ ...filters, category: e.target.value })}
              />
            </div>
            <div className="col-md-3">
              <input
                className="form-control"
                placeholder="Tag"
                value={filters.tag}
                onChange={(e) => setFilters({ ...filters, tag: e.target.value })}
              />
            </div>
            <div className="col-md-2 d-flex gap-2">
              <button className="btn btn-primary flex-grow-1" onClick={handleSearch}>
                Search
              </button>
              <button className="btn btn-outline-secondary" onClick={clearFilters}>
                Clear
              </button>
            </div>
          </div>

          <CrudTable columns={COLUMNS} rows={entries} onEdit={startEdit} onDelete={handleDelete} />
        </>
      )}
    </>
  )
}
