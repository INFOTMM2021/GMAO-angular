import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { CentreCout } from 'src/app/Models/CentreCout';
import { CentreCoutService } from 'src/app/Shared/Services/CentreCout/centre-cout.service';

@Component({
  selector: 'app-add-centre-cout',
  templateUrl: './add-centre-cout.component.html',
  styleUrls: ['./add-centre-cout.component.css']
})
export class AddCentreCoutComponent implements OnInit {
  public AddCentreC: FormGroup;
  CCIdUpdate = '';
  massage!: string;
  dataSaved = false;
  constructor(private router: Router, private CCService: CentreCoutService, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.AddCentreC = new FormGroup({
    CodeCC : new FormControl(),
    Designation : new FormControl(),
    CodeAnt : new FormControl(),
    Seq : new FormControl()
    });
    const Id = this.route.snapshot.queryParams['CodeCC'];
    if (Id != null) {
      this.CCEdit(Id);
    }
  }

  onFormSubmit(){
    const p = this.AddCentreC.value;
    this.InsertCC(p);
  }

  CCEdit(Codecc :string){

  }

  InsertCC(cc: CentreCout) {
    
    if (this.CCIdUpdate !== '') {
      cc.CodeCC = this.CCIdUpdate;
    }
    this.CCService.AddCentreCout(cc).subscribe(
      () => {
        if (this.CCIdUpdate === '') {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add success');
        this.dataSaved = true;
        this.router.navigate(['/listCentreCout']).then();
      });
  }


  ClearForm(){
    this.AddCentreC.controls['CodeCC'].setValue('');
    this.AddCentreC.controls['Designation'].setValue('');
    this.AddCentreC.controls['CodeAnt'].setValue('');
    this.AddCentreC.controls['Seq'].setValue('');
  }

}
