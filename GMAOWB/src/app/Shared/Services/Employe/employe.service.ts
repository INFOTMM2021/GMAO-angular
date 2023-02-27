import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employe } from 'src/app/Models/Employe';
    const Url = 'https://localhost:5001/api/Employes';
@Injectable({
  providedIn: 'root'
})
export class EmployeService {


  constructor(private http: HttpClient) { }


  GetAllEmployes(): Observable<Employe[]> {
    return this.http.get<Employe[]>(Url + '/GetAllEmployesActif');
  }

  GetAllEmployesList(): Observable<any[]> {
    return  this.http.get<any>(Url + '/GetAllEmployesToList');
  }

  Deactiver(emp :Employe){
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url+'/UpdateActif',emp.Matricule,httpOptions);
  }
  
}
