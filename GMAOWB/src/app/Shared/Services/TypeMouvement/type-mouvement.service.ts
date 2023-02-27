import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TypeMouvement } from 'src/app/Models/TypeMouvement';

const Url = 'https://localhost:5001/api/TypeMouvement';
@Injectable({
  providedIn: 'root'
})

export class TypeMouvementService {

  constructor(private http: HttpClient) { }

  GetAllTypeMouvement(): Observable<TypeMouvement[]> {
    return this.http.get<TypeMouvement[]>(Url + '/GetAllTypeMouvementToList');
  }
}
