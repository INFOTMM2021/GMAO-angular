import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MvtArt } from 'src/app/Models/MvtArt';
const Url = 'https://localhost:5001/api/MvtArts';
@Injectable({
  providedIn: 'root'
})
export class MvtArtService {

  constructor(private http: HttpClient) { }


  AddMvtArt(mvtArt: MvtArt) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddMvtArtP/', mvtArt, httpOptions);
  }

  GetAllMvtArt(): Observable<MvtArt[]> {
    return this.http.get<MvtArt[]>(Url + '/GetAllMvtArts');
  }

  DeleteMvtArt(NumMvt: number): Observable<number> {
    return this.http.delete<number>(NumMvt + '/DeleteMvtArt/?NumMvt=' + NumMvt);
  }
  
}
