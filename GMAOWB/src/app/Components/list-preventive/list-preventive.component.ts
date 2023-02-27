import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-list-preventive',
  templateUrl: './list-preventive.component.html',
  styleUrls: ['./list-preventive.component.css']
})
export class ListPreventiveComponent implements OnInit {

  displayedColumns: string[] =['Codepreventive','Description','TypeIntervention','DateDebut','Condition','DernierExecution','ValDemExec','Statut'];
  dataSource!: MatTableDataSource<[]>;




  constructor() { }

  ngOnInit(): void {
  }






  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }



}
