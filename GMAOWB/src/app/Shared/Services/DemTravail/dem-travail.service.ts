import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { DemTravail } from 'src/app/Models/DemTravail';


const Url = 'https://localhost:5001/api/DemandeTravails';
@Injectable({
  providedIn: 'root'
})
export class DemTravailService {

  
  dataChange: BehaviorSubject<DemTravail[]> = new BehaviorSubject<DemTravail[]>([]);
  dialogData: any;
  id :string; 
  
  constructor(private http: HttpClient) { }


  GetAllDemTravail(): Observable<DemTravail[]> {
    return this.http.get<DemTravail[]>(Url + '/GetAllDemandesTravails');
  }

  DeleteDemTravail(Numdem: number) {
    return this.http.delete(Url+'/'+Numdem);
  }

  AddDemTravail(demtrav: DemTravail) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddDemTravail/', demtrav, httpOptions);
  }

  UpdateDemTravail(demtrav : DemTravail) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url + '/UpdateDemTravail/',demtrav , httpOptions);
  }

  UpdateDemTravailRecap(demtrav : DemTravail) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url + '/UpdateDemTravailRecap/',demtrav , httpOptions);
  }
}
