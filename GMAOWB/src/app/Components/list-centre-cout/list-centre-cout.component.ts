import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CentreCout } from 'src/app/Models/CentreCout';
import { CentreCoutService } from 'src/app/Shared/Services/CentreCout/centre-cout.service';

@Component({
  selector: 'app-list-centre-cout',
  templateUrl: './list-centre-cout.component.html',
  styleUrls: ['./list-centre-cout.component.css']
})
export class ListCentreCoutComponent implements OnInit {
  displayedColumns: string[] =['CodeCC','Designation','CodeAnt','Seq','Actions'];
  dataSource!: MatTableDataSource<CentreCout>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  listCC !: Observable<CentreCout[]>;
  message: String;
  dataSaved = false;

  constructor(private router: Router, private CCS: CentreCoutService, private http : HttpClientModule ) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.GetAllCC().subscribe(
      (data: CentreCout[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  GetAllCC(){
  return this.listCC = this.CCS.GetAllCentreCouts();
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  public DeleteCC( CodeCC : string){
    if (confirm('Are You Sure To Delete this Informations')) {
      console.log(CodeCC);
        this.CCS.DeleteCentreCout(CodeCC).subscribe(
          () => {
            this.dataSaved = true;
            this.message = 'Deleted Successfully';
          }
        );
        this.refreshTable();
  }

}

navigateToAddCC(){
  this.router.navigate(['/addCentreCout']).then();
}





}
