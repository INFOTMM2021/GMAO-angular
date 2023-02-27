import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Piece } from 'src/app/Models/Piece';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent  implements OnInit{
  public AddPiece2: FormGroup;

  constructor(public dialogRef: MatDialogRef<AddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Piece,
    public dataService: PieceService) { }


formControl = new FormControl('', [
Validators.required
// Validators.email,
]);

getErrorMessage() {
return this.formControl.hasError('required') ? 'Required field' :
this.formControl.hasError('email') ? 'Not a valid email' :
'';
}

onFormSubmit() {
  this.confirmAdd();
}

ngOnInit(){
  this.AddPiece2 = new FormGroup ({
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
    RefFournisseur: new FormControl(),
    PAchat: new FormControl(),
    Obsolete: new FormControl(),
    CodeFournisseur: new FormControl(),
    CodeCC: new FormControl(),
    Analyse1: new FormControl(),
    Analyse2: new FormControl(),
    Analyse3: new FormControl(),
    ZStockage: new FormControl(),
    IF_CC: new FormControl(),
  });
}

onNoClick(): void {
this.dialogRef.close();
}

public confirmAdd(): void {
this.dataService.AddPiece(this.data);
}
}