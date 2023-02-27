import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DemTravail } from 'src/app/Models/DemTravail';
import { DemTravailService } from 'src/app/Shared/Services/DemTravail/dem-travail.service';

@Component({
  selector: 'app-recap-intervention',
  templateUrl: './recap-intervention.component.html',
  styleUrls: ['./recap-intervention.component.css']
})
export class RecapInterventionComponent implements OnInit {
  @ViewChild('nd') nd!: ElementRef;
  public RecapInterv: FormGroup;
  demtrvList:any[];
  selectedValue:any;
  DemTrvIdUpdate: number;
  DemPieceIdUpdate: number;
  massage: string;
  dataSaved: boolean;
  constructor(private demtrvService: DemTravailService ,private router: Router) { }

  ngOnInit() {
    this.getListDemTravail();
    this.RecapInterv = new FormGroup({
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
      DateDInter: new FormControl(history.state.item.DateDInter),
      DateClo: new FormControl(history.state.item.DateClo),
      DatePreClo: new FormControl(history.state.item.DatePreClo),
      DateSouh: new FormControl(history.state.item.DateSouh),
      DateFinT: new FormControl(history.state.item.DateFinT),
    });
  }


  getListDemTravail(){
    this.demtrvService.GetAllDemTravail().subscribe((data: any) => { this.demtrvList = data });
  }

  SelectedDem(event: any) {
    this.selectedValue = event.target.value;
  }

  InsertDemTravail(demtrv: DemTravail) {
    if (this.DemTrvIdUpdate !== null) {
      demtrv.NumDem = this.DemTrvIdUpdate;
    }
    this.demtrvService.UpdateDemTravailRecap(demtrv).subscribe(
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

  onFormSubmit(){
    const dt = this.RecapInterv.value;
    this.InsertDemTravail(dt);
    alert("La demande de travail a etait modifier avec succes :"+history.state.item.NumDem);
    this.router.navigate(['/listDemTravail']).then();
  }

}
