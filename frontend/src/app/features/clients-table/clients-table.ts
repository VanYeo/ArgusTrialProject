import {
  Component,
  EventEmitter,
  HostListener,
  Input,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Client } from '../view-client/client-model';
import {
  calculateContractExpiry,
  hasContractExpired,
} from '../../service/contract-end-calculator';
import { getPageNumbers } from '../../service/pagination';

@Component({
  selector: 'app-clients-table',
  standalone: false,
  templateUrl: './clients-table.html',
  styleUrl: './clients-table.scss',
})
export class ClientsTable {
  @Input() clients: Client[] = [];
  @Input() pageIndex: number = 1;
  @Input() pageSize!: number;
  @Input() totalCount!: number;
  @Input() hasNextPage: boolean = false;
  @Input() columnsSections: any[] = [];

  @Output() pageChanged = new EventEmitter<number>();
  @Output() pageSizeChanged = new EventEmitter<number>();
  @Output() clientSelected = new EventEmitter<number>();
  Math = Math;
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['pageIndex']) {
      console.log(
        '[ClientsTableComponent] pageIndex changed to:',
        this.pageIndex
      );
    }
  }

  isColumnChecked(field: string): boolean {
    const isChecked = this.columnsSections.some(
      (column) => column.id === field && column.checked
    );
    return isChecked;
  }

  viewClientDetails(id: number) {
    this.clientSelected.emit(id);
  }

  goToPage(page: number) {
    this.pageChanged.emit(page);
  }

  prevPage() {
    if (this.pageIndex > 1) this.pageChanged.emit(this.pageIndex - 1);
  }

  nextPage() {
    if (this.hasNextPage) this.pageChanged.emit(this.pageIndex + 1);
  }

  getPageNumbers(): number[] {
    return getPageNumbers(this.pageIndex, this.totalCount, this.pageSize);
  }

  onPaginationChange() {
    console.log('[ClientsTableComponent] Page size changed:', this.pageSize);
    this.pageSizeChanged.emit(this.pageSize);
  }

  // contract expiry logic
  onCalculateContractExpiry(
    startDate: string,
    contractTerm: string,
    customValue: number
  ) {
    return calculateContractExpiry(startDate, contractTerm, customValue);
  }

  onHasContractExpired(
    startDate: string,
    contractTerm: string,
    customValue: number
  ) {
    return hasContractExpired(startDate, contractTerm, customValue);
  }

  showPageSizeDropdown = false;

  togglePageSizeDropdown() {
    this.showPageSizeDropdown = !this.showPageSizeDropdown;
  }

  selectPageSize(size: number) {
    if (this.pageSize !== size) {
      this.pageSize = size;
      this.pageSizeChanged.emit(this.pageSize);
    }
    this.showPageSizeDropdown = false;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const clickedInside = (event.target as HTMLElement).closest(
      '.custom-select-wrapper'
    );
    if (!clickedInside) {
      this.showPageSizeDropdown = false;
    }
  }
}
