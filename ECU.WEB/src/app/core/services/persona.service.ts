import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { PersonaListResponse } from '../interfaces/persona/persona-list-response';
import { _urlApiAdmim } from '../../endpoint/const';
import { Persona } from '../interfaces/persona/persona';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  private PERSONA_URL = _urlApiAdmim + 'persona/';

  constructor(private httpClient: HttpClient, private router: Router) { }

  getAll(): Observable<PersonaListResponse> {

    return this.httpClient.get<PersonaListResponse>(this.PERSONA_URL + 'list').pipe(
      tap(response => {
        if (response) {
          console.log(response);
        }
      })
    )
  }

  // Editar una persona
  editarPersona(persona: Persona): Observable<ResponseApi> {
    return this.httpClient.put<ResponseApi>(`${this.PERSONA_URL}update`, persona);
  }

  // Eliminar una persona
  eliminarPersona(idPersona: number): Observable<ResponseApi> {
    return this.httpClient.delete<ResponseApi>(`${this.PERSONA_URL}/${idPersona}`);
  }


}
