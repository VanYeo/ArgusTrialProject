import {
  Component,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef,
  HostListener,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
export interface SearchFilter {
  id: string;
  label: string;
  checked: boolean;
}

export interface SearchRequest {
  keyword: string;
  filters: string[];
}

@Component({
  selector: 'app-search-combobox',
  standalone: false,
  templateUrl: './search-combobox.html',
  styleUrl: './search-combobox.scss',
})
export class SearchCombobox {
  @ViewChild('searchWrapper', { static: true }) searchWrapper!: ElementRef;
  @Input() columnOptions: any[] = [];
  @Output() searchChange = new EventEmitter<SearchRequest>();

  searchKeyword: string = '';
  isDropdownOpen: boolean = false;
  dropdownWidth: number = 0;
  filters: SearchFilter[] = [];

  ngOnInit() {
    // initialise filters from column options (excluding actions)
    this.filters = this.columnOptions
      .filter((column) => column.id !== 'actions')
      .map((column) => ({
        id: column.id,
        label: column.label,
        checked: column.checked,
      }));
  }
  ngAfterViewInit() {
    setTimeout(() => {
      this.updateDropdownWidth();
    });
  }

  @HostListener('window:resize')
  onWindowResize() {
    this.updateDropdownWidth();
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event) {
    const target = event.target as HTMLElement;
    if (!this.searchWrapper.nativeElement.contains(target)) {
      this.isDropdownOpen = false;
    }
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
    if (this.isDropdownOpen) {
      this.updateDropdownWidth();
    }
  }

  onSearchInput() {
    this.emitSearchChange();
  }

  onFilterChange() {
    this.emitSearchChange();
  }

  private updateDropdownWidth() {
    if (this.searchWrapper) {
      this.dropdownWidth = this.searchWrapper.nativeElement.offsetWidth;
    }
  }

  private emitSearchChange() {
    const checkedFilters = this.filters
      .filter((filter) => filter.checked)
      .map((filter) => filter.id);

    const searchRequest: SearchRequest = {
      keyword: this.searchKeyword.trim(),
      filters: checkedFilters,
    };

    this.searchChange.emit(searchRequest);
  }

  cancelInput() {
    this.searchKeyword = '';
    this.emitSearchChange();
  }

  get checkedFilterCount(): number {
    return this.filters.filter((f) => f.checked).length;
  }
}
