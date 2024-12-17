import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export default class DashboardComponent {
  titulo: string = '';

  constructor(private route: ActivatedRoute) {
    this.titulo = this.route.snapshot.data['titulo'];
  }
}
