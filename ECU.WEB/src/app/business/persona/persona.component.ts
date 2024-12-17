import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { PersonaService } from '../../core/services/persona.service';
import { PersonaListResponse } from '../../core/interfaces/persona/persona-list-response';
import { ActivatedRoute } from '@angular/router';
import { Persona } from '../../core/interfaces/persona/persona';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-persona',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './persona.component.html',
  styleUrl: './persona.component.css'
})

export default class PersonaComponent implements OnInit {
  jsonPersona: string = '';
  personas: Persona[] = [];
  personaSeleccionada: Persona | null = null;

  titulo: string = '';
  _personaData: PersonaListResponse | undefined

  constructor(
    private _personaService: PersonaService,
    private route: ActivatedRoute
  ) {
    this.titulo = this.route.snapshot.data['titulo'];
  }

  ngOnInit(): void {
    this.cargarPersonas();
    this._personaService.getAll().subscribe(data => {
      this._personaData = data;
      console.log(this._personaData);
    })
  }

  cargarPersonas(): void {
    this._personaService.getAll().subscribe(response => {
      if (!response.tieneError) {
        this.personas = response.result;
        this.jsonPersona = JSON.stringify(this.personas, null, 2);
      } else {
        console.error('Error al cargar las personas:', response.mensaje);
      }
    });
  }

  editarPersona(persona: Persona): void {
    this.personaSeleccionada = { ...persona }; // Hacemos una copia para editar
  }

  guardarPersona(): void {
    if (this.personaSeleccionada) {
      this._personaService.editarPersona(this.personaSeleccionada).subscribe(response => {
        if (!response.TieneError) {
          console.log('Persona editada correctamente');
          this.cargarPersonas();
          this.personaSeleccionada = null;
        } else {
          console.error('Error al editar la persona:', response.Mensaje);
        }
      });
    }
  }


  eliminarPersona(idPersona: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta persona?')) {
      this._personaService.eliminarPersona(idPersona).subscribe(response => {
        if (!response.TieneError) {
          console.log('Persona eliminada correctamente');
          this.cargarPersonas(); // Volver a cargar las personas
        } else {
          console.error('Error al eliminar la persona:', response.Mensaje);
        }
      });
    }
  }
}
