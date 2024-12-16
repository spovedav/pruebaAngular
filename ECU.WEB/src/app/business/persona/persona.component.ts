import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { PersonaService } from '../../core/services/persona.service';
import { PersonaListResponse } from '../../core/interfaces/persona/persona-list-response';

@Component({
  selector: 'app-persona',
  standalone: true,
  imports: [],
  templateUrl: './persona.component.html',
  styleUrl: './persona.component.css'
})

export default class PersonaComponent implements OnInit {

  _personaData: PersonaListResponse | undefined

  constructor(
    private _personaService: PersonaService,
  ) { }

  ngOnInit(): void {
    this._personaService.getAll().subscribe(data => {
      this._personaData = data;
      console.log(this._personaData);
    })
  }
}
