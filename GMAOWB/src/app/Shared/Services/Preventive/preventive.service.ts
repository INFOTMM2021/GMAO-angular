import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Preventive } from 'src/app/Models/Preventive';

const Url = 'https://localhost:5001/api/Preventives';
@Injectable({
  providedIn: 'root'
})
export class PreventiveService {
  Url = 'https://localhost:5001/api/Preventives';
  constructor(private http: HttpClient) { }


  GetAllPreventives(): Observable<Preventive[]> {
    return this.http.get<Preventive[]>(Url + '/GetAllPreventives');
  }

  DeletePreventive (codeInter:number){
    return this.http.delete(Url+'/'+codeInter);
  }

  UpdatePreventive(pre : Preventive) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url + '/UpdatePreventive/', pre, httpOptions);
  }

  AddPreventive(pre : Preventive) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddPreventive/',pre, httpOptions);
  }
  
  GetPreventiveById(id: string) {
    return this.http.get<Preventive>(Url + '/GetPreventiveById/'+ id);
  }
}
