import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { DemPiece } from 'src/app/Models/DemPiece';
import { DemTravail } from 'src/app/Models/DemTravail';
import { Employe } from 'src/app/Models/Employe';
import { Intervention } from 'src/app/Models/Intervention';
import { DemPieceService } from 'src/app/Shared/Services/DemPiece/dem-piece.service';
import { DemTravailService } from 'src/app/Shared/Services/DemTravail/dem-travail.service';
import { EmployeService } from 'src/app/Shared/Services/Employe/employe.service';
import { InterventionService } from 'src/app/Shared/Services/Intervention/intervention.service';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';

@Component({
  selector: 'app-dem-interv2',
  templateUrl: './dem-interv2.component.html',
  styleUrls: ['./dem-interv2.component.css']
})
export class DemInterv2Component implements OnInit {
  listEmploye: Observable<Employe[]>;
  selectedValue = '';
  massage: string;
  dataSaved: boolean;
  IntervIdUpdate: number;
  DemTrvIdUpdate: number;
  public Addintervention: FormGroup;
  public updateDemTravail:FormGroup;
  employeList: any[];
  DemPieceIdUpdate: number;
  pieceList: any;
  public AddDempiece: FormGroup;
  selectedValueP = '';

  firstFormGroup = this._formBuilder.group({
    firstCtrl: ['', Validators.required],
  });
  secondFormGroup = this._formBuilder.group({
    secondCtrl: ['', Validators.required],
  });
  thirdFormGroup = this._formBuilder.group({
    thirdCtrl: ['', Validators.required],
  });

  isLinear = false;

  constructor(private router: Router,
    private InterService: InterventionService,
    private demtrvService: DemTravailService,
    private empService: EmployeService,
    private demPieceService: DemPieceService,
    private ES: EmployeService,private _formBuilder: FormBuilder, private pieceService: PieceService) { }

  ngOnInit() {
    this.getListEmploye();
    this.getListPieces();
    this.updateDemTravail = new FormGroup({
      NumDem: new FormControl(history.state.item.NumDem),
      Status: new FormControl(history.state.item.Status),
      Contact: new FormControl(history.state.item.Contact),
      ArretMachine: new FormControl(history.state.item.ArretMachine),
      Description: new FormControl(history.state.item.Description),
      TEffect: new FormControl(history.state.item.TEffect),
      DateSouh: new FormControl(),
      DateFinT: new FormControl(),
      DateDInter: new FormControl(),
      DateClo: new FormControl(),
      DatePreClo: new FormControl(),
    });
    this.AddDempiece = new FormGroup({
      CodePiece: new FormControl(this.selectedValueP),
      Designation: new FormControl(this.selectedValueP),
      NumDem: new FormControl(history.state.item.NumDem),
      QteDem: new FormControl()
    });
    this.Addintervention = new FormGroup({
      Intervenant: new FormControl(this.selectedValue),
      Matricule: new FormControl(this.selectedValue),
      NumDem: new FormControl(history.state.item.NumDem)
    });
    
    
  }

  getListEmploye() {
    this.empService.GetAllEmployesList().subscribe((data: any) => { this.employeList = data });
  }

  SelectedPiece(event: any) {
    this.selectedValueP = event.target.value;
  }

  onFormSubmit() {
    const demp = this.AddDempiece.value;
    this.InsertDemPiece(demp);

    const dt = this.updateDemTravail.value;
    this.InsertDemTravail(dt);

    const cp=this.AddDempiece.value.CodePiece;
    const qtep=this.AddDempiece.value.QteDem;
    this.MajPiece(qtep,cp);
    console.log('success de maj nbr pieces');

    const inter = this.Addintervention.value;
    if (inter != null)
      this.InsertIntervention(inter);
      console.log('success intervention');
  }

  LoadEmployes() {
    return this.listEmploye = this.ES.GetAllEmployes();
  }

  MajPiece(qtep :number, cp:string){
   return  this.demPieceService.MAJPiece(qtep,cp);
    }


  SelectedEmp(event: any) {
    this.selectedValue = event.target.value;
  }

  InsertIntervention(interv: Intervention) {
    if (this.IntervIdUpdate !== null) {
      interv.NOInter = this.IntervIdUpdate;
    }
    this.InterService.AddIntervention(interv).subscribe(
      () => {
        if (this.IntervIdUpdate === null) {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
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

  getListPieces() {
    this.pieceService.GetDesignationcodePiece().subscribe((dataP: any) => { this.pieceList = dataP });
  }



}
