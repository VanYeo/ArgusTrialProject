import {
  Component,
  EventEmitter,
  inject,
  Input,
  Output,
  AfterViewInit,
  ElementRef,
  AfterViewChecked,
} from '@angular/core';
import { Router } from '@angular/router';

declare var bootstrap: any;

@Component({
  selector: 'app-sidebar',
  standalone: false,
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class SidebarComponent implements AfterViewChecked {
  @Input() opened: boolean = true;
  @Output() toggleSidebar = new EventEmitter<void>();

  private router = inject(Router);
  toolsDropdownOpen = false;

  private tooltipsInitialized = false;
  constructor(private el: ElementRef) {}

  ngAfterViewChecked() {
    // Only initialize tooltips if sidebar is collapsed and not already initialized
    if (!this.opened && !this.tooltipsInitialized) {
      this.initTooltips();
      this.tooltipsInitialized = true;
    }
    // Dispose tooltips if sidebar is expanded
    if (this.opened && this.tooltipsInitialized) {
      this.disposeTooltips();
      this.tooltipsInitialized = false;
    }
  }

  private initTooltips() {
    const tooltipEls = Array.from(
      this.el.nativeElement.querySelectorAll('[data-bs-toggle="tooltip"]')
    );
    tooltipEls.forEach((el: any) => {
      el._tooltipInstance = new bootstrap.Tooltip(el);
    });
  }

  private disposeTooltips() {
    const tooltipEls = Array.from(
      this.el.nativeElement.querySelectorAll('.nav-item')
    );
    tooltipEls.forEach((el: any) => {
      if (el._tooltipInstance) {
        el._tooltipInstance.dispose();
        el._tooltipInstance = null;
      }
      el.removeAttribute('data-bs-toggle');
      el.removeAttribute('title');
    });
  }

  onToggleSidebar() {
    this.toggleSidebar.emit();
  }

  toggleToolsDropdown() {
    if (this.opened) {
      this.toolsDropdownOpen = !this.toolsDropdownOpen;
    }
  }
}
