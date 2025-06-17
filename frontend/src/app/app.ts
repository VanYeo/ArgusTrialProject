import { Component, OnInit } from '@angular/core';
import { HealthService } from './service/health-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected title = 'ArgusTrialProject';

  constructor(private healthService: HealthService) { }

  ngOnInit(): void {
    this.healthService.checkHealth().subscribe({
      next: (res) => console.log(' Health:', res),
      error: (err) => console.error('Health check failed:', err)
    });
  }
}
