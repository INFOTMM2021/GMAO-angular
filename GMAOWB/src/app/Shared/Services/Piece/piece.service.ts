import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject, Observable, catchError } from 'rxjs';
import { Piece } from 'src/app/Models/Piece';

 const Url = 'https://localhost:5001/api/Pieces';

@Injectable({
  providedIn: 'root'
})
export class PieceService {

  Url = 'https://localhost:5001/api/Pieces';
  dataChange: BehaviorSubject<Piece[]> = new BehaviorSubject<Piece[]>([]);
  dialogData: any;
  id :string; 



  constructor(private http: HttpClient) {
  }
  
  get data(): Piece[] {
    return this.dataChange.value;
  }

  getDialogData() {
    return this.dialogData;
  }

  AddPiece(piece: Piece) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<any>(Url + '/AddPiece/', piece, httpOptions);
  }

  GetAllPieces(): Observable<Piece[]> {
    return this.http.get<Piece[]>(Url + '/GetAllPieces');
  }

  GetAllPiecesList(): Observable<any[]> {
    return this.http.get<any>(Url + '/GetAllPiecesToList');
  }


  DeletePiece(CodePiece: string) {
    return this.http.delete(this.Url+'/'+CodePiece);
  }


  GetPieceById(id: string) {
    return this.http.get<Piece>(Url + '/GetPieceById/'+ id);
  }

  UpdatePiece(piece : Piece) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.put<any>(Url + '/UpdatePiece/', piece, httpOptions);
  }

  GetDesignationcodePiece(){
    return this.http.get<string[]>(Url + '/GetAllDesignCodeP');
  }

   GetNumberOfPieces(cp:string){
  return this.http.get<number>(Url + '/CheckStockBeforeInsert/'+cp)
}

}


