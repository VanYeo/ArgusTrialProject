import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { HealthService } from './service/health-service';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss',
})
export class App implements OnInit {
  protected title = 'ArgusTrialProject';
  private healthService = inject(HealthService);
  private router = inject(Router);
  private breakpointObserver = inject(BreakpointObserver);

  hideLayout = false;
  opened = true;

  ngOnInit(): void {
    this.healthService.checkHealth().subscribe({
      next: (res) => console.log('Health:', res),
      error: (err) => console.error('Health check failed:', err),
    });

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        const hiddenRoutes = ['/login', '/forgot-password'];
        this.hideLayout = hiddenRoutes.includes(event.urlAfterRedirects);
      });

    this.breakpointObserver
      .observe([Breakpoints.Medium, Breakpoints.Small, Breakpoints.XSmall])
      .subscribe((result) => {
        this.opened = !result.matches;
      });
  }

  toggleSidebar() {
    this.opened = !this.opened;
    console.log('Sidebar opened:', this.opened);
  }
}
