import { Component, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DemTravail } from 'src/app/Models/DemTravail';
import { DemTravailService } from 'src/app/Shared/Services/DemTravail/dem-travail.service';
import { SharedServicesService } from 'src/app/Shared/Services/SharedServices/shared-services.service';
import { CentreCoutComponent } from '../centre-cout/centre-cout.component';
import { RapportService } from 'src/app/Shared/Services/Rapports/rapport.service';

@Component({
  selector: 'app-demande-travail',
  templateUrl: './demande-travail.component.html',
  styleUrls: ['./demande-travail.component.css']
})
export class DemandeTravailComponent implements OnInit {

  
  DemTrvIdUpdate : number ;
  @Input() CCDesignation: any;
  public AddDemandeTravail: FormGroup;
  selectedCentreCout: any;
  massage!: string;
  dataSaved = false;
  constructor(private router :Router,private sharedServ: SharedServicesService, private demtrvService: DemTravailService ) { }

  ngOnInit() {

    this.CCDesignation = this.sharedServ.getDesignation();
    this.AddDemandeTravail = new FormGroup({
      CentreCout: new FormControl(this.CCDesignation),
      NumDem: new FormControl(),
      Contact: new FormControl(),
      Nature: new FormControl(),
      Consommable:new FormControl(),
      Description: new FormControl(),
      NatPanne: new FormControl(),
      ArretMachine: new FormControl(),
      DateSouh: new FormControl()
    });
  }



  NavigateDemTravailToCC() {
    this.router.navigate(['/CCList2']).then();
  }

  onFormSubmit(){
    const dt = this.AddDemandeTravail.value;
    this.InsertDemTravail(dt);
  }


  getItemSelected2(CCDesignation: Node) {
    this.selectedCentreCout = CentreCoutComponent.arguments.getItemSelected(CCDesignation);
    this.AddDemandeTravail.controls['CentreCout'].setValue(CCDesignation);
  }


  InsertDemTravail(demtrv: DemTravail) {
    
    if (this.DemTrvIdUpdate !== null) {
      demtrv.NumDem = this.DemTrvIdUpdate;
    }
    this.demtrvService.AddDemTravail(demtrv).subscribe(
      () => {
        if (this.DemTrvIdUpdate === null) {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add success');
        this.dataSaved = true;
        this.router.navigate(['/listDemTravail']).then();
      });
  }





}
