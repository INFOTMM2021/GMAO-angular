import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Piece } from 'src/app/Models/Piece';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';
import { AddComponent } from '../add/add.component';



@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent  implements OnInit{
  public updatefrm: FormGroup;

  constructor( public dialogRef: MatDialogRef<AddComponent>, 
              @Inject(MAT_DIALOG_DATA) public data: Piece, 
              public dataService: PieceService , private router: Router) { }



  ngOnInit(): void {
    
    this.updatefrm = new FormGroup ({
      CodePiece: new FormControl(),
      Designation: new FormControl(),
      AnCode: new FormControl(),
      Unite: new FormControl(),
      DateAchat: new FormControl(),
      DateFabrication: new FormControl(),
      Marque: new FormControl(),
      Machine: new FormControl(),
      NumSerie: new FormControl(),
      CentreCout: new FormControl(),
      QteStock: new FormControl(),
      StockAlerte: new FormControl(),
      QteCmd: new FormControl(),
      PAchat: new FormControl(),
      CodeFournisseur: new FormControl(),
      Analyse1: new FormControl(),
      Analyse2: new FormControl(),
      Analyse3: new FormControl(),
    
    });
  }




  onFormSubmit() {
    //this.confirmAdd();
    this.dataService.UpdatePiece(this.data);  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  stopEdit(): void {
    this.dataService.UpdatePiece(this.data);
    this.router.navigate(['/listPieces']).then();
  }



}