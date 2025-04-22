import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RedeSocial } from '@app/models/RedeSocial';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RedeSocialService {
  baseURL = environment.apiURL + 'api/redeSocial';

  constructor(
    private http: HttpClient
  ) { }

  /**
   * 
   * @param origem Precisa passar a palavra 'palestrante' ou 'evento' - Escrito em minúsculo.
   * @param id Precisa passar o PalestranteId ou o EventoId dependendo da sua Origem.
   * @returns Obervable<RedeSocial[]>
   */
  public getRedesSociais(origem: string, id: number): Observable<RedeSocial[]> {
    let URL = 
      id === 0 
        ? `${this.baseURL}/${origem}` 
        : `${this.baseURL}/${origem}/${id}`;

    return this.http.get<RedeSocial[]>(URL).pipe(take(1));
  }

  /**
   * 
   * @param origem Precisa passar a palavra 'palestrante' ou 'evento' - Escrito em minúsculo.
   * @param id Precisa passar o PalestranteId ou o EventoId dependendo da sua Origem.
   * @param redesSocial Precisa adicionar Redes Sociais organizadas em RedeSocial[].
   * @returns Obervable<RedeSocial[]>
   */
  public saveRedesSociais(origem: string, id: number, redesSociais: RedeSocial[]): Observable<RedeSocial[]> {
    let URL = 
      id === 0 
        ? `${this.baseURL}/${origem}` 
        : `${this.baseURL}/${origem}/${id}`;

    return this.http.put<RedeSocial[]>(URL, redesSociais).pipe(take(1));
  }

  /**
   * 
   * @param origem Precisa passar a palavra 'palestrante' ou 'evento' - Escrito em minúsculo.
   * @param id Precisa passar o PalestranteId ou o EventoId dependendo da sua Origem.
   * @param redesSocialId Precisa usar o id da Rede Social.
   * @returns Obervable<any> - Pois é o retorno da Rota
   */
  public deleteRedeSocial(origem: string, id: number, redesSocialId: number): Observable<any> {
    let URL = 
      id === 0 
        ? `${this.baseURL}/${origem}/${redesSocialId}` 
        : `${this.baseURL}/${origem}/${id}/${redesSocialId}`;

    return this.http.delete(URL).pipe(take(1));
  }
}
