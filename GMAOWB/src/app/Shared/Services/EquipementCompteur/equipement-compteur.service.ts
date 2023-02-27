import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { EquipementCompteur } from 'src/app/Models/EquipementCompteur';

const Url = 'https://localhost:5001/api/EquipementCompteurs';

@Injectable({
  providedIn: 'root'
})
export class EquipementCompteurService {
  Url = 'https://localhost:5001/api/EquipementCompteurs';
  dataChange: BehaviorSubject<EquipementCompteur[]> = new BehaviorSubject<EquipementCompteur[]>([]);
  dialogData: any;
  id :string; 
  constructor(private http: HttpClient) { }


    AddEquipementCompteur(EqCompteur: EquipementCompteur) {
      const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
      return this.http.post<any>(Url + '/AddEqCompteur/', EqCompteur, httpOptions);
    }
  
    GetAllEqCompteurs(): Observable<EquipementCompteur[]> {
      return this.http.get<EquipementCompteur[]>(Url + '/GetAllEqCompteurs');
    }
  
  
    DeleteEqCompteur(CodeCompteur: string) {
      return this.http.get(this.Url + '/DeleteEqCompteur/?CodeCompteur='+ CodeCompteur.toString());
    }
  
  
    GetEqCompteursById(id: string) {
      return this.http.get<EquipementCompteur>(Url + '/GetEqCompteursById/' + id);
    }
  
    UpdateEqCompteur(EqCompteur : EquipementCompteur) {
      const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
      return this.http.post<EquipementCompteur[]>(Url + '/UpdateEqCompteur/', EqCompteur, httpOptions);
    }



  }

