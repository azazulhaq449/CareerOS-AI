import type { FormEvent } from 'react'
import { ArrayField } from './ArrayField'

export type FieldType = 'text' | 'textarea' | 'array' | 'array-multiline' | 'number' | 'email'

export interface FieldSchema<T> {
  key: keyof T
  label: string
  type: FieldType
  icon?: string
  placeholder?: string
}

interface CrudFormProps<T> {
  schema: FieldSchema<T>[]
  value: T
  onChange: (value: T) => void
  onSubmit: () => void
  submitLabel?: string
  isSubmitting?: boolean
  error?: string | null
}

export function CrudForm<T extends object>({
  schema,
  value,
  onChange,
  onSubmit,
  submitLabel = 'Save',
  isSubmitting = false,
  error,
}: CrudFormProps<T>) {
  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    onSubmit()
  }

  const setField = (key: keyof T, fieldValue: unknown) => {
    onChange({ ...value, [key]: fieldValue })
  }

  return (
    <form onSubmit={handleSubmit}>
      {error && <div className="alert alert-danger py-2">{error}</div>}

      {schema.map((field) => {
        const key = field.key as string
        const current = value[field.key]

        if (field.type === 'array' || field.type === 'array-multiline') {
          return (
            <ArrayField
              key={key}
              label={field.label}
              values={(current as string[] | undefined) ?? []}
              onChange={(v) => setField(field.key, v)}
              placeholder={field.placeholder}
              multiline={field.type === 'array-multiline'}
            />
          )
        }

        return (
          <div className="mb-3" key={key}>
            <label className="form-label">{field.label}</label>
            <div className="form-icon position-relative">
              {field.icon && <i className={`uil ${field.icon} fea icon-sm icons`}></i>}
              {field.type === 'textarea' ? (
                <textarea
                  className={field.icon ? 'form-control ps-5' : 'form-control'}
                  rows={4}
                  value={(current as string) ?? ''}
                  placeholder={field.placeholder}
                  onChange={(e) => setField(field.key, e.target.value)}
                />
              ) : (
                <input
                  type={field.type === 'number' ? 'number' : field.type === 'email' ? 'email' : 'text'}
                  className={field.icon ? 'form-control ps-5' : 'form-control'}
                  value={(current as string | number) ?? ''}
                  placeholder={field.placeholder}
                  onChange={(e) =>
                    setField(field.key, field.type === 'number' ? Number(e.target.value) : e.target.value)
                  }
                />
              )}
            </div>
          </div>
        )
      })}

      <button className="btn btn-primary" type="submit" disabled={isSubmitting}>
        {isSubmitting ? 'Saving…' : submitLabel}
      </button>
    </form>
  )
}
