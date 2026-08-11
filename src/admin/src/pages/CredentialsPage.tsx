import { useEffect, useState } from 'react'
import { portfolioApi } from '../api/portfolioApi'
import { CrudTable, type CrudColumn } from '../components/crud/CrudTable'
import { CrudForm, type FieldSchema } from '../components/crud/CrudForm'
import type { Certification, CertificationRequest, CredentialsResponse, EducationEntryRequest } from '../types/portfolio'

const CERT_SCHEMA: FieldSchema<CertificationRequest>[] = [
  { key: 'name', label: 'Name', type: 'text' },
  { key: 'date', label: 'Date', type: 'text', placeholder: 'e.g. March 2021' },
  { key: 'sortOrder', label: 'Sort Order', type: 'number' },
]

const CERT_COLUMNS: CrudColumn<Certification>[] = [
  { header: 'Name', render: (row) => row.name },
  { header: 'Date', render: (row) => row.date },
]

const EDUCATION_SCHEMA: FieldSchema<EducationEntryRequest>[] = [
  { key: 'degree', label: 'Degree', type: 'text' },
  { key: 'school', label: 'School', type: 'text' },
  { key: 'location', label: 'Location', type: 'text' },
  { key: 'date', label: 'Date', type: 'text' },
]

export function CredentialsPage() {
  const [data, setData] = useState<CredentialsResponse | null>(null)
  const [educationForm, setEducationForm] = useState<EducationEntryRequest | null>(null)
  const [educationSaving, setEducationSaving] = useState(false)
  const [educationSaved, setEducationSaved] = useState(false)
  const [educationError, setEducationError] = useState<string | null>(null)

  const [editingId, setEditingId] = useState<string | null>(null)
  const [certForm, setCertForm] = useState<CertificationRequest | null>(null)
  const [certSaving, setCertSaving] = useState(false)
  const [certError, setCertError] = useState<string | null>(null)

  const load = () =>
    portfolioApi.getCredentials().then((d) => {
      setData(d)
      setEducationForm(d.education)
    })

  useEffect(() => {
    load()
  }, [])

  const handleEducationSubmit = async () => {
    if (!educationForm) return
    setEducationSaving(true)
    setEducationError(null)
    setEducationSaved(false)
    try {
      await portfolioApi.updateEducation(educationForm)
      await load()
      setEducationSaved(true)
    } catch {
      setEducationError('Failed to save education.')
    } finally {
      setEducationSaving(false)
    }
  }

  const startCreateCert = () => {
    setEditingId('new')
    setCertForm({ name: '', date: '', sortOrder: 0 })
    setCertError(null)
  }

  const startEditCert = (cert: Certification) => {
    setEditingId(cert.id)
    setCertForm({ name: cert.name, date: cert.date, sortOrder: 0 })
    setCertError(null)
  }

  const cancelCert = () => {
    setEditingId(null)
    setCertForm(null)
  }

  const handleCertSubmit = async () => {
    if (!certForm) return
    setCertSaving(true)
    setCertError(null)
    try {
      if (editingId === 'new') {
        await portfolioApi.createCertification(certForm)
      } else if (editingId) {
        await portfolioApi.updateCertification(editingId, certForm)
      }
      cancelCert()
      await load()
    } catch {
      setCertError('Failed to save. Please check the fields and try again.')
    } finally {
      setCertSaving(false)
    }
  }

  const handleCertDelete = async (cert: Certification) => {
    if (!window.confirm(`Delete "${cert.name}"?`)) return
    await portfolioApi.deleteCertification(cert.id)
    await load()
  }

  if (!data || !educationForm) {
    return <div className="spinner-border text-primary" role="status" />
  }

  return (
    <>
      <h5 className="mb-4">Education</h5>
      <div className="card border-0 shadow rounded p-4 mb-5">
        {educationSaved && <div className="alert alert-success py-2">Education updated.</div>}
        <CrudForm
          schema={EDUCATION_SCHEMA}
          value={educationForm}
          onChange={(v) => {
            setEducationForm(v)
            setEducationSaved(false)
          }}
          onSubmit={handleEducationSubmit}
          isSubmitting={educationSaving}
          error={educationError}
          submitLabel="Save Education"
        />
      </div>

      <div className="d-md-flex justify-content-between align-items-center mb-4">
        <h5 className="mb-0">Certifications</h5>
        {!editingId && (
          <button className="btn btn-primary mt-2 mt-md-0" onClick={startCreateCert}>
            <i className="uil uil-plus"></i> Add Certification
          </button>
        )}
      </div>

      {editingId ? (
        <div className="card border-0 shadow rounded p-4">
          <CrudForm
            schema={CERT_SCHEMA}
            value={certForm!}
            onChange={setCertForm}
            onSubmit={handleCertSubmit}
            isSubmitting={certSaving}
            error={certError}
            submitLabel={editingId === 'new' ? 'Create' : 'Update'}
          />
          <button className="btn btn-outline-secondary mt-2" type="button" onClick={cancelCert}>
            Cancel
          </button>
        </div>
      ) : (
        <CrudTable
          columns={CERT_COLUMNS}
          rows={data.certifications}
          onEdit={startEditCert}
          onDelete={handleCertDelete}
        />
      )}
    </>
  )
}
