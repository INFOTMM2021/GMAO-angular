import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CentreCout, ICentreCout } from 'src/app/Models/CentreCout';
import { createRequestOption } from '../../request-util';


type EntityResponseType = HttpResponse<ICentreCout>;
type EntityArrayResponseType = HttpResponse<Map<CentreCout, CentreCout[]>>;

type EntityArrayArrayResponseType = HttpResponse<Map<CentreCout[], CentreCout[]>>;

const Url = 'https://localhost:5001/api/CentreCouts';
@Injectable({
  providedIn: 'root'
})
export class CentreCoutService {

  

  constructor(private http: HttpClient ) {
  }

  public getDataList(req?: any): Observable<EntityArrayArrayResponseType> {
    const options = createRequestOption(req);
    return this.http
        .get<Map<CentreCout[], CentreCout[]>>(Url + '/ListeCentreCoutSeq', {params: options, observe: 'response'});
  }

  getData(req?: any): Observable<EntityArrayResponseType> {
    const options = createRequestOption(req);
    return this.http
        .get<Map<CentreCout, CentreCout[]>>(Url + '/ListeCentreCoutSeq', {params: options, observe: 'response'});
}

  AddCentreCout(cc: CentreCout) {
  const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
  return this.http.post<any>('https://localhost:5001/api/CentreCoutSeq/AddCentreCout/', cc, httpOptions);
}


  GetAllCentreCouts(): Observable<CentreCout[]> {
    return this.http.get<CentreCout[]>(Url + '/GetAllCentreCout');
  }

  GetAllCentreCoutsHT():Observable<CentreCout[]>{
    return this.http.get<CentreCout[]>(Url+'/ListeCentreCoutHT');
  }

  getlstchildren():Observable<any[]>{
    return this.http.get<any[]>(Url+'/getlstchildrens');
  }

  DeleteCentreCout(CodeCC :string){
    return this.http.delete(Url+'/'+CodeCC);
  }

}
