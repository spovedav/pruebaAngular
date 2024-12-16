import { Persona } from "./persona";

export interface PersonaListResponse {
  TieneError: boolean,
  Persona: Persona[],
  Mensaje: string
}
