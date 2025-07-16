export interface ColumnOption {
  id: string;
  label: string;
  checked: boolean;
}

export function toggleAllColumns(columns: ColumnOption[], selectAll: boolean) {
  columns.forEach((col) => {
    col.checked = selectAll;
  });
}

export function clearAllColumns(columns: ColumnOption[]) {
  toggleAllColumns(columns, false);
}

export function updateSelectAllColumnsState(columns: ColumnOption[]): boolean {
  return columns.every((col) => col.checked);
}

export function getSelectedColumns(columns: ColumnOption[]): string[] {
  return columns.filter((col) => col.checked).map((col) => col.id);
}