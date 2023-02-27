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
  selector: 'app-list-interventionby-num-dem',
  templateUrl: './list-interventionby-num-dem.component.html',
  styleUrls: ['./list-interventionby-num-dem.component.css']
})
export class ListInterventionbyNumDemComponent implements OnInit {
  displayedColumns: string[] =['NumDem','Matricule','Intervenant','DDebut','DFin','NHeure'];
  listInterventions: Observable<Intervention[]>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataSource!: MatTableDataSource<Intervention>;

  constructor(private router: Router, private http : HttpClientModule, private InterService: InterventionService) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadInterventions().subscribe(
      (data: any) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.dataSource);
      });
  }
  


  LoadInterventions()  {
    return this.listInterventions = this.InterService.GetAllInterventionsNumDem(history.state.item.NumDem);
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
}
