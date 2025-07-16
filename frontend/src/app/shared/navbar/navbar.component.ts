import {
  Component,
  ElementRef,
  HostListener,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class NavbarComponent implements OnInit {
  isOpened = false;
  private router = inject(Router);
  pathSegments: string[] = [];
  userName: string = '';
  email: string = '';

  @ViewChild('dropdownRef') dropdownRef!: ElementRef;

  toggleDropdown(event: MouseEvent) {
    event.stopPropagation();
    this.isOpened = !this.isOpened;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const clickedInside = this.dropdownRef.nativeElement.contains(event.target);
    if (!clickedInside) {
      this.isOpened = false;
    }
  }

  ngOnInit(): void {
    const initialUrl = this.router.url;
    this.setPathSegments(initialUrl);

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        const currentUrl = event.urlAfterRedirects;
        this.setPathSegments(currentUrl);
      });

    const email = localStorage.getItem('email');
    this.email = email ? email : '';
    this.userName = email ? email.split('@')[0].charAt(0).toUpperCase() + email.split('@')[0].slice(1) : '';
  }

  private setPathSegments(url: string) {
    this.pathSegments = url
      .split('/')
      .filter((segment) => segment)
      .map(this.formatSegment);
  }

  formatSegment(segment: string): string {
    return segment
      .split('-')
      .map((word) => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  }
}
