export function getPageNumbers(pageIndex: number, totalCount: number, pageSize: number, maxDisplay = 5): number[] {
  const totalPages = Math.ceil(totalCount / pageSize);
  const pages: number[] = [];

  if (totalPages <= maxDisplay) {
    for (let i = 1; i <= totalPages; i++) pages.push(i);
  } else {
    if (pageIndex <= 3) {
      pages.push(1, 2, 3, 4, -1, totalPages);
    } else if (pageIndex >= totalPages - 2) {
      pages.push(
        1,
        -1,
        totalPages - 3,
        totalPages - 2,
        totalPages - 1,
        totalPages
      );
    } else {
      pages.push(
        1,
        -1,
        pageIndex - 1,
        pageIndex,
        pageIndex + 1,
        -1,
        totalPages
      );
    }
  }

  return pages;
}