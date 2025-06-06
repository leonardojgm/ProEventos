import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Lote } from '@app/models/Lote';
import { environment } from '@environments/environment';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class LoteService {
  baseURL = `${environment.apiURL}api/lote`;
  
  constructor(private http: HttpClient) { }

  public getLotesByEventoId(eventoId: number): Observable<Lote[]> {
    return this.http
      .get<Lote[]>(`${this.baseURL}/${eventoId}`)
      .pipe(take(1));
  }

  public saveLote(eventoId: number, lotes: Lote[]): Observable<Lote> {
    return this.http
      .put<Lote>(`${this.baseURL}/${eventoId}`, lotes)
      .pipe(take(1));
  }

  public deleteLote(eventoId: number, loteId: number): Observable<any> {
    return this.http
      .delete<any>(`${this.baseURL}/${eventoId}/${loteId}`)
      .pipe(take(1));
  }
}
