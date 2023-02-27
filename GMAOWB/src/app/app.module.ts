import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PieceComponent } from './Components/piece/piece.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FournisseurComponent } from './Components/fournisseur/fournisseur.component';
import { EquipementComponent } from './Components/equipement/equipement.component';
import { ListPiecesComponent } from './Components/list-pieces/list-pieces.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { TreeviewModule } from 'ngx-treeview';
import { UploadFileComponent } from './Components/upload-file/upload-file.component';
import { FileManagerComponent } from './Components/file-manager/file-manager.component';
import { MatIconModule } from '@angular/material/icon';
import { AddComponent } from './Dialogs/Piece/add/add.component';
import { MatDialogModule } from '@angular/material/dialog';
import { UpdateComponent } from './Dialogs/Piece/update/update.component';
import { CompteurEquipementComponent } from './Components/compteur-equipement/compteur-equipement.component';
import {MatTreeModule} from '@angular/material/tree';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { ListPreventiveComponent } from './Components/list-preventive/list-preventive.component';
import { ParametrePreventiveComponent } from './Components/parametre-preventive/parametre-preventive.component';
import { MainOeuvreComponent } from './Components/main-oeuvre/main-oeuvre.component';
import { PreventiveComponent } from './Components/Preventive/preventive.component';
import { ListFilesComponent } from './Components/list-files/list-files.component';
import { TreeViewModule } from '@syncfusion/ej2-angular-navigations';
import {MatCardModule} from '@angular/material/card';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { CentreCoutComponent } from './Components/centre-cout/centre-cout.component';
import { ListDemTravailComponent } from './Components/list-dem-travail/list-dem-travail.component';
import { AddCentreCoutComponent } from './Components/add-centre-cout/add-centre-cout.component';
import { ListCentreCoutComponent } from './Components/list-centre-cout/list-centre-cout.component';
import { DemandeTravailComponent } from './Components/demande-travail/demande-travail.component';
import { CentreCout2Component } from './Components/centre-cout2/centre-cout2.component';
import {MatRadioModule} from '@angular/material/radio';
import { InterventionComponent } from './Components/intervention/intervention.component';
import { ListInterventionComponent } from './Components/list-intervention/list-intervention.component';
import { MatTabsModule } from '@angular/material/tabs';
import { AddPieceDemTravailComponent } from './Components/add-piece-dem-travail/add-piece-dem-travail.component';
import { RecapInterventionComponent } from './Components/recap-intervention/recap-intervention.component';
import { ListInterventionbyNumDemComponent } from './Components/list-interventionby-num-dem/list-interventionby-num-dem.component';
import { ListPiecebyNumDemComponent } from './Components/list-pieceby-num-dem/list-pieceby-num-dem.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { ReportViewerComponent } from './Components/report-viewer/report-viewer.component';
import { RetourFournisseurComponent } from './Components/retour-fournisseur/retour-fournisseur.component';
import { DemInterv2Component } from './Components/dem-interv2/dem-interv2.component';
import { MatStepperModule } from '@angular/material/stepper';
import { PrelevementComponent } from './Components/prelevement/prelevement.component';
import { RegulStockComponent } from './Components/regul-stock/regul-stock.component';
import { ListMouvementComponent } from './Components/mouvement/list-mouvement/list-mouvement.component';
import { ListMouvementDetailsComponent } from './Components/mouvement/list-mouvement-details/list-mouvement-details.component';
import { GridModule } from '@syncfusion/ej2-angular-grids';
import { AgGridModule } from 'ag-grid-angular';
import { ModuleRegistry } from '@ag-grid-community/core';
import { ServerSideRowModelModule } from '@ag-grid-enterprise/server-side-row-model';
import {LicenseManager} from "ag-grid-enterprise";
import 'ag-grid-enterprise';

@NgModule({

  declarations: [
    PieceComponent,
    FournisseurComponent,
    EquipementComponent,
    ListPiecesComponent,
    AppComponent,
    UploadFileComponent,
    FileManagerComponent,
    AddComponent,
    UpdateComponent,
    CompteurEquipementComponent,
    ListPreventiveComponent,
    ParametrePreventiveComponent,
    MainOeuvreComponent,
    PreventiveComponent,
    ListFilesComponent,
    CentreCoutComponent,
    ListDemTravailComponent,
    AddCentreCoutComponent,
    ListCentreCoutComponent,
    DemandeTravailComponent,
    CentreCout2Component,
    InterventionComponent,
    ListInterventionComponent,
    AddPieceDemTravailComponent,
    RecapInterventionComponent,
    ListInterventionbyNumDemComponent,
    ListPiecebyNumDemComponent,
    ReportViewerComponent,
    RetourFournisseurComponent,
    DemInterv2Component,
    PrelevementComponent,
    RegulStockComponent,
    ListMouvementComponent,
    ListMouvementDetailsComponent
  ],


imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule, 
    BrowserAnimationsModule,
    MatSelectModule,
    MatIconModule,
    MatDialogModule ,
    MatTreeModule,
    MatCheckboxModule,
    TreeViewModule,
    MatTreeModule,
    MatCardModule,
    MatProgressBarModule,
    MatRadioModule,
    MatTabsModule,
    NgxExtendedPdfViewerModule,
    MatStepperModule,
    GridModule,
    AgGridModule
     ],


  providers: [],
  bootstrap: [AppComponent]

  
})
export class AppModule {
  constructor() {
    // Register the ServerSideRowModelModule
    ModuleRegistry.registerModules([ServerSideRowModelModule]);
  }
 }
