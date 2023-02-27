import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DemTravail } from 'src/app/Models/DemTravail';
import { Intervention } from 'src/app/Models/Intervention';


const Url = 'https://localhost:5001/api/Interventions';
@Injectable({
  providedIn: 'root'
})
export class InterventionService {

  constructor(private http: HttpClient) { }


  GetAllInterventions(): Observable<Intervention[]> {
    return this.http.get<Intervention[]>(Url + '/GetAllInterv');
  }

  DeleteIntervention (codeInter:number){
    return this.http.delete(Url+'/'+codeInter);
  }

  UpdateIntervention(interv : Intervention) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url + '/UpdateIntervention/', interv, httpOptions);
  }

  AddIntervention(interv : Intervention) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddIntervention/', interv, httpOptions);
  }

  GetAllInterventionsNumDem(numdem :number):Observable<Intervention[]>{
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.get<any>(Url + '/GetAllIntervByNumDem/?numDem='+numdem,httpOptions);
  }

}
