export interface FilterSection {
  title: string;
  options: { id: string; label: string; checked: boolean }[];
}

export function toggleAllFilters(filterSections: FilterSection[], selectAll: boolean) {
  filterSections.forEach((section) => {
    section.options.forEach((option) => {
      option.checked = selectAll;
    });
  });
}

export function updateSelectAllState(filterSections: FilterSection[]): boolean {
  return filterSections.every((section) =>
    section.options.every((option) => option.checked)
  );
}

export function clearAllFilters(filterSections: FilterSection[]) {
  toggleAllFilters(filterSections, false);
}

export function getSelectedFilters(filterSections: FilterSection[]): string[] {
  const selected: string[] = [];
  filterSections.forEach((section) => {
    section.options.forEach((option) => {
      if (option.checked && !selected.includes(option.id)) {
        selected.push(option.id);
      }
    });
  });
  return selected;
}