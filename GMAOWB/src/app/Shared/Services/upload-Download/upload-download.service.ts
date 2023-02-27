import { HttpClient, HttpEvent, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { DocumentEquipement } from 'src/app/Models/DocumentEquipement';
@Injectable({
  providedIn: 'root'
})
export class UploadDownloadService implements OnInit{
  private baseApiUrl: string;
  private apiDownloadUrl: string;
  private apiUploadUrl: string;
  private apiFileUrl: string;
private apiDocs:string;
  constructor(private httpClient: HttpClient) {
    this.baseApiUrl = 'https://localhost:5001/api/Upload/';
    this.apiDownloadUrl = this.baseApiUrl + 'download';
    this.apiUploadUrl = this.baseApiUrl + 'upload';
    this.apiFileUrl = this.baseApiUrl + 'GetAllFiles';
    this.apiDocs = this.baseApiUrl+'GetAllDocEquip';
  }
  ngOnInit(): void {
    
  }

  public downloadFile(file: string): Observable<HttpEvent<Blob>> {
    return this.httpClient.request(new HttpRequest('GET',`${this.apiDownloadUrl}?file=${file}`,null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }

  public uploadFile(file: Blob): Observable<HttpEvent<void>> {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.request(new HttpRequest('POST',this.apiUploadUrl,formData,{reportProgress: true}));
  }

  AddDocumentEquipement(de: DocumentEquipement) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.httpClient.post<DocumentEquipement>(this.baseApiUrl + 'addDocEquip/', de, httpOptions);
  }

  public getFiles(): Observable<string[]> {
    return this.httpClient.get<string[]>(this.apiFileUrl);
  }

  public getDocuments(): Observable<DocumentEquipement[]> {
    return this.httpClient.get<DocumentEquipement[]>(this.apiDocs);
  }

}
