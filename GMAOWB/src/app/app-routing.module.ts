import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCentreCoutComponent } from './Components/add-centre-cout/add-centre-cout.component';
import { CentreCoutComponent } from './Components/centre-cout/centre-cout.component';
import { CentreCout2Component } from './Components/centre-cout2/centre-cout2.component';
import { CompteurEquipementComponent } from './Components/compteur-equipement/compteur-equipement.component';
import { DemandeTravailComponent } from './Components/demande-travail/demande-travail.component';
import { EquipementComponent } from './Components/equipement/equipement.component';
import { FileManagerComponent } from './Components/file-manager/file-manager.component';
import { FournisseurComponent } from './Components/fournisseur/fournisseur.component';
import { InterventionComponent } from './Components/intervention/intervention.component';
import { ListCentreCoutComponent } from './Components/list-centre-cout/list-centre-cout.component';
import { ListDemTravailComponent } from './Components/list-dem-travail/list-dem-travail.component';
import { ListFilesComponent } from './Components/list-files/list-files.component';
import { ListInterventionComponent } from './Components/list-intervention/list-intervention.component';
import { ListPiecesComponent } from './Components/list-pieces/list-pieces.component';
import { ListPreventiveComponent } from './Components/list-preventive/list-preventive.component';
import { MainOeuvreComponent } from './Components/main-oeuvre/main-oeuvre.component';
import { ParametrePreventiveComponent } from './Components/parametre-preventive/parametre-preventive.component';
import { PieceComponent } from './Components/piece/piece.component';
import { PreventiveComponent } from './Components/Preventive/preventive.component';
import { UploadFileComponent } from './Components/upload-file/upload-file.component';
import { AddComponent } from './Dialogs/Intervention/add/add.component';
import { AddPieceDemTravailComponent } from './Components/add-piece-dem-travail/add-piece-dem-travail.component';
import { RecapInterventionComponent } from './Components/recap-intervention/recap-intervention.component';
import { ListInterventionbyNumDemComponent } from './Components/list-interventionby-num-dem/list-interventionby-num-dem.component';
import { ListPiecebyNumDemComponent } from './Components/list-pieceby-num-dem/list-pieceby-num-dem.component';
import { ReportViewerComponent } from './Components/report-viewer/report-viewer.component';
import { DemInterv2Component } from './Components/dem-interv2/dem-interv2.component';
import { PrelevementComponent } from './Components/prelevement/prelevement.component';
import { RegulStockComponent } from './Components/regul-stock/regul-stock.component';
import { ListMouvementComponent } from './Components/mouvement/list-mouvement/list-mouvement.component';
import { ListMouvementDetailsComponent } from './Components/mouvement/list-mouvement-details/list-mouvement-details.component';


const routes: Routes = [
  { path: 'addPiece', component: PieceComponent },
  { path: 'listFournisseur', component: FournisseurComponent },
  { path: 'addEquipement', component: EquipementComponent },
  { path: 'listPieces', component: ListPiecesComponent },
  { path: 'uploadFile', component: UploadFileComponent },
  { path: 'compteurEq', component: CompteurEquipementComponent },
  { path: 'listPreventive', component: ListPreventiveComponent },
  { path: 'parametrePreventive', component: ParametrePreventiveComponent },
  { path: 'mainOeuvre', component: MainOeuvreComponent },
  { path: 'preventive', component: PreventiveComponent },
  { path: 'files', component: FileManagerComponent },
  { path: 'listFiles', component: ListFilesComponent },
  { path: 'CCList', component: CentreCoutComponent },
  { path: 'listDemTravail', component: ListDemTravailComponent },
  { path: 'addCentreCout', component: AddCentreCoutComponent },
  { path: 'listCentreCout', component: ListCentreCoutComponent },
  { path: 'demandeTravail', component: DemandeTravailComponent },
  { path: 'CCList2', component: CentreCout2Component },
  { path: 'intervention', component: InterventionComponent },
  { path: 'listInterventions', component: ListInterventionComponent },
  { path : 'addPieceDemTravail', component: AddPieceDemTravailComponent},
  { path: 'recapIntervention', component:RecapInterventionComponent},
  { path: 'listIntervByNumDem', component:ListInterventionbyNumDemComponent},
  { path: 'listDemPieceByNumDem', component:ListPiecebyNumDemComponent},
  { path: 'reportViewer', component:ReportViewerComponent},
  { path: 'demInterv2', component:DemInterv2Component},
  { path: 'prelevement', component:PrelevementComponent},
  { path: 'regulStock', component:RegulStockComponent},
  { path: 'listMouvDet', component:ListMouvementComponent},
  { path: 'listMouvementDetails', component:ListMouvementDetailsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
