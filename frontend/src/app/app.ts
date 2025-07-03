import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { HealthService } from './service/health-service';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected title = 'ArgusTrialProject';
  private healthService = inject(HealthService)
  private router = inject(Router)

  hideLayout = false;
  opened = true;

  ngOnInit(): void {
    this.healthService.checkHealth().subscribe({
      next: (res) => console.log('Health:', res),
      error: (err) => console.error('Health check failed:', err)
    });

    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.hideLayout = event.urlAfterRedirects === '/login';
      });
  }

  toggleSidebar() {
    this.opened = !this.opened;
    console.log('Sidebar opened:', this.opened);
  }
}
