import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DemPiece } from 'src/app/Models/DemPiece';
import { DemTravail } from 'src/app/Models/DemTravail';
import { DemPieceService } from 'src/app/Shared/Services/DemPiece/dem-piece.service';
import { DemTravailService } from 'src/app/Shared/Services/DemTravail/dem-travail.service';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';

@Component({
  selector: 'app-add-piece-dem-travail',
  templateUrl: './add-piece-dem-travail.component.html',
  styleUrls: ['./add-piece-dem-travail.component.css']
})

export class AddPieceDemTravailComponent implements OnInit {
  @ViewChild('listPiecesCpD') listPiecesCpD!: ElementRef;
  public AddDempiece: FormGroup;
  public updateDemTravail: FormGroup;
  DemPieceIdUpdate: number;
  massage: string;
  dataSaved: boolean;
  pieceList: any;
  selectedValueP = '';

  DemTrvIdUpdate: number;
  CodePiece: string;
  Designation: string;

  constructor(private router: Router ,private pieceService: PieceService, private demPieceService: DemPieceService,  private demtrvService: DemTravailService) { }

  ngOnInit(): void {
    this.getListPieces();
    this.updateDemTravail = new FormGroup({
      NumDem: new FormControl(history.state.item.NumDem),
      Status: new FormControl(history.state.item.Status),
      Contact: new FormControl(history.state.item.Contact),
      ArretMachine: new FormControl(),
      Nature: new FormControl(history.state.item.Nature),
      Consommable: new FormControl(history.state.item.Consommable),
      CentreCout: new FormControl(history.state.item.CentreCout),
      Description: new FormControl(history.state.item.Description),
      NatPanne: new FormControl(history.state.item.NatPanne),
      TEffect: new FormControl(history.state.item.TEffect)
      /*DateDInter: new FormControl(),
      DateClo: new FormControl(),
      DatePreClo: new FormControl(),
      DateSouh: new FormControl(),
      DateDem: new FormControl(history.state.item.DateDem),
      DateFinT: new FormControl(),*/
    });
    
    this.AddDempiece = new FormGroup({
      CodePiece: new FormControl(this.selectedValueP),
      Designation: new FormControl(this.selectedValueP),
      NumDem: new FormControl(history.state.item.NumDem),
      QteDem: new FormControl()
    });
  
  }

  SelectedPiece(event: any) {
    this.selectedValueP = event.target.value;
  }


  getListPieces() {
    this.pieceService.GetDesignationcodePiece().subscribe((dataP: any) => { this.pieceList = dataP });
  }

  onFormSubmit(){
    const dt = this.updateDemTravail.value;
    this.InsertDemTravail(dt);
    const demp = this.AddDempiece.value;
    this.InsertDemPiece(demp);
    const cp=this.AddDempiece.value.CodePiece;
    const qtep=this.AddDempiece.value.QteDem;
    this.MajPiece(qtep,cp);
    alert("La piece est ajouté a la demande de travail numéro :"+history.state.item.NumDem);
    this.router.navigate(['/listDemTravail']).then();
  }

  MajPiece(qtep :number, cp:string){
  this.demPieceService.MAJPiece(qtep,cp);
  }

  InsertDemTravail(demtrv: DemTravail) {
    if (this.DemTrvIdUpdate !== null) {
      demtrv.NumDem = this.DemTrvIdUpdate;
    }
    this.demtrvService.UpdateDemTravail(demtrv).subscribe(
      () => {
        if (this.DemTrvIdUpdate === null) {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add success Dem Travail');
        this.dataSaved = true;
      });
  }

  
  InsertDemPiece(demp: DemPiece) {
    if (this.DemPieceIdUpdate !== null) {
      demp.NumOrdre = this.DemPieceIdUpdate;
    }
    this.demPieceService.AddDemPiece(demp).subscribe(
      () => {
        if (this.DemPieceIdUpdate === null) {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add success dem piece');
        this.dataSaved = true;
      });
  }


}
