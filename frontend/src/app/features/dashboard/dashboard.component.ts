import { Component, inject, OnDestroy, OnInit } from '@angular/core';
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
import {
  calculateContractExpiry,
  hasContractExpired,
} from '../../service/contractDate';
import { ViewClientService } from '../view-client/view-client.service';
import { Client } from '../view-client/client-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class DashboardComponent implements OnInit, OnDestroy {
  Math = Math;
  filterDropdownOpen = false;
  columnsDropdownOpen = false;
  openForm = false;
  selectedClient: any = null;
  isEditMode = false;
  showForm = false;

  onAddClient() {
    this.selectedClient = null;
    this.isEditMode = false;
    this.showForm = true;
  }

  onEditClient(client: any) {
    this.selectedClient = client;
    this.isEditMode = true;
    this.showForm = true;
  }

  constructor(
    private dashboardService: DashboardService,
    private router: Router,
    private viewClientService: ViewClientService
  ) {}

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
  }

  opened = true;
  selectAllCheckedFilters = true;
  filterSections: FilterSection[] = [
    {
      title: 'Type',
      options: [
        { id: 'active', label: 'Active', checked: true },
        { id: 'inactive', label: 'Inactive', checked: true },
      ],
    },
  ];

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

  keyword: string = '';
  selectedFields = ['CompanyName', 'Email'];
  sortBy = 'clientID';
  sortDirection = 'asc';
  pageIndex = 1;
  pageSize = 10;
  totalCount = 0;
  hasNextPage = true;
  clients: Client[] = [];
  selectedFilters: string[] = [];

  onSearchChange(searchRequest: SearchRequest) {
    this.keyword = searchRequest.keyword;
    this.selectedFields = this.mapFiltersToFields(searchRequest.filters);
    this.pageIndex = 1;
    this.search();
  }

  private mapFiltersToFields(filterIds: string[]): string[] {
    const fieldMapping: { [key: string]: string } = {
      clientID: 'ClientID',
      companyName: 'CompanyName',
      tradingName: 'TradingName',
      contact: 'Contact',
      phone: 'Phone',
      mobile: 'Mobile',
      email: 'loginEmail',
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

    console.log('[DashboardComponent] Sending search payload:', payload);

    this.dashboardService.searchClients(payload).subscribe((res: any) => {
      this.clients = res.items;
      this.pageIndex = res.pageIndex || 1;
      this.pageSize = res.pageSize || this.pageSize;
      this.totalCount = res.totalCount;
      this.hasNextPage = res.hasNextPage;

      console.log('[DashboardComponent] Updated clients:', this.clients);
    });
  }

  onPaginationChange(newSize: number): void {
    console.log('[DashboardComponent] Page size changed to:', newSize);
    this.pageSize = newSize;
    this.pageIndex = 1;
    this.search();
  }

  onPageChange(newPage: number) {
    this.pageIndex = newPage;
    this.search();
  }

  onViewClientDetails(clientId: number) {
    this.router.navigate(['/clients', clientId]);
  }

  onCloseForm() {
    this.showForm = false;
    this.selectedClient = null;
    this.isEditMode = false;
  }
}
