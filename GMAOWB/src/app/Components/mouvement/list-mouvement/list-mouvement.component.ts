import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MouvementDetail } from 'src/app/Models/MouvementDetail';
import { MouvementDetailService } from 'src/app/Shared/Services/Mouvements/mouvement-detail.service';

@Component({
  selector: 'app-list-mouvement',
  templateUrl: './list-mouvement.component.html',
  styleUrls: ['./list-mouvement.component.css']
})
export class ListMouvementComponent implements OnInit {

  displayedColumns: string[] =['NumMvt','Type','CodePiece','QteDem','QteMvt','PAchat','PRevient','Actions'];
  listMouvDet: Observable<MouvementDetail[]>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataSource!: MatTableDataSource<MouvementDetail>;



  constructor(private router: Router, private mouveDetServ :MouvementDetailService) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadMouvementDetails().subscribe(
      (data: MouvementDetail[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
  });
  }

  LoadMouvementDetails(){
    return this.listMouvDet = this.mouveDetServ.GetAllMouvementDetails();
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

  Reguler(nummvt :Number){

  }
}
