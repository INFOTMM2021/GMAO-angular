import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { DocumentEquipement } from 'src/app/Models/DocumentEquipement';
import { UploadDownloadService } from 'src/app/Shared/Services/upload-Download/upload-download.service';

@Component({
  selector: 'app-list-files',
  templateUrl: './list-files.component.html',
  styleUrls: ['./list-files.component.css']
})
export class ListFilesComponent implements OnInit {
  displayedColumns: string[] = ['CodeEquipement', 'CodeDocument', 'Description', 'DateCreation', 'ExtentionFile', 'Actions'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataSource!: MatTableDataSource<DocumentEquipement>;
  listFiles!: Observable<string[]>;
  listDocuments!: Observable<DocumentEquipement[]>;
  



  constructor(private router: Router, private US: UploadDownloadService, private http: HttpClientModule) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.Loaddocuments().subscribe(
      (data: DocumentEquipement[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.dataSource);
      });
  }

  Loaddocuments() {
    return this.listDocuments = this.US.getDocuments();
  }

  LoadFiles() {
    return this.listFiles = this.US.getFiles();
  }


  FichDoc(){
    this.router.navigate(['/uploadFile']).then();
  }

  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }




}
