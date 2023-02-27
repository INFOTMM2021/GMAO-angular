import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DemPiece } from 'src/app/Models/DemPiece';
import { DemTravail } from 'src/app/Models/DemTravail';
import { Intervention } from 'src/app/Models/Intervention';
import { DemPieceService } from 'src/app/Shared/Services/DemPiece/dem-piece.service';
import { DemTravailService } from 'src/app/Shared/Services/DemTravail/dem-travail.service';
import { EmployeService } from 'src/app/Shared/Services/Employe/employe.service';
import { InterventionService } from 'src/app/Shared/Services/Intervention/intervention.service';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldControl } from '@angular/material/form-field';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';
import { MatTableDataSource } from '@angular/material/table';
import { Employe } from 'src/app/Models/Employe';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-intervention',
  templateUrl: './intervention.component.html',
  styleUrls: ['./intervention.component.css']
})
export class InterventionComponent implements OnInit {
  @Input() demandetrav: any;
  @ViewChild('empp') empp!: ElementRef;
  listEmploye: Observable<Employe[]>;
  id: number;
  demtrStorage: any;
  selectedEmp: any;
  employeList: any[];
  public AddDempiece: FormGroup;
  public Addintervention: FormGroup;
  public updateDemTravail: FormGroup;
  IntervIdUpdate: number;
  DemTrvIdUpdate: number;
  DemPieceIdUpdate: number;
  massage: string;
  dataSaved: boolean;
  NumDem: any;
  selectedValue = '';
  selectedValueMat: HTMLElement;
  selectedValueNP: HTMLElement;
  IntervListMatricule: any;
  NBon: number = 1;
  pieceList: any;

  constructor(private router: Router,
    private InterService: InterventionService,
    private demtrvService: DemTravailService,
    private empService: EmployeService,
    private ES: EmployeService) { }


  getListEmploye() {
    this.empService.GetAllEmployesList().subscribe((data: any) => { this.employeList = data });
  }


  SelectedEmp(event: any) {
    this.selectedValue = event.target.value;
    /*this.selectedEmp = this.GetInterventionMatricule();
    console.log("selected employe", this.selectedEmp);*/
  }


  ngOnInit() {
    this.getListEmploye();

    this.updateDemTravail = new FormGroup({
      NumDem: new FormControl(history.state.item.NumDem),
      Status: new FormControl(history.state.item.Status),
      Contact: new FormControl(history.state.item.Contact),
      ArretMachine: new FormControl(history.state.item.ArretMachine),
      Nature: new FormControl(history.state.item.Nature),
      Consommable: new FormControl(history.state.item.Consommable),
      CentreCout: new FormControl(history.state.item.CentreCout),
      Description: new FormControl(history.state.item.Description),
      NatPanne: new FormControl(history.state.item.NatPanne),
      TEffect: new FormControl(history.state.item.TEffect),
      DateDem: new FormControl(history.state.item.DateDem),
      DateDInter: new FormControl(),
      DateClo: new FormControl(),
      DatePreClo: new FormControl(),
      DateSouh: new FormControl(),
      DateFinT: new FormControl(),
    });

    this.Addintervention = new FormGroup({
      Intervenant: new FormControl(this.selectedValue),
      Matricule: new FormControl(this.selectedValue),
      NumDem: new FormControl(history.state.item.NumDem)
    });

  }

  LoadEmployes() {
    return this.listEmploye = this.ES.GetAllEmployes();
  }


  onFormSubmit() {
    const dt = this.updateDemTravail.value;
    this.InsertDemTravail(dt);

    const inter = this.Addintervention.value;
    if (inter != null)
      this.InsertIntervention(inter);

    //this.GetInterventionMatricule();
    localStorage.removeItem('DemTravailStorage');
    localStorage.removeItem('DemTravailStorage22');
    alert("L'intervention est ajouté a la demande de travail numéro :" + history.state.item.NumDem);
    this.router.navigate(['/listDemTravail']).then();
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

  InsertIntervention(inter: Intervention) {
    if (this.IntervIdUpdate !== null) {
      inter.NOInter = this.IntervIdUpdate;
    }
    this.InterService.AddIntervention(inter).subscribe(
      () => {
        if (this.IntervIdUpdate === null) {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        this.dataSaved = true;
      });
  }






}
