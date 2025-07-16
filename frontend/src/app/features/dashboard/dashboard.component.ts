import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  SearchCombobox,
  SearchRequest,
} from '../../shared/search-combobox/search-combobox';
import { DashboardService } from './dashboard.service';
import { getPageNumbers } from '../../service/pagination';
import {
  toggleAllFilters,
  updateSelectAllState,
  clearAllFilters,
  getSelectedFilters,
  FilterSection,
} from '../../service/filters';
import {
  toggleAllColumns,
  clearAllColumns,
  updateSelectAllColumnsState,
  getSelectedColumns,
  ColumnOption,
} from '../../service/columns';
@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class DashboardComponent implements OnInit {
  Math = Math;
  filterDropdownOpen = false;
  columnsDropdownOpen = false;

  ngOnInit() {
    document.addEventListener('click', this.closeDropdowns.bind(this));
    this.search();
  }

  ngOnDestroy() {
    document.removeEventListener('click', this.closeDropdowns.bind(this));
  }

  closeDropdowns(event: MouseEvent) {
    this.filterDropdownOpen = false;
    this.columnsDropdownOpen = false;
  }

  toggleSidebar() {
    this.opened = !this.opened;
    console.log('Sidebar opened:', this.opened);
  }

  opened = true;

  filterSections = [
    {
      title: 'Type',
      options: [
        { id: 'active', label: 'Active', checked: true },
        { id: 'inactive', label: 'Inactive', checked: true },
      ],
    },
  ];

  selectAllCheckedFilters = true;

  //filter
  onToggleAllFilters() {
    toggleAllFilters(this.filterSections, this.selectAllCheckedFilters);
  }

  onUpdateSelectAllState() {
    this.selectAllCheckedFilters = updateSelectAllState(this.filterSections);
  }

  onClearAllFilters() {
    this.selectAllCheckedFilters = false;
    clearAllFilters(this.filterSections);
  }

  onApplyFilters() {
    this.selectedFilters = getSelectedFilters(this.filterSections);
    console.log(this.selectedFilters);
    this.search();
  }

  columnsSections = [
    { id: 'clientID', label: 'Client ID', checked: true },
    { id: 'companyName', label: 'Company Name', checked: true },
    { id: 'tradingName', label: 'Trading Name', checked: true },
    { id: 'contact', label: 'Contact', checked: true },
    { id: 'phone', label: 'Phone', checked: true },
    { id: 'mobile', label: 'Mobile', checked: true },
    { id: 'email', label: 'Email', checked: true },
    { id: 'contractEnd', label: 'Contract End', checked: true },
    { id: 'connections', label: 'Connections', checked: true },
    { id: 'actions', label: 'Actions', checked: true },
  ];

  columnsSectionsTemp = [...this.columnsSections.map((col) => ({ ...col }))];

  selectAllCheckedColumns = true;
  // toggle columns
  onToggleAllColumns() {
    toggleAllColumns(this.columnsSectionsTemp, this.selectAllCheckedColumns);
  }

  onUpdateSelectAllColumnsState() {
    this.selectAllCheckedColumns = updateSelectAllColumnsState(
      this.columnsSectionsTemp
    );
  }

  onClearAllColumns() {
    this.selectAllCheckedColumns = false;
    clearAllColumns(this.columnsSectionsTemp);
  }

  onApplyColumns() {
    this.columnsSections = this.columnsSectionsTemp.map((col) => ({ ...col }));
  }

  isColumnChecked(field: string): boolean {
    return this.columnsSections.some((col) => col.id === field && col.checked);
  }

  // search
  keyword: string = '';
  selectedFields = ['CompanyName', 'Email'];
  sortBy = 'clientID';
  sortDirection = 'asc';
  pageIndex: number = 1;
  pageSize: number = 10;
  clients: any[] = [];
  contractExpiry = '';
  totalCount: number = 0;
  hasNextPage: boolean = true;
  selectedFilters: string[] = [];

  private dashboardService = inject(DashboardService);

  // handle from child: search combobox changes
  onSearchChange(searchRequest: SearchRequest) {
    this.keyword = searchRequest.keyword;
    this.selectedFields = this.mapFiltersToFields(searchRequest.filters);
    this.pageIndex = 1;
    this.search();
  }

  // map to backend filters
  private mapFiltersToFields(filterIds: string[]): string[] {
    const fieldMapping: { [key: string]: string } = {
      clientID: 'ClientID',
      companyName: 'CompanyName',
      tradingName: 'TradingName',
      contact: 'Contact',
      phone: 'Phone',
      mobile: 'Mobile',
      email: 'Email',
      contractEnd: 'ContractEnd',
      connections: 'Connections',
    };

    return filterIds.map((id) => fieldMapping[id]).filter((field) => field);
  }

  search(): void {
    const payload = {
      keyword: this.keyword,
      selectedFields: this.selectedFields,
      sortBy: this.sortBy,
      sortDirection: this.sortDirection,
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      selectedFilters: this.selectedFilters,
    };

    this.dashboardService.searchClients(payload).subscribe((res: any) => {
      console.log('Received response:', res);
      this.clients = res.items;
      this.pageIndex = res.pageIndex || 1;
      this.pageSize = res.pageSize || this.pageSize;
      this.totalCount = res.totalCount;
      this.hasNextPage = res.hasNextPage;
    });
  }

  // pagination
  onPaginationChange() {
    this.pageIndex = 1;
    console.log('Page size changed to:', this.pageSize);
    this.search();
  }

  prevPage() {
    if (this.pageIndex > 1) {
      this.pageIndex--;
      this.search();
    }
  }

  nextPage() {
    if (this.hasNextPage) {
      this.pageIndex++;
      this.search();
    }
  }

  getPageNumbers(): number[] {
    return getPageNumbers(this.pageIndex, this.totalCount, this.pageSize);
  }

  goToPage(page: number): void {
    if (page !== -1 && page !== this.pageIndex) {
      this.pageIndex = page;
      this.search();
    }
  }

  // contract end logic
  calculateContractExpiry(
    startDate: string | null,
    contractTermMonths: number | null
  ): Date | null {
    if (
      !startDate ||
      contractTermMonths === null ||
      contractTermMonths === undefined
    )
      return null;

    const date = new Date(startDate);
    date.setMonth(date.getMonth() + contractTermMonths);
    return date;
  }

  hasContractExpired(
    startDate: string | null,
    contractTermMonths: number | null
  ) {
    const expiryDate = this.calculateContractExpiry(
      startDate,
      contractTermMonths
    );
    if (expiryDate != null) {
      if (expiryDate <= new Date()) {
        return true;
      }
    }
    return false;
  }

  viewClient(client: any[]) {
    console.log('redirect to client view');
  }
}
