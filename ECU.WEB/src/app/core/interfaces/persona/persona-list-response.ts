import { Persona } from "./persona";

export interface PersonaListResponse {
  tieneError: boolean,
  result: Persona[],
  mensaje: string
}
