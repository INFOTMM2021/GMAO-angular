import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Mouvement } from 'src/app/Models/Mouvement';

const Url = 'https://localhost:5001/api/Mouvement';
@Injectable({
  providedIn: 'root'
})
export class MouvementService {

  constructor(private http: HttpClient) { }

  AddMouvement(mouvement: Mouvement) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddMouvement/', mouvement, httpOptions);
  }

  GetAllMouvements(): Observable<Mouvement[]> {
    return this.http.get<Mouvement[]>(Url + '/GetAllMouvements');
  }

  DeleteMouvement(NumMvt: number): Observable<number> {
    return this.http.delete<number>(NumMvt + '/DeleteMouvement/?NumMvt=' + NumMvt);
  }
}
