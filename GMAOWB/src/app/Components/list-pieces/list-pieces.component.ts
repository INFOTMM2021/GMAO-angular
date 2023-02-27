import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AddComponent } from 'src/app/Dialogs/Piece/add/add.component';
import { UpdateComponent } from 'src/app/Dialogs/Piece/update/update.component';
import { Piece } from 'src/app/Models/Piece';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';



@Component({
  selector: 'app-list-pieces',
  templateUrl: './list-pieces.component.html',
  styleUrls: ['./list-pieces.component.css']
})
export class ListPiecesComponent implements OnInit {
  AddPiece2 : FormGroup;
  displayedColumns: string[] =['CodePiece','Designation','CentreCout','NumSerie','Analyse1','Analyse2','Analyse3','Actions'];
  PieceIdUpdate = '';
  dataSource!: MatTableDataSource<Piece>;
  listPieces!: Observable<Piece[]>;
  AddPiece : FormGroup;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  message: string;
  dataSaved = false;
  index: number;
  id: string;
  
  constructor(private router: Router,  private PS: PieceService, private http : HttpClientModule, public dialog: MatDialog) {   }

  addNew() {
    const dialogRef = this.dialog.open(AddComponent, { data: {issue: Piece } });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        // After dialog is closed we're doing frontend updates
        // For add we're just pushing a new row inside DataService
        this.PS.dataChange.value.push(this.PS.getDialogData());
        this.refreshTable();
        this.router.navigate(['/listPieces']).then();
      }
    });
  }

  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.LoadPieces().subscribe(
      (data: Piece[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  public DeletePiece(CodePiece: string) {
    if (confirm('Are You Sure To Delete this Informations')) {
    console.log(CodePiece);
      this.PS.DeletePiece(CodePiece).subscribe(
        () => {
          this.dataSaved = true;
          this.message = 'Deleted Successfully';
        }
      );
      this.refreshTable();
    }
    
  }

  LoadPieces(){
    return this.listPieces =  this.PS.GetAllPieces();
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

  /*PieceEdit( id: string){
    this.PS.GetPieceById(id).subscribe( p=> {
    this.PieceIdUpdate=id;
    if(p){   
    this.AddPiece.controls['CodePiece'].setValue(p.CodePiece);
    this.AddPiece.controls['Designation'].setValue(p.Designation);
    this.AddPiece.controls['AnCode'].setValue(p.AnCode);
    this.AddPiece.controls['Unite'].setValue(p.Unite);
    this.AddPiece.controls['DateAchat'].setValue(p.DateAchat);
    this.AddPiece.controls['DateFabrication'].setValue(p.DateFabrication);
    this.AddPiece.controls['Marque'].setValue(p.Marque);
    this.AddPiece2.controls['Machine'].setValue(p.Machine);
    this.AddPiece2.controls['NumSerie'].setValue(p.NumSerie);
    this.AddPiece2.controls['CentreCout'].setValue(p.CentreCout);
    this.AddPiece2.controls['QteStock'].setValue(p.QteStock);
    this.AddPiece2.controls['StockAlerte'].setValue(p.StockAlerte);
    this.AddPiece2.controls['QteCmd'].setValue(p.QteCmd);
    this.AddPiece2.controls['RefFournisseur'].setValue(p.RefFournisseur);
    this.AddPiece2.controls['PAchat'].setValue(p.PAchat);
  }
  ;})
}*/
 


startEdit(i:number,dd:Piece, CodePiece :string, Designation : string , AnCode:string , Unite:string , DateAchat: string , DateFabrication : Date , Marque:string ,Machine:string ,NumSerie:string , CentreCout:string, QteStock:number, StockAlerte:number,QteCmd:number, PAchat:number ,Analyse1:string, Analyse2:string, Analyse3:string) {
  this.id = CodePiece;
  // index row is used just for debugging proposes and can be removed
  this.index = i;
  
  console.log(this.index);
  const dialogRef = this.dialog.open(UpdateComponent, {
    data: {CodePiece: CodePiece, Designation: Designation,AnCode : AnCode,Unite:Unite, DateAchat:DateAchat,DateFabrication:DateFabrication,Marque:Marque,Machine:Machine, NumSerie: NumSerie,CentreCout:CentreCout,QteStock:QteStock,StockAlerte:StockAlerte,QteCmd:QteCmd,PAchat:PAchat, Analyse1: Analyse1, Analyse2: Analyse2, Analyse3: Analyse3}
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result === 1) {
      // When using an edit things are little different, firstly we find record inside DataService by id
      const foundIndex = this.PS.dataChange.value.findIndex(x => x.CodePiece === this.id);
      console.log("piece a modifier", foundIndex);
      // Then you update that record using data from dialogData (values you enetered)
      this.PS.dataChange.value[foundIndex] = this.PS.getDialogData();
      // And lastly refresh table
      this.refreshTable();

    }
  });
}




}


