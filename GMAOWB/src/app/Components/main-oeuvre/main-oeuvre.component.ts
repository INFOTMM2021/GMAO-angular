import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Employe } from 'src/app/Models/Employe';
import { EmployeService } from 'src/app/Shared/Services/Employe/employe.service';

@Component({
  selector: 'app-main-oeuvre',
  templateUrl: './main-oeuvre.component.html',
  styleUrls: ['./main-oeuvre.component.css']
})
export class MainOeuvreComponent implements OnInit {

  displayedColumns: string[] =['Matricule','Nom','Prenom','Categorie','Specialite','Actif','Actions'];
  dataSource!: MatTableDataSource<Employe>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  listEmploye!: Observable<Employe[]>;
  newRowData = {    Matricule: '',Nom: '',Prenom:'',Categorie:'',Specialite:''};
    
  constructor(private ES: EmployeService, private router: Router) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadEmployes().subscribe(
      (data: Employe[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        //console.log(this.dataSource);
      });
  }
  LoadEmployes(){
    return this.listEmploye =  this.ES.GetAllEmployes();
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  Deactiver(empp:Employe){
   return this.ES.Deactiver(empp);
  }


  NavigateToIntervention(emp:Employe){
    this.router.navigate(['/intervention'], {state: {item: emp}});
  }
}
