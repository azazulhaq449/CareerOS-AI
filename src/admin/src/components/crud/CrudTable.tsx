import type { ReactNode } from 'react'

export interface CrudColumn<T> {
  header: string
  render: (row: T) => ReactNode
}

interface CrudTableProps<T extends { id: string }> {
  columns: CrudColumn<T>[]
  rows: T[]
  onEdit: (row: T) => void
  onDelete: (row: T) => void
  emptyMessage?: string
}

export function CrudTable<T extends { id: string }>({
  columns,
  rows,
  onEdit,
  onDelete,
  emptyMessage = 'No records yet.',
}: CrudTableProps<T>) {
  if (rows.length === 0) {
    return <p className="text-muted">{emptyMessage}</p>
  }

  return (
    <div className="table-responsive shadow rounded">
      <table className="table table-center bg-white mb-0">
        <thead>
          <tr>
            {columns.map((col) => (
              <th key={col.header} className="border-bottom p-3">
                {col.header}
              </th>
            ))}
            <th className="text-end border-bottom p-3" style={{ minWidth: 160 }}></th>
          </tr>
        </thead>
        <tbody>
          {rows.map((row) => (
            <tr key={row.id}>
              {columns.map((col) => (
                <td key={col.header} className="p-3">
                  {col.render(row)}
                </td>
              ))}
              <td className="text-end p-3">
                <button className="btn btn-sm btn-soft-primary" type="button" onClick={() => onEdit(row)}>
                  <i className="uil uil-edit-alt"></i> Edit
                </button>
                <button
                  className="btn btn-sm btn-soft-danger ms-2"
                  type="button"
                  onClick={() => onDelete(row)}
                >
                  <i className="uil uil-trash-alt"></i> Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
