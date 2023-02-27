import { animate } from '@angular/animations';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GridColumn } from '@syncfusion/ej2-angular-grids';
import { DataManager, ODataV4Adaptor, WebApiAdaptor } from '@syncfusion/ej2-data';
import { ColDef, GridApi, GridOptions } from 'ag-grid-community';
import { GridReadyEvent } from 'ag-grid-community/dist/lib/events';
import { Observable } from 'rxjs';
import { MouvementDetailService } from 'src/app/Shared/Services/Mouvements/mouvement-detail.service';

@Component({
  selector: 'app-prelevement',
  templateUrl: './prelevement.component.html',
  styleUrls: ['./prelevement.component.css']
})
export class PrelevementComponent implements OnInit {
  editSettings: { allowEditing: boolean; allowAdding: boolean; allowDeleting: boolean; };
  toolbar: string[];
  data: any;
  public data1$!:Observable <any[]>;
  private gridApi: GridApi;
  private api: GridApi;
  private gridColumnApi: GridColumn;

  public columsDefs:ColDef[]= [
    {headerName : 'NumMvt',field:'NumMvt'},
    {headerName : 'CodePiece',field:'CodePiece'},
    {headerName : 'Type',field:'Type'},
    {headerName : 'NumSerie',field:'NumSerie'},
    {headerName : 'QteDem',field:'QteDem'},
    {headerName : 'PAchat',field:'PAchat'},
    {headerName : 'PRevient',field:'PRevient'},
    {headerName : 'Devise',field:'Devise'},
      ];

      gridOptions: GridOptions = {
        rowModelType: 'serverSide',
        pagination: true,
        paginationPageSize: 10,
        cacheBlockSize: 10,
        maxBlocksInCache: 5,
        onGridReady: (params:any) => {
          this.api = params.api;
          this.api.setServerSideDatasource(this.dataSource);
        }
      };
      dataSource = {
        getRows: (params:any) => {
          const sortModel = params.sortModel;
          const filterModel = params.filterModel;
          const page = params.endRow / params.pageSize;
          const pageSize = params.pageSize;
          const url = `https://localhost:5001/api/MouvementDetails/GetAllMouvementDetail?page=${page}&pageSize=${pageSize}&sort=${JSON.stringify(sortModel)}&filter=${JSON.stringify(filterModel)}`;
          this.http.get<any>(url).subscribe(result => {
            const rowsThisPage = result.data;
            const lastRow = result.totalCount;
            params.successCallback(rowsThisPage, lastRow);
          });
        }
        };


    public defaultColDef : ColDef= {sortable:true,filter:true, editable:true}

  constructor(private servMouvdet : MouvementDetailService,
              private route:Router, private http:HttpClient) { }

  ngOnInit(): void {

  }



  
  onGridReady(params:any) {
    this.data1$ = this.http.get<any[]>('https://localhost:5001/api/MouvementDetails/GetAllMouvementDetail');
    this.api = params.api;
    }
    
    onAddClicked() {
    const newProduct = { name: 'New Product', price: 0 };
    this.http.post('https://localhost:5001/api/MouvementDetails/AddmouvementDetailRegul', newProduct).subscribe(() => {
    this.api.refreshServerSideStore({ purge: true });
    });
    }
    
    onUpdateClicked() {
    const selectedRows = this.api.getSelectedRows();
    if (selectedRows.length > 0) {
    const updatedProduct = { ...selectedRows[0], price: selectedRows[0].price + 1 };
    this.http.put('https://localhost:5001/api/MouvementDetails/${updatedProduct.id}', updatedProduct).subscribe(() => {
    this.api.refreshServerSideStore({ purge: true });
    });
    }
    }
    
    onDeleteClicked() {
      const selectedRows = this.api.getSelectedRows();
      if (selectedRows.length > 0) {
      this.http.delete('https://localhost:5001/api/MouvementDetails/${selectedRows[0].id}').subscribe(() => {
      this.api.refreshServerSideStore({ purge: true });
      });
      }
    }


}
