  // contract end logic
  export function calculateContractExpiry(
    startDate: string | null,
    contractTermMonths: string | null,
    customValue: number | null
  ): Date | null {
    if (!startDate) {
      return null;
    }

    const date = new Date(startDate);

    // Case: contractTerm is 'Open' or null/undefined
    if (
      contractTermMonths === null ||
      contractTermMonths === undefined ||
      contractTermMonths === '' ||
      contractTermMonths === 'custom' ||
      contractTermMonths.toLowerCase?.() === 'open'
    ) {
      if (!customValue || customValue === 0) {
        return null;
      }

      date.setMonth(date.getMonth() + customValue);
      return date;
    }

    // Try parsing contractTermMonths as integer
    const term = parseInt(contractTermMonths, 10);
    if (!isNaN(term)) {
      date.setMonth(date.getMonth() + term);
      return date;
    }

    return null;
  }

  export function hasContractExpired(
    startDate: string | null,
    contractTermMonths: string | null,
    customValue: number | null
  ) {
    const expiryDate = calculateContractExpiry(
      startDate,
      contractTermMonths,
      customValue
    );
    if (expiryDate != null) {
      if (expiryDate <= new Date()) {
        return true;
      }
    }
    return false;
  }