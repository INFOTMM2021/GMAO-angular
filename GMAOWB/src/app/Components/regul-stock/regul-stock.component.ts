import { formatCurrency } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MouvementDetail } from 'src/app/Models/MouvementDetail';
import { TypeMouvement } from 'src/app/Models/TypeMouvement';
import { MouvementDetailService } from 'src/app/Shared/Services/Mouvements/mouvement-detail.service';
import { ParametreService } from 'src/app/Shared/Services/Parametre/parametre.service';
import { PieceService } from 'src/app/Shared/Services/Piece/piece.service';
import { TypeMouvementService } from 'src/app/Shared/Services/TypeMouvement/type-mouvement.service';

@Component({
  selector: 'app-regul-stock',
  templateUrl: './regul-stock.component.html',
  styleUrls: ['./regul-stock.component.css']
})
export class RegulStockComponent implements OnInit {
public RegulStock: FormGroup;
pieceList: any;
selectedValueP = '';
MaxNumPrelev : any;
massage: string;
dataSaved: boolean;
mvtdtid: number;
listData:any;
CodeP:string;
Designation:string;
nbrpieces:any;


  constructor(private router :Router, 
              private pieceService: PieceService, 
              private mouvDetailService: MouvementDetailService,
              private parametreService: ParametreService,
              private fb :FormBuilder, private http: HttpClient) {
                this.listData=[];
               }

  ngOnInit(): void {
    this.getMaxNumPrelev();
    this.getListPieces();
    this.RegulStock = this.fb.group({
      NumMvt:new FormControl(this.MaxNumPrelev),
      CodePiece : new FormControl(this.CodeP),
      Designation : new FormControl(this.Designation),
      QteDem: new FormControl(),
      RefFournisseur:new FormControl()
    });
  }


  public addItem(): void {
    this.pieceService.GetNumberOfPieces(this.CodeP).subscribe(
      nbrpieces => {
        const qteDem = this.RegulStock.controls['QteDem'].value;
        if (nbrpieces >= qteDem) {
          this.listData.push(this.RegulStock.value);
        } else {
          alert("Quantité du stock insuffisante la quantité disponible est: " + nbrpieces);
          this.clearform();
        }
      },
      error => {
        console.error(error);
      }
    );
  }



public removeItem(element:any){
  this.listData.forEach((value:any,index:any)=>{
    if (value==element)
    this.listData.splice(index,1);
    });
}

  onFormSubmit(){
    const mvtd = this.RegulStock.value;
    this.InsertMouvementDetailsRegul(mvtd);
    console.log('success mouvement details');
    alert("Le regule est ajouté avec succées");
    this.clearform();
  }

  clearform(){
    this.RegulStock.controls['CodePiece'].setValue('');
    this.RegulStock.controls['QteDem'].setValue('');
    this.RegulStock.controls['Designation'].setValue('');
    this.RegulStock.controls['RefFournisseur'].setValue('');

  }

  SelectedPiece(event: any) {
    this.selectedValueP = event.target.value;
    let test = this.selectedValueP.split(': ')[1].split(' ');
    this.CodeP=test[0];
    this.Designation=test.slice(1).join(':');
    this.Designation = this.Designation.replace(/:/g, ' ');
    this.RegulStock.controls['NumMvt'].setValue(this.MaxNumPrelev);
    this.RegulStock.controls['CodePiece'].setValue(this.CodeP);
    this.RegulStock.controls['Designation'].setValue(this.Designation);
  }

  getMaxNumPrelev(){
    return  this.parametreService.getMaxPrelevNumber().subscribe((data:any)=>{this.MaxNumPrelev= data});
  }

  getListPieces() {
    this.pieceService.GetDesignationcodePiece().subscribe((dataP: any) => { this.pieceList = dataP });
  }

  InsertMouvementDetailsRegul(Mvtdt: MouvementDetail) {
    this.mouvDetailService.AddMouvementDetailRegul(Mvtdt).subscribe(
      () => {
        this.massage = this.mvtdtid === null ? 'Saved Successfully' : 'Update Successfully';
        this.dataSaved = true;
      },
      error => {
        console.info(error.error);
      });
  }

  sendDataToBackend() {
    const data = this.listData  // replace with your data model
    // iterate through the list and send each item as a separate HTTP request
    for (let i = 0; i < data.length; i++) {
      const currentItem = data[i];
      // send the item as an HTTP request to the backend API
      this.InsertMouvementDetailsRegul(currentItem);
    }
    this.parametreService.updateParametre().subscribe();
    alert('Mouvements details Ajouté avec succées.');
    
  }

}
