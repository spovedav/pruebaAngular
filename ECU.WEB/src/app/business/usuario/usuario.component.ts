import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { PersonaService } from '../../core/services/persona.service';
import { PersonaListResponse } from '../../core/interfaces/persona/persona-list-response';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})

export default class UsuarioComponent implements OnInit {
  titulo: string = '';
  _personaData: PersonaListResponse | undefined

  constructor(
    private _personaService: PersonaService,
    private route: ActivatedRoute
  ) {
    this.titulo = this.route.snapshot.data['titulo'];
  }

  ngOnInit(): void {

    this._personaService.getAll().subscribe(data => {
      this._personaData = data;
      console.log(this._personaData);
    })
  }
}
