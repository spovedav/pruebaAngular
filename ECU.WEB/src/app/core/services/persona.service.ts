import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { PersonaListResponse } from '../interfaces/persona/persona-list-response';
import { _urlApiAdmim } from '../../endpoint/const';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  private PERSONA_URL = _urlApiAdmim + 'persona/';

  constructor(private httpClient: HttpClient, private router: Router) { }

  getAll(): Observable<PersonaListResponse> {

    return this.httpClient.get<PersonaListResponse>(this.PERSONA_URL + 'getAll').pipe(
      tap(response => {
        if (response) {
          console.log(response);
        }
      })
    )
  }

}
