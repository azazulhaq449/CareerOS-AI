interface ArrayFieldProps {
  label: string
  values: string[]
  onChange: (values: string[]) => void
  placeholder?: string
  multiline?: boolean
}

export function ArrayField({ label, values, onChange, placeholder, multiline }: ArrayFieldProps) {
  const updateAt = (index: number, value: string) => {
    const next = [...values]
    next[index] = value
    onChange(next)
  }

  const removeAt = (index: number) => {
    onChange(values.filter((_, i) => i !== index))
  }

  const add = () => onChange([...values, ''])

  return (
    <div className="mb-3">
      <label className="form-label">{label}</label>
      {values.map((value, index) =>
        multiline ? (
          <div className="array-field-row" key={index}>
            <textarea
              className="form-control"
              rows={2}
              value={value}
              placeholder={placeholder}
              onChange={(e) => updateAt(index, e.target.value)}
            />
            <button type="button" className="btn btn-sm btn-soft-danger" onClick={() => removeAt(index)}>
              <i className="uil uil-times"></i>
            </button>
          </div>
        ) : (
          <div className="array-field-row" key={index}>
            <input
              type="text"
              className="form-control"
              value={value}
              placeholder={placeholder}
              onChange={(e) => updateAt(index, e.target.value)}
            />
            <button type="button" className="btn btn-sm btn-soft-danger" onClick={() => removeAt(index)}>
              <i className="uil uil-times"></i>
            </button>
          </div>
        ),
      )}
      <button type="button" className="btn btn-sm btn-soft-primary mt-1" onClick={add}>
        <i className="uil uil-plus"></i> Add
      </button>
    </div>
  )
}
