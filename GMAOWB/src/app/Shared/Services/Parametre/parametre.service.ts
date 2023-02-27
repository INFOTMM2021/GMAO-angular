import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
const Url = 'https://localhost:5001/api/Parametres';


@Injectable({
  providedIn: 'root'
})
export class ParametreService {

  constructor(private http: HttpClient) { }

  getMaxPrelevNumber(){
    return this.http.get<any>(Url + '/GetMaxPrelev',httpOptions);
  }

  updateParametre(){
    return this.http.put<any>(Url+'/UpdatePrelev/',httpOptions);
  }

}
