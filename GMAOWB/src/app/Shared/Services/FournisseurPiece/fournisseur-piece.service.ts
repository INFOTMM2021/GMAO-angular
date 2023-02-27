import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FournisseurPiece } from 'src/app/Models/FournisseurPiece';

const Url = 'https://localhost:5001/api/FournisseurPieces';

@Injectable({
  providedIn: 'root'
})
export class FournisseurPieceService {

  Url = 'https://localhost:5001/api/FournisseurPieces';

  constructor(private http: HttpClient) {
  }

  AddFournisseurPiece(fournisseur: FournisseurPiece) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddFournisseur/', fournisseur, httpOptions);
  }

  GetAllFournisseurPieces(): Observable<FournisseurPiece[]> {
    return this.http.get<FournisseurPiece[]>(Url + '/GetAllFournisseurs');
  }

  DeleteFournisseurPiece(id: string): Observable<string> {
    return this.http.delete<string>(Url + '/Delete/?id=' + id);
  }

  GetFournisseurPieceById(id: string) {
    return this.http.get<FournisseurPiece>(Url + '/GetFournisseurById/?id=' + id);
  }

  UpdateFournisseurPiece(fournisseurpiece : FournisseurPiece) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<FournisseurPiece[]>(Url + '/EditFournisseurPiece/', fournisseurpiece, httpOptions);
  }

}
