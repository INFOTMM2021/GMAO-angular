import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { DocumentEquipement } from 'src/app/Models/DocumentEquipement';
import { Piece } from 'src/app/Models/Piece';
import { ProgressStatus, ProgressStatusEnum } from 'src/app/Models/ProgressStatutEnum';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';
import { UploadDownloadService } from 'src/app/Shared/Services/upload-Download/upload-download.service';


@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
  @Input() public disabled: boolean;
  @Output() public uploadStatus: EventEmitter<ProgressStatus>;
  @ViewChild('inputFile', {static: false}) inputFile: ElementRef;
  CodeEquipList ?: Observable<Piece[]> ;
  DocEquipIdUpdate = '';
  massage!: string;
  dataSaved = false;
  public uploadF: FormGroup;

  constructor(private service: UploadDownloadService, private CCS: PieceService, private router: Router) {
    this.uploadStatus = new EventEmitter<ProgressStatus>();
  }
  ngOnInit(): void {
    this.LoadCentreCout();
    this.uploadF = new FormGroup({
      CodeDocument: new FormControl(),
      CodeEquipement: new FormControl(),
      Description: new FormControl(),
      ExtentionFile: new FormControl()
    });
  }

  public InsertDocumentequipement(DE: DocumentEquipement) {
   
    if (this.DocEquipIdUpdate !== '') {
      DE.CodeDocument = this.DocEquipIdUpdate;
    }
    this.service.AddDocumentEquipement(DE).subscribe(
      () => {
        if (this.DocEquipIdUpdate === '') {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add succes');
        this.dataSaved = true;
        //this.router.navigate(['/listFiles']).then();
      });
  }

  onFormSubmit() {
    const p = this.uploadF.value;
    this.InsertDocumentequipement(p);
  }

  public uploadFile(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadStatus.emit({status: ProgressStatusEnum.START});
      this.service.uploadFile(file).subscribe(
        data => {
          if (data) {
            switch (data.type) {
              case HttpEventType.UploadProgress:
                this.uploadStatus.emit({status: ProgressStatusEnum.IN_PROGRESS, percentage: Math.round((data.loaded/ 1) * 100)});
                break;
              case HttpEventType.Response:
                this.inputFile.nativeElement.value = '';
                this.uploadStatus.emit({status: ProgressStatusEnum.COMPLETE});
                break;
            }
          }
        },
        error => {
          this.inputFile.nativeElement.value = '';
          this.uploadStatus.emit({status: ProgressStatusEnum.ERROR});
        }
      );
    }
    
  }


  FileList(){
    this.router.navigate(['/listFiles']).then();
  }
  
  LoadCentreCout(){
    return this.CodeEquipList =  this.CCS.GetAllPieces();
  }


}







  
