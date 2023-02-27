import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { DemPiece } from 'src/app/Models/DemPiece';
import { DemPieceService } from 'src/app/Shared/Services/DemPiece/dem-piece.service';

@Component({
  selector: 'app-list-pieceby-num-dem',
  templateUrl: './list-pieceby-num-dem.component.html',
  styleUrls: ['./list-pieceby-num-dem.component.css']
})
export class ListPiecebyNumDemComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataSource!: MatTableDataSource<DemPiece>;
  displayedColumns: string[] =['NumDem','CodePiece','Designation','QteDem','QteLiv','NumBon'];
  listInterventions: Observable<DemPiece[]>;
  
  constructor(private router: Router, private http : HttpClientModule, private DemPieceService: DemPieceService) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadDemPieces().subscribe(
      (data: any) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.dataSource);
      });
  }

  LoadDemPieces()  {
    return this.listInterventions = this.DemPieceService.GetAllPiecesByNumDem(history.state.item.NumDem);
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
