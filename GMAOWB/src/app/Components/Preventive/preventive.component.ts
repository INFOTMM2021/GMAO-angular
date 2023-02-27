import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Preventive } from 'src/app/Models/Preventive';
import { PreventiveService } from 'src/app/Shared/Services/Preventive/preventive.service';

@Component({
  selector: 'app-preventive',
  templateUrl: './preventive.component.html',
  styleUrls: ['./preventive.component.css']
})
export class PreventiveComponent implements OnInit {
  
  displayedColumns: string[] =['CodePrev','Description','TypeIntervention','Actions'];
  dataSource!: MatTableDataSource<Preventive>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  prev :FormGroup;
  message: String;
  dataSaved = false;
  listPrev!: Observable<Preventive[]>;
  index: number;
  id: string;
  massage!: string;
  PrevIdUpdate = 0;
  constructor(private router: Router,  private PS: PreventiveService, private http : HttpClientModule,) { }

 

  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  
  onFormSubmit(){
    const pr = this.prev.value;
    this.InsertPreventive(pr);
  }

  ngOnInit(): void {
    this.prev = new FormGroup({
      //CodePrev: new FormControl(),
      Description: new FormControl(),
      TypeIntervention: new FormControl()
    });

    this.dataSource = new MatTableDataSource();
    this.LoadPrev().subscribe(
      (data: Preventive[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.dataSource);
      });
  }

  public DeletePrev(CodePrev: number) {
    if (confirm('Are You Sure To Delete this Informations')) {
    console.log(CodePrev);
      this.PS.DeletePreventive(CodePrev).subscribe(
        () => {
          this.dataSaved = true;
          this.message = 'Deleted Successfully';
        }
      );
      this.refreshTable();
    }
    
  }

  LoadPrev(){
    return this.listPrev =  this.PS.GetAllPreventives();
  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  InsertPreventive(pre: Preventive) {
    // tslint:disable-next-line: triple-equals
    if (this.PrevIdUpdate !== null) {
      pre.CodePrev = this.PrevIdUpdate;
    }
    this.PS.AddPreventive(pre).subscribe(
      () => {
        // tslint:disable-next-line: no-conditional-assignment
        if (this.PrevIdUpdate === null) {
          this.massage = 'Saved Successfully';

        } else {
          this.massage = 'Update Successfully';
        }
        console.log('Add succes');
        this.dataSaved = true;
        //this.router.navigate(['/listPieces']).then();
      });
  }







}
