import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { FournisseurPiece } from 'src/app/Models/FournisseurPiece';
import { FournisseurPieceService } from 'src/app/Shared/Services/FournisseurPiece/fournisseur-piece.service';

@Component({
  selector: 'app-fournisseur',
  templateUrl: './fournisseur.component.html',
  styleUrls: ['./fournisseur.component.css']
})
export class FournisseurComponent implements OnInit {

  listFournissuers !: Observable<FournisseurPiece[]>;
  displayedColumns: string[] =['No_','Name','Address','City'];
  dataSource!: MatTableDataSource<FournisseurPiece>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor( private router: Router, private route: ActivatedRoute, private FS:FournisseurPieceService) { }

 

  Loadfournisseurs(){
    return this.listFournissuers =  this.FS.GetAllFournisseurPieces();
  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.Loadfournisseurs().subscribe(
      (data: FournisseurPiece[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }


}
