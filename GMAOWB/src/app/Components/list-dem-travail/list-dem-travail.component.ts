import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { DemTravail } from 'src/app/Models/DemTravail';
import { DemTravailService } from 'src/app/Shared/Services/DemTravail/dem-travail.service';
import { InterventionComponent } from '../intervention/intervention.component';
import { RapportService } from 'src/app/Shared/Services/Rapports/rapport.service';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
@Component({
  selector: 'app-list-dem-travail',
  templateUrl: './list-dem-travail.component.html',
  styleUrls: ['./list-dem-travail.component.css'],
  providers: [NgxExtendedPdfViewerModule]

})
export class ListDemTravailComponent implements OnInit {
  displayedColumns: string[] =['NumDem','Nature','Contact','Description','Status','DateDem','Actions'];
  listDemTrav: Observable<DemTravail[]>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataSource!: MatTableDataSource<DemTravail>;
  message: String;
  dataSaved = false;
 

  constructor(private router: Router, 
              private rptService:RapportService,
              private DemTravService: DemTravailService) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadDemTravail().subscribe(
      (data: DemTravail[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
  });
}


  LoadDemTravail(){
    return this.listDemTrav = this.DemTravService.GetAllDemTravail();  
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

  public DeleteDemTravail(NumDem: number) {
    if (confirm('Are You Sure To Delete this Informations')) {
    console.log(NumDem);
      this.DemTravService.DeleteDemTravail(NumDem).subscribe(
        () => {
          this.dataSaved = true;
          this.message = 'Deleted Successfully';
        }
      );
      this.refreshTable();
    }

}
onRowClicked(dd:DemTravail){
  localStorage.setItem("DemTravailStorage",JSON.stringify({state: {item: dd}}));
  this.router.navigate(['/demInterv2'], {state: {item: dd}});
}

NavigateToAddPieceDemTravial(item:DemTravail){
  this.router.navigate(['/addPieceDemTravail'], {state: {item: item}});
}

NavigateToRecapIntervention(item:DemTravail){
  this.router.navigate(['/recapIntervention'], {state: {item: item}});
}


navigateToAddDT(){
  this.router.navigate(['/demandeTravail']).then();
}

NavigateToIntervention(item:DemTravail){
  localStorage.setItem("DemTravailStorage",JSON.stringify({state: {item: item}}));
  this.router.navigate(['/demInterv2'], {state: {item: item}});
}

NavigateToListInterv(item:DemTravail){
  localStorage.setItem("NumDem",JSON.stringify({state: {item: item.NumDem}}));
  this.router.navigate(['/listIntervByNumDem'], {state: {item: item}});
}

NavigateToListPieces(item:DemTravail){
  localStorage.setItem("NumDem",JSON.stringify({state: {item: item.NumDem}}));
  this.router.navigate(['/listDemPieceByNumDem'], {state: {item: item}});
}

Reports(){
this.router.navigate(['/reportViewer']);
return this.rptService.getDemandeTravailReport();
 //window.print();
}

}


