import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ECU.WEB';

  constructor(private authService: AuthService) {

  }

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.authService.autoRefreshToken()
    }
  }

}
