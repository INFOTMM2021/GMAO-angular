import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
const Url = 'https://localhost:5001/api/Rapports';
const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Injectable({
  providedIn: 'root'
})

export class RapportService {

  constructor(private httpClient: HttpClient) { }

  getDemandeTravailReport(): Observable<any> {

    return this.httpClient.get(Url+'/DemandeTravailRapport',{responseType: "blob"});
  }

}
