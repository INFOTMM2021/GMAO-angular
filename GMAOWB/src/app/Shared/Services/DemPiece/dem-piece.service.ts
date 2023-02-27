import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DemPiece } from 'src/app/Models/DemPiece';


const Url = 'https://localhost:5001/api/DemandePieces';

@Injectable({
  providedIn: 'root'
})
export class DemPieceService {

  constructor(private http: HttpClient) { }

  AddDemPiece(dempiece: DemPiece) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddDemPiece/', dempiece, httpOptions);
  }

  GetAllPiecesByNumDem(numdem :number):Observable<DemPiece[]>{
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.get<any>(Url + '/GetAllPiecesByNumDem/?numDem='+numdem,httpOptions);
  }

  MAJPiece(qtePiece:number, cp :string):Observable<any>{
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url + '/MAJPiece/', {qtePiece, cp},httpOptions);
  }

}
