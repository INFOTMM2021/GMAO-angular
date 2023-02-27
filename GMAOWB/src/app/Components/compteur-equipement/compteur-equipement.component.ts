import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { EquipementCompteur } from 'src/app/Models/EquipementCompteur';
import { EquipementCompteurService } from 'src/app/Shared/Services/EquipementCompteur/equipement-compteur.service';

@Component({
  selector: 'app-compteur-equipement',
  templateUrl: './compteur-equipement.component.html',
  styleUrls: ['./compteur-equipement.component.css']
})
export class CompteurEquipementComponent implements OnInit {

  dataSource!: MatTableDataSource<EquipementCompteur>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  CompteurEquiForm: FormGroup;
  DocEquipIdUpdate = '';
  massage!: string;
  dataSaved = false;


  constructor(private service : EquipementCompteurService, private router: Router) { }



  ngOnInit() { 
    this.CompteurEquiForm = new FormGroup({
      CodeCompteur: new FormControl(),
      NumSerie: new FormControl(),
      Dernieremodif: new FormControl(),
      Valeur: new FormControl()
    })
  }



  public InsertEquipementCompteur(EqComp: EquipementCompteur) {
   
    if (this.DocEquipIdUpdate !== '') {
      EqComp.CodeCompteur = this.DocEquipIdUpdate;
    }
    this.service.AddEquipementCompteur(EqComp).subscribe(
      () => {
        if (this.DocEquipIdUpdate === '') {
          this.massage = 'Saved Successfully';
        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add succes');
        this.dataSaved = true;
        this.router.navigate(['/listFiles']).then();
      });
  }


  onFormSubmit() {
    const p = this.CompteurEquiForm.value;
    this.InsertEquipementCompteur(p);
  }












}
