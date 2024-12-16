import { Persona } from "./persona";

export interface PersonaResponse {
  TieneError: boolean,
  Persona: Persona,
  Mensaje: string
}
