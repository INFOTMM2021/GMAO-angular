import { Component, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Piece } from 'src/app/Models/Piece';

import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';
import { Observable } from 'rxjs';
import { CentreCoutService } from 'src/app/Shared/Services/CentreCout/centre-cout.service';
import { CentreCoutComponent } from '../centre-cout/centre-cout.component';
import { SharedServicesService } from 'src/app/Shared/Services/SharedServices/shared-services.service';




@Component({
  selector: 'app-piece',
  templateUrl: './piece.component.html',
  styleUrls: ['./piece.component.css']
})

export class PieceComponent implements OnInit {
  
  @Input() CCDesignation: any;
 

  public AddPiece: FormGroup;
  piece?: Observable<Piece[]>;
  PieceIdUpdate = '';
  massage!: string;
  dataSaved = false;
  selectedCentreCout: any;
  

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private PS: PieceService,
    private route: ActivatedRoute,
    private CCS: CentreCoutService,
    private sharedServ: SharedServicesService) { }

    NavigatePieceToCC() {
      
    this.router.navigate(['/CCList']).then();
  }

  getItemSelected(CCDesignation: Node) {
    this.selectedCentreCout = CentreCoutComponent.arguments.getItemSelected(CCDesignation);
    this.AddPiece.controls['CentreCout'].setValue(CCDesignation);
  }

  InsertPiece(piece: Piece) {
    
    if (this.PieceIdUpdate !== '') {
      piece.CodePiece = this.PieceIdUpdate;
    }
    this.PS.AddPiece(piece).subscribe(
      () => {
        if (this.PieceIdUpdate === '') {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add success');
        this.dataSaved = true;
        this.router.navigate(['/listPieces']).then();
      });
  }

  ngOnInit() {
    this.CCDesignation = this.sharedServ.getDesignation();
    this.AddPiece = new FormGroup({
      CodePiece: new FormControl(),
      Designation: new FormControl(),
      AnCode: new FormControl(),
      Unite: new FormControl(),
      DateAchat: new FormControl(),
      DateFabrication: new FormControl(),
      Marque: new FormControl(),
      Machine: new FormControl(),
      NumSerie: new FormControl(),
      CentreCout: new FormControl(this.CCDesignation!),
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
      Zstockage: new FormControl(),
      IF_CC: new FormControl(),
    });
    const Id = this.route.snapshot.queryParams['CodePiece'];
    if (Id != null) {
      this.PieceEdit(Id);
    }
  }


  PieceEdit(id: string) {
    this.PS.GetPieceById(id).subscribe(p => {
      this.massage = "";
      this.dataSaved = false;
      this.PieceIdUpdate = id;
      if (p) {
        this.AddPiece.controls['CodePiece'].setValue(p.CodePiece);
        this.AddPiece.controls['Designation'].setValue(p.Designation);
        this.AddPiece.controls['AnCode'].setValue(p.AnCode);
        this.AddPiece.controls['Unite'].setValue(p.Unite);
        this.AddPiece.controls['DateAchat'].setValue(p.DateAchat);
        this.AddPiece.controls['DateFabrication'].setValue(p.DateFabrication);
        this.AddPiece.controls['Marque'].setValue(p.Marque);
        this.AddPiece.controls['Machine'].setValue(p.Machine);
        this.AddPiece.controls['NumSerie'].setValue(p.NumSerie);
        this.AddPiece.controls['CentreCout'].setValue(p.CentreCout);
        this.AddPiece.controls['QteStock'].setValue(p.QteStock);
        this.AddPiece.controls['StockAlerte'].setValue(p.StockAlerte);
        this.AddPiece.controls['QteCmd'].setValue(p.QteCmd);
        this.AddPiece.controls['RefFournisseur'].setValue(p.RefFournisseur);
        this.AddPiece.controls['PAchat'].setValue(p.PAchat);
      }
      ;
    })
  }

  onFormSubmit() {
    const p = this.AddPiece.value;
    this.InsertPiece(p);
  }

  ClearForm() {
    this.AddPiece.controls['CodePiece'].setValue('');
    this.AddPiece.controls['Designation'].setValue('');
    this.AddPiece.controls['AnCode'].setValue('');
    this.AddPiece.controls['Unite'].setValue('');
    this.AddPiece.controls['DateAchat'].setValue('');
    this.AddPiece.controls['DateFabrication'].setValue('');
    this.AddPiece.controls['Marque'].setValue('');
    this.AddPiece.controls['Machine'].setValue('');
    this.AddPiece.controls['NumSerie'].setValue('');
    this.AddPiece.controls['CentreCout'].setValue('');
    this.AddPiece.controls['QteStock'].setValue('');
    this.AddPiece.controls['StockAlerte'].setValue('');
    this.AddPiece.controls['QteCmd'].setValue('');
    this.AddPiece.controls['RefFournisseur'].setValue('');
    this.AddPiece.controls['PAchat'].setValue('');
    this.AddPiece.controls['Zstockage'].setValue('');
  }

}




