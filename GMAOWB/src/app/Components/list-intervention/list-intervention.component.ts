import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Intervention } from 'src/app/Models/Intervention';
import { InterventionService } from 'src/app/Shared/Services/Intervention/intervention.service';

@Component({
  selector: 'app-list-intervention',
  templateUrl: './list-intervention.component.html',
  styleUrls: ['./list-intervention.component.css']
})
export class ListInterventionComponent implements OnInit {
  listIntervs!: Observable<Intervention[]>;
  displayedColumns: string[] =['NumDem','Matricule','Intervenant','Categorie','NOInter','DDebut','DFin','NHeure','Status','Actions'];
  dataSource!: MatTableDataSource<Intervention>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private router: Router , private http : HttpClientModule, private intervService: InterventionService) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadInterventions().subscribe(
      (data: Intervention[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  LoadInterventions(){
    return this.listIntervs =  this.intervService.GetAllInterventions();
  }
  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
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

}
