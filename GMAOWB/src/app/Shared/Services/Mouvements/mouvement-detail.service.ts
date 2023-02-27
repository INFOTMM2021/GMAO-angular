import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MouvementDetail } from 'src/app/Models/MouvementDetail';

const Url = 'https://localhost:5001/api/MouvementDetails';


@Injectable({
  providedIn: 'root'
})

export class MouvementDetailService {

  constructor(private http: HttpClient) { }

  AddMouvementDetail(mouvementDetail: MouvementDetail) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddMouvementDetail/', mouvementDetail, httpOptions);
  }

  AddMouvementDetailRegul(mouvementDetail: MouvementDetail) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddMouvementDetailRegul/', mouvementDetail, httpOptions);
  }

  GetAllMouvementDetails(): Observable<MouvementDetail[]> {
    return this.http.get<MouvementDetail[]>(Url + '/GetAllMouvementDetail');
  }

  DeleteMouvementDetail(NumMvt: number): Observable<number> {
    return this.http.delete<number>(NumMvt + '/DeleteMouvemenDetail/?NumMvt=' + NumMvt);
  }
  
}
