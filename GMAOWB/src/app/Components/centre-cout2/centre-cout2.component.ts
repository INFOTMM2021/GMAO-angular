import { Component, EventEmitter, Injectable, Input, Output } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { CollectionViewer, DataSource, SelectionChange } from '@angular/cdk/collections';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs';
import { merge } from 'rxjs';
import { map } from 'rxjs/operators';
import { CentreCout } from 'src/app/Models/CentreCout';
import { CentreCoutService } from 'src/app/Shared/Services/CentreCout/centre-cout.service';
import { Router } from '@angular/router';
import { SharedServicesService } from 'src/app/Shared/Services/SharedServices/shared-services.service';
import { PieceComponent } from '../piece/piece.component';
import { DemandeTravailComponent } from '../demande-travail/demande-travail.component';




/** Flat node with expandable and level information */
export class DynamicFlatNode {
  constructor(public item: CentreCout, public level: number = 1, public expandable: boolean = false, public isLoading: boolean = false) { }
}

/**
 * Database for dynamic data. When expanding a node in the tree, the data source will need to fetch
 * the descendants data from the database.
 */
@Injectable()
export class CentreData {

  dataMap: Map<CentreCout, CentreCout[]>;

  constructor(public CentreService: CentreCoutService) {
    this.CentreService.getDataList().subscribe(
      res => {
        //console.log(res);
        let map = new Map<CentreCout, CentreCout[]>();
        res.body!.forEach((listDeList: any[]) => map.set(listDeList[0][0], listDeList[1]));
        this.dataMap = map;
      }
    );
    /*this.CentreService.getData().subscribe(
      res => {    
      this.dataMap = new Map(res.body);
      console.log("res",res); 
      console.log("datamap",this.dataMap);
    });*/


  }

  //this is first node
  rootNode: CentreCout = new CentreCout("U", "Usine", "", 1);
  rootLevelNodes = [this.rootNode];

  /** Initial data from database */
  initialData(): DynamicFlatNode[] {
    return this.rootLevelNodes.map(node => new DynamicFlatNode(node, 0, true));
  }


  getChildren(node: CentreCout): CentreCout[] | undefined {
    children = [];
    var children: CentreCout[];
    for (const [key, value] of this.dataMap) {
      if (typeof key === 'object' && key.Seq === node.Seq) {
        children = value;
        break;
      }
    }
    //console.log("child", children);
    return children;
  }

  isExpandable(node: CentreCout): boolean {
    var expandable = false;
    for (const [key, value] of this.dataMap) {
      if (typeof key === 'object' && key.Seq === node.Seq) {
        expandable = true;
        break;
      }
    }
    return expandable;
    //return this.dataMap.has(node);
  }
}
/**
 * File database, it can build a tree structured Json object from string.
 * Each node in Json object represents a file or a directory. For a file, it has filename and type.
 * For a directory, it has filename and children (a list of files or directories).
 * The input will be a json object string, and the output is a list of `FileNode` with nested
 * structure.
 */
@Injectable()
export class DynamicDataSource implements DataSource<DynamicFlatNode> {

  dataChange: BehaviorSubject<DynamicFlatNode[]> = new BehaviorSubject<DynamicFlatNode[]>([]);

  get data(): DynamicFlatNode[] { return this.dataChange.value; }
  set data(value: DynamicFlatNode[]) {
    this.treeControl.dataNodes = value;
    this.dataChange.next(value);
  }

  constructor(private treeControl: FlatTreeControl<DynamicFlatNode>,
    private database: CentreData) { }

  connect(collectionViewer: CollectionViewer): Observable<DynamicFlatNode[]> {
    this.treeControl.expansionModel.changed!.subscribe((change: any) => {
      if ((change as SelectionChange<DynamicFlatNode>).added ||
        (change as SelectionChange<DynamicFlatNode>).removed) {
        this.handleTreeControl(change as SelectionChange<DynamicFlatNode>);
      }
    });

    return merge(collectionViewer.viewChange, this.dataChange).pipe(map(() => this.data));
  }

  disconnect(collectionViewer: CollectionViewer): void { }

  /** Handle expand/collapse behaviors */
  handleTreeControl(change: SelectionChange<DynamicFlatNode>) {
    if (change.added) {
      change.added.forEach((node: any) => this.toggleNode(node, true));
    }
    if (change.removed) {
      change.removed.reverse().forEach((node: any) => this.toggleNode(node, false));
    }
  }

  /**
   * Toggle the node, remove from display list
   */
  toggleNode(node: DynamicFlatNode, expand: boolean) {
    const children = this.database.getChildren(node.item);
    const index = this.data.indexOf(node);
    if (!children || index < 0) { // If no children, or cannot find the node, no op
      return;
    }

    if (expand) {
      node.isLoading = true;

      setTimeout(() => {
        const nodes = children.map(name =>
          new DynamicFlatNode(name, node.level + 1, this.database.isExpandable(name)));
        this.data.splice(index + 1, 0, ...nodes);
        // notify the change
        this.dataChange.next(this.data);
        node.isLoading = false;
      }, 500);
    } else {
      this.data.splice(index + 1, children.length);
      this.dataChange.next(this.data);
    }
  }
}


@Component({
  selector: 'app-centre-cou2t',
  templateUrl: './centre-cout2.component.html',
  styleUrls: ['./centre-cout2.component.css'],
  providers: [CentreData]
})
export class CentreCout2Component {
  @Output() CCDesignation = new EventEmitter<any>();
  @Input() fromWhere : string;
  constructor(database: CentreData, private router: Router, private sharedServ: SharedServicesService) {
    this.treeControl = new FlatTreeControl<DynamicFlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new DynamicDataSource(this.treeControl, database);
    this.dataSource.data = database.initialData();

  }

  treeControl: FlatTreeControl<DynamicFlatNode>;

  dataSource: DynamicDataSource;

  getLevel = (node: DynamicFlatNode) => { return node.level; };

  isExpandable = (node: DynamicFlatNode) => { return node.expandable; };

  hasChild = (_: number, _nodeData: DynamicFlatNode) => { return _nodeData.expandable; };



  public getItemSelectedToDemTravail(node: CentreCout) {
    alert("Vous avez choisissez " + node.Designation + " comme centre cout");
    this.CCDesignation.emit(node.Designation);
    this.sharedServ.setDesignation(node.Designation!);
    //console.log("designation selected", this.CCDesignation);
      this.router.navigate(['/demandeTravail'], { queryParams: { Data: node.Designation } });
    return (node.Designation) ;
  }

}



  // dataMap = new Map([["Usine",["Megisserie","Station d'épuration","Utilités","HP - Usine Mornaguia","Parc roulant","Divers","Magasin Pièces de Rechange","Laboratoire","Magasin","Tannerie"]],[ "Megisserie",["Rivière","Teinture","Corroyage","Finissage","Magasin Produits Finis","HP - Unité de Délainage"]],["Rivière",["Trempe","Enchauçonnage","Délainage","Pelainage","Echarnage","Tannage","Essorage","Dérayage WB","Mise au Vent","Utilitaires","Bascule 2T N°975105","Classificateur à sable type CS 260","Foulon Inox: DOSE MENBAU GMBH:3.20x 2.20x 1.50"]],
  // ["Trempe",[  "Coudreuse N°1 P17 DEMAR","Coudreuse N°2 P17 DEMAR","Coudreuse N°3 P17 DEMAR","Coudreuse N°4 P17 DEMAR","Coudreuse N°5 P17 DEMAR","Coudreuse N°6 P17 DEMAR","Coudreuse N°7 P17 DEMAR","Coudreuse N°8 P17 DEMAR","bascule 150 kg N° 116034"]],
  // ["Enchauçonnage",[  "HP - Enchauçonneuse  (Eliminée)","HP - Enchauçonneuse  (Eliminée)","Enchauçonneuse INOX AMALRIC modifiée à la TMM N°2","machine de sulfure a pistolets"]],
  // ["Délainage",[  "Délaineuse N°1 à Table Verticale","Délaineuse N°2  à Table Verticale"]],
  // ["Pelainage",[  "Mixer N°1 DEMAR","Mixer N°2 Chalanger","Mixer N°3 Chalanger","dégrilleur inox 316 L CHI 1500/500/6 2014"]],
  // ["Echarnage",[  "HP - Echarneuse MERCIER N°1 (Transférer au Mornagu","HP - Echarneuse MERCIER N°2 (Transférer au Mornagu","Echarneuse N°3 OMC 1600  Mat 299","Echarneuse N°4 OMC 1600","Echarneuse N°1 OMC 1600  Mat 476","Echarneuse N°2 OMC 1600  Mat 475"]],
  // ["Tannage",[  "Foulon en Bois N°1","Foulon en Bois N°2","Foulon en Bois N°3","Foulon en Bois N°4","Foulon en Bois N°5","Foulon en Bois N°6","HP - Italmix W6F éliminée (éliminé)"]],
  // ["Essorage",[  "Essoreuse N°1 RIZZI PRN2","HP - Mett. au vent OMC 1600 Mat290  (Transférer au","HP - Mett. au vent OMC 1600 Mat286 (Transférer au","HP - Metteuse au vent KANINA T.J  N°4","HP - Presse Essoreuse Vertical 1700  (Conf.Locale)"]
  // ],["Dérayage WB",[  "Dérayeuse N°4 OMC - 600","Dérayeuse N°2 RIZZI WB RU10/134","Dérayeuse N°3 RIZZI WB RU10/197","Dérayeuse N°1 RIZZI RLA9/476  (N°3 Tannerie)"]
  // ],["Mise au Vent",[  "Mett. au Vent  RM N°2 RA 1600-223","Mett. au Vent RM N°3 RA 1600-221","Mett. au Vent OMC  N°1 1600 -286","Metteuse au Vent RM  N°4  RA1600-222"]
  // ],["Teinture",[  "Foulon en Bois N°1","Foulon en Bois N°2","Foulon en Bois N°3","Foulon en Bois N°4","Foulon en Bois N°5","Foulon en Bois N°6","Foulon en Bois N°7","Foulon en Bois N°8","Foulon en Bois N°9","HP - Italmix W9F éliminée","Bascule SONELECT 150Kg N°106031","Utilitaires"]
  // ],["Corroyage",[  "Séchage","Mise en Humeur","Palissonage","Cadrage","Ponçage","Dérayage à Sec","Refondage","Utilitaires","Sous vide INCOMA petit modèle -682-1984","HP - Sous vide INCOMA petit modèle -683-1984","Ponceuse FICINI/ Modèle BUMA 1300"]
  // ],["Séchage",[  "Séchoir Tunnel TEGO   N°1","Chaîne Aérienne ITALPROGETTI L150m","Séchoir Tunnel THEMA N°2","séchoir MAINO ME135 -2006"]],
  // ["Mise en Humeur",[  "Metteuse en Humeur"]],
  // ["Palissonage",[  "HP - Schodelle SEDELMAT  (Transférer au Mornaguia)","HP - Schodelle SEDELMAT  (Transférer au Mornaguia)","Lunetteuse MERCIER DH2 1700   N°1","Palissonneuse OMC 1600  N°2   Mat 509","Palissonneuse OMC 1600  N°3   Mat 513"]],
  // ["Cadrage",[  "HP - Cadrage QUICK 16S (Transférer au Mornaguia)","Cadrage QUICK N°2 14S","Cadrage QUICK N°3 16S","Cadrage QUICK N°4 F.VARESE 16S","HP - Mesureuse WEGA NOVA2 487 (Transférer au salle"]],
  // ["Ponçage",[  "Ponceuse BERGI N°1 KURTA 1200","Ponceuse BERGI N°2 SPA 600","HP - Ponceuse BERGI N°3 SPA 600 éliminée","Ponceuse CAP DE VILLA MCE 120 N°4","HP - Ponceuse ESLIMATIC N°5","Aspirateur ITALPROGETTI N°1","Ponceuse à roue CAP DE VILLA N°383","Ponceuse à roue CAP DE VILLA N°386","ponceuse BERGI  1500","polisseuse BERGI 1200 PL"
  // ]],["Dérayage à Sec",[  "Dérayeuse à sec MERCIER N°1 DOLRAY 1500 N°133","HP - Dérayeuse à sec MERCIER N°2 DOLRAY 1500","Dérayeuse à sec FLAMAR N°1 PUNTA 5","Dérayeuse à sec FLAMAR N°2 PUNTA 5","Foulon à sec POLETTO N°1","Foulon à sec POLETTO N°2","Foulon à sec POLETTO N°3","Dépoussiéreuse T.JOFRESA N°1","HP - Aspirateur ITALPROGETTI  (Eliminée)","Dépoussiéreuse  AMALRIC modifiée","Aspirateur CAMA N°2  complet"
  // ]],["Refondage",[  "Refondeuse à Sec MERCIER N°1 SCIMATIC2"
  // ]],["Finissage",[  "Pigmentage","Rotopressage","Polissage","Satainage","Utilitaires"
  // ]],["Pigmentage",[  "Pigmenteuse CARLESSI N°1 SRX2600","HP - Pigmenteuse CARLESSI N°2 SR2600 éliminée mai","Bascule SONELECT 150 Kg N°044646","Bascule SONELECT 5 Kg N°104552","Pigmenteuse BARNINI N°3 (12+12 Pistolets)","Pigmenteuse Barnini  mod 3400 mai 2012","Bascule SONELECT 5Kg N°104678"
  // ]],["Rotopressage",[  "Rotopresse MOSTARDINI N°1 W2 1500","Roto. MOST. W2 1800 (transferer à la Tannerie)","Rotopresse MOSTARDINI N°2 WS 1800","APPLICART 1800 Marque DASCOMAR"
  // ]],["Polissage",[  "Polisseuse FICINI POLAR GIPRA 1200","HP - Lustreuse NAXOS FICINI N°1 (Ambra)  (Eliminée","HP - Lustreuse NAXOS FICINI N°2 (Ambra)  (Eliminée"
  // ]],["Satainage",[  "Finiflex MERCIER N°1 H2-1800","Finiflex MERCIER N°2","Finiflex MERCIER N°3  (Eliminée)"
  // ]],["Utilitaires",[  "Cabine de couleurs"
  // ]],["Magasin Produits Finis",[  "Mesureuse WEGA QUASAR 2/369 N°1","Mesureuse WEGA QUASAR 2/383 N°2","Bascule SONELECT 150Kg N°007039","Machine d'emballage (ROTOPLAT JOLLY)","Utilitaires","MACHINE MARQUAGE SICOMEC"
  // ]],["HP - Unité de Délainage",[  "Wagonnets en Acier","Mécanisme de Basculement","Chargeur FLEISSNER","Bac de Neutralisation Sulfure","Presse Essoreuse pneumatique 1400","Bac de Blanchiment","Presse Essoreuse Mécanique","Chargeur AMALRIC (Elimlinée)","Séchoir à Laine  (Eliminée)","Tapis Transporteur Caoutchouc","Condenseur (Aspirateur Pneumatique)","Presse à Emballer avec Tapis Transporteur","Essoreuse à laine CAPDEVILA \"Centrifugeuse\"","Séchoir à laine à tablier linéaire avec chargeuse","Utilitaires"
  // ]],["Teinture",[  "Foulon en Bois N°14","Foulon en Bois N°15","Foulon en Bois N°16","Foulon en Bois N°17","Foulon en Bois N°18","Foulon en Bois N°19","HP - Italmix W6F éliminé","Utilitaires","foulon inox  GSR 1500 X 1000 N°22 MAT 100148","foulon inox GSR 1500 X 1000 N°23 MAT 100147","Foulon en bois N°20","Foulon en bois N°21","Foulon de teinture 3x2  N°14","Foulon de teinture 3x2  N°19"
  // ]],["Corroyage",[  "Mise au Vent","Sous Vide","Séchage","Mise en Humeur","Palissonage","Cadrage","Ponçage","Imprégnation","Utilitaires"
  // ]],["Mise au Vent",[  "HP - Mette. au Vent RIZZI MRGP6/409 (Eliminée)","HP - Mette. au Vent RIZZI MRGP6/550 (Eliminée)","Metteuse au vent CM N°1 RAL/P-1311/04 2750mm","Metteuse au vent CM N°2 RAL/P-1310/04 2750mm","Metteuse au vent CM à chaud  N°3 RAL/PR-1312/04 27"
  // ]],["Sous Vide",[  "Sous vide incoma TM2 RE N°1 (déplacée à la mégisse","Sous Vide CARTIGLIANO N°2 - 4E SV40 Série 2100","Schiller 2011"
  // ]],["Séchage",[  "Séchoir Tunnel CARLESSI","HP - Chaîne Aérienne ITALPROGETTI L150m vendue","Chaîne aérienne thema"
  // ]],["Mise en Humeur",[  "Metteuse en Humeur"
  // ]],["Palissonage",[  "HP - Palisson 3P-C20-1600  (Eliminée)","Palissonneuse CARTIGLIANO  N°2 (déplacée à la mégi","HP - Empilateur FBP SB18LL1 (Eliminée)","HP - Palisson 3P-C20-1900 N°1 éliminée","Palissoneuse MOD. 3H 3200  (2011)"
  // ]],["Cadrage",[  "Cadrage CARLESSI N°1 PMEG40 hors production","Cadrage CARLESSI N°2 PMEG84","Cadrage QUICK HYSPANIA Sup3 N°3","Cadrage de marque CARLESSI modèle EGX 75"
  // ]],["Ponçage",[  "Ponçeuse BERGI 1800 N°1","Ponçeuse FICINI 1900 N°2 Hors Production","HP - Empilateur FBP SB18ZC1 éliminée","Aspirateur ITALPROGETTI","Foulon à Sec POLETTO N°1","Foulon à Sec POLETTO N°2","HP - Emplilateur FBP Mod. COMPACT éliminé","HP - Ponçeuse BERGI SELECTA 3200 vendue","Ravvivatrice RC 1800  Matricule 5260","Foulon à Sec AMC N°3 ","Foulon à Sec POLETTO PS537 2.5"
  // ]],["Imprégnation",[  "Tapis Gemata Imprégnation (Machine à Rideau KUENY","Chaîne Aérienne ITALPROGETTI L80m","Gemata  MTR1800  Mat 8705717 éliminée"
  // ]],["Finissage",[  "Pigmentage","Pressage","Rotopressage","Polissage","Lissage","Utilitaires","Auto laveuse COMAC SIMPLA 50 B-BT"
  // ]],["Pigmentage",[  "Pigmenteuse CARLESSI N°1 SRX3000 1493E","Pigmenteuse CARLESSI N°2 SRX3000 1492E","Gemata ROTO PLUS N°1 1800-202094 Synchro - cire","Gemata ROTO PLUS N°2 1800-4 & Système d'alimentati","Chaîne Aérienne ITALPROGETTI L80m","Bascule SONELECT 150Kg N°974406","Bascule SONELECT 5Kg N°084007","Machine Dascomar Mod ROLLKIM SOFT 18/4  avec sécho","Bascule SONELECT 5Kg N°104633","cabine de pistoletage 3500mm confection locale","Balance ACOM","Balance ACOM"
  // ]],["Unité de salage des peaux",[  "Mixeur de salage des peaux bovines N°1","Mixeur de salage des peaux bovines N°2","Mixeur de salage des peaux ovines N°3","MIXEUR EN ACIER INOX  Marque BERNARDINI"
  // ]],["Magasin Produits Chimiques",[  "Bascule SONELECT 106033 150Kg  N°1","Bascule SONELECT 116084  150Kg N°2","Bascule SONELECT 974116 150Kg  N°3","Bascule SONELECT 974214  150Kg  N°4","Bascule SONELECT 106034 150Kg  N°5","Bascule SONELECT 955212   150kg  N°6","Utilitaires","Bascule Scopia 134534 - 5kg N°7","Table de pesage aspirante"
  // ]],["Réception Peaux Brutes",[  "HP - Bas. SONEL. 2T  N°960001 éliminée","Bascule SONELECT 2T N°78701"
  // ]],["Salle d'Agréage",[  "HP - Mesureuse WEGA NOVA2 487","Utilitaires"
  // ]],["Station d'épuration",[  "Station Chrome","Station Déshydratation Boues","Station Traitement Physico-Chimique","Station Traitement Biologique","Laboratoire","Utilitaires","unité de traitement de carnasses","Station traitement physico-chimique","Station traitement biologique "
  // ]],["Station Chrome",[  "Filtre CONOSCREEN CST1351","Doseur à Chaux TPH","Doseur de Floculant TPF","Doseur de polyéléctrolyte TPO","Agitateur mécanique MPH","Agitateur mécanique Vertical MEQ","Agitateur Mécanique MPF","Agitateur Mécanique MPO","Agitateur Mécanique MCS","HP - Pompe d'alimentation Conosc. CST 1351 PFC1 HP","HP - Pompe d'alimentation Conosc. CST 1351 PFC2 HP","HP - Pompe de Lavage LRS PWC éliminée","Pompe Doseuse Tymerisée PPH déplacée à l'unité de","Pompe Podrello Immergée VXC30","Pompe d'alimentation Décanteur PEQ","HP - Pompe Doseuse à Membrane PPF éliminée","Pompe Doseuse à Membrane PPO","Pompe d'alimentation Presse à Plaque PCS","Presse à Plaque RC","HP - PH Mètre éliminé HP"
  // ]],["Station Déshydratation Boues",[  "Agitateur Vertical MSP","Agitateur Mécanique MCS1","Agitateur Mécanique MCS2","Pompe Doseuse PSP","Pompe d'alimentation Presse à Bande PCS","HP - Pompe de Lavage Presse à Bande PLT éliminée","HP - Presse à Bande Rotative RC 1 éliminée","Doseur de polyéléctrolyte TSP","Pompe d'extraction Boues PHS","Presse à bandes rotative RC 2","Décanteur C3E-4/454 HTS SP3.10 2014","vis convoyeur inox 304 L CV 260 6m 2014","vis convoyeur inox 304 L CV 260 2m 2014"
  // ]],["Station Traitement Physico-Chimique",[  "Dégrilleur en Canal LRS1","HP - Dégrilleur en Canal LRS2 éliminée","Diffuseur d'air BPOS1","Diffuseur d'air BPOS2","Diffuseur d'air BPE","Doseur de Chaux TPH","Doseur de Floculant TFL","Doseur de polyéléctrolyte TPO","Collecteur et système d'aération AOS1","Collecteur et système d'aération AOS2","Collecteur et système d'aération AEQ","Souffleur d'air BOS1","Souffleur d'air BOS2","Souffleur d'air BEQ1","Souffleur d'air BEQ2","Pompe de Sulfure immergée POS1","HP - Pompe de Sulfure immergée POS2 éliminée","Pompe d'alimentation Conoscreen   PFC1","Pompe d'alimentation Conoscreen   PFC2","Pompe Surnajants PSU éliminée","HP - Pompe de Lavage Conoscreen   PWC éliminée","Pompe Doseuse à Membranes PFL","Pompe Doseuse Tymerisée PPH","Pompe auxiliaire PAX","Pompe d'alimentation Décanteur PEQ1","Pompe d'alimentation Décanteur PEQ2","Pompe Doseuse à Membranes PPO","Pompe Doseuse à Membranes PBP","Agitateur Mécanique MPH","Agitateur Mécanique MFL","Agitateur Mécanique MPO","Pont racleur Décanter","HP - Filtre CONOSCREEN CST1602 hors service","HP - Pompe Podrello E/P SUB 3CV VXC30 éliminée","HP - Filtre CONOSCREEN CST1352 hors service","Pompe Immergée Dreno","dégrilleur khrystall type CHI 2800/800/6"
  // ]],["Station Traitement Biologique",[  "Difffuseur d'air BPB","Doseur de polyéléctrolyte TBP","Doseur de nutrient TNU","Collecteur et Système d'aération ABT","Souffleur d'air BBT1","Souffleur d'air BBT2","HP - Pompe doseuse à membranes PNU éliminée","Pompe de recirculation PRC1","Pompe de recirculation PRC2","Pompe de recyclage Boues PBS1","Pompe de recyclage Boues PBS2","Agitateur Mécanique MNU","Agitateur Mécanique MTD1","Agitateur Mécanique MTD2","Agitateur Mécanique MTD3","Agitateur Mécanique MBP","Pont Racleur Décanter CBS","PH Mètre éliminée","HP - Bascule SONELECT 300Kg N°1 éliminée","HP - Bascule SONELECT N°972286  5Kg  N°2 éliminée","HP - Débimètre DFU-22d hors service","Filtre à sable MONASAND MDS 50 "
  // ]],["Laboratoire",[  "HP - Spectrophotomètre éliminée","pH Mètre portatif","HP - Conductivi-mètre éliminée","Balance de Précision RADWAG de 220 g","Microscope optique de Précision","Agitateur Magnétique IKA","Filtration sous vide Millipore","Centrifugeuse","HP - Pipette éléctrique PIPETUS","Chronomètre","Réfrigirateur BOSCH","Distillateur Fistreem-Calypso","Réacteur DCO HACH","HP - Oxymètre HACH","Four à Moufle Selecta","Etuve (WTB Binder)","Jar test"
  // ]],["Utilités",[  "Energie","Sécurité","Atelier"
  // ]],["Energie",[  "Air Comprimé","Electricité","Vapeur","Pompage"
  // ]],["Air Comprimé",[  "HP - Compresseur  A.C  GA 708     (Eliminé)","HP - Compresseur  A.C  GA 708     (Eliminé)","Compresseur N°1 I-RAND ML150","HP - Sécheur d'air  I-RAND","Réservoir d'air","Compresseur N°2  I-RAND ML160","Compresseur DARI 200L 3 CV ","Compresseur Air FINI 10bars 15KW complet"
  // ]],["Electricité",[  "Transformateur N°1 1000 KVA","Transformateur N°2 1000 KVA","Transformateur N°3 1000 KVA","Transformateur N°4 1000 KVA","Transformateur N°5 1000 KVA","Transformateur N°6 500 KVA","TGBT 60VT01"
  // ]],["Vapeur",[  "Filtre à sable","HP - Adoucisseur N°1 ADY éliminé","HP - Adoucisseur N°2 ADY éliminé","HP - Osmoseur ADY éliminé","Bache à eau osmosée déplacé à l'unité de salage","Dégazeur MINGAZINI éliminé","Bâche  alimentaire de chaudières16000L en INOX","Chaudière N°1 MINGAZINI N°6978/96","Chaudière N°2 MINGAZINI N°6979/96","Bache à eau adoucie chaude N°1 34000 l","Bache à eau adoucie chaude N°2 34000 l","Bache à eau traitée chaude  12000 L éliminée","Bache à Fuel-Oil N°1 40000 L","Bache à Fuel-Oil N°2 40000 l","Pompe Transfert Fuel-Oil N°1","Pompe Transfert Fuel-Oil N°2","Bache à Gazoil éliminé","HP - Bache W.Spirit hors service","adoucisseur WATEC 2007","Bache à eau adoucie froide N°1 18000 L","Bache à eau adoucie froide N°2 18000 L","adoucisseur watec 2011","Adoucisseur 2012","Poste de détente de gaz","Adoucisseur d'eau 2500 litres et bac à sel- Watec","Bâche à eau adoucie chaude N°3 15000 l en INOX"
  // ]],["Pompage",[  "HP - Pompe de sondage Immergée N°1","HP - Pompe de sondage Immergée N°2","Pompe de sondage Immergée N°3","Pompe de sondage Immergée N°4","Pompe Eau Brute N°1","Pompe Eau Brute N°2","Pompe Eau Brute N°3","Pompe pour eau chaude adoucie N°1","Pompe pour eau chaude adoucie N°2","Pompe pour eau chaude adoucie N°3","Pompe pour eau froide adoucie N°1","Pompe pour eau froide adoucie N°2","Pompe Alimentaire chaudière  N°1","Pompe Alimentaire chaudière  N°2","Pompe eau anti-incendie N°4","Pompe eau anti-incendie N°5","Pompe eau anti-incendie 4 kw N°6","Motopompe gasoil circuit RIA "
  // ]],["Sécurité",[  "RIA","Extincteur CO2","Extincteur Mousse","Extincteur Poudre"
  // ]],["Atelier",[  "Relamatrice ESCOMAR","poste de soudure linear 340 2011"
  // ]],["HP - Usine Mornaguia",[  "Dérayeuse ALETTI","Essoreuse ALETTI","Gemata","Presse TURQUE","Refondeuse MOSCONI SPM 2300","Divers","Chariot Elevateur   T15 MAPO 6   H2.50","Metteuse au vent OMC 1600 MV160 Mat 290","Metteuse au vent MERCIER ETIR 2 N°1","Metteuse au vent MERCIER ETIR 2 N°2","Metteuse au vent MERCIER ETIR 2 N°3","Schodelle sedelmat N°1","Schodelle sedelmat N°2","Cadrage QUICK 16S N°1","Essoreuse 3P 2700","Dérayeuse RIZZI RLA/475","Utilitaires","Echarneuse MERCIER N°1","Echarneuse MERCIER N°2","Metteuse au vent OMC 1600 Mat 286"
  // ]],["Parc roulant",[  "Manutention","Véhicules de Service"
  // ]],["Manutention",[  "HP - Chariot Elevateur T15 MAPO 1    H3.00 vendu","HP - Chariot Elevateur T15 MAPO 2    H3.20 vendu","HP - Chariot Elevateur T15 MAPO 3    H3.20 vendu","HP - Chariot Elevateur T15 MAPO 4    H3.20 vendu","HP - Chariot Elevateur T15 MAPO 5    H2.50 vendu","HP - Tracteur N°1 vendu","TRACTEUR JEDDA TYPE 275 avec  trax avec remorque d","HP - Iveco : 82 TU 2012 vendu","Scania : 68 TU 7931","HP - GP13 : 45 TU 942 (Hors Service)","HP - Chariot Elevateur  H2.5  (Transférer au Morna","HP - Chariot Elevateur T15 MAPO 7    H2.5XL vendu","HP - Chariot Elevateur T15 MAPO 8    H3.2XM vendu","HP - Chariot Elevateur T15 MAPO 9    H3.2XM vendu","Chariot Elevateur N°10 T15 MAPO H5.0XM","Rampe de levage 10T","Iveco 35 :  4391 TU 103","Utilitaires","HP - Chariot Elevateur vendu","HP - Chariot Elevateur vendu","Chariot  élévateur N°11","Chariot élévateur N°12","Chariot élévateur N°13","Chariot élévateur N°14","HP - Chariot élévateur mornaguia vendu","Tronçonneuse à chaine 7900","Gerbeur électrique rechargeable","Camion SCANIA RS 138933","CHARIOT ELEVATEUR N°15 CPCD40 JAC ","CHARIOT ELEVATEUR N°16 CPCD30 NIULI","GERBEUR ELECTRIQUE STILL egg 16 mast 2F2800","Chariot Elevateur N°17 CPCD30 NIULI","CAMION IVECO DAILY 2P","CHARIOT ELEVATEUR N°18 CPCD30 NIULI"
  // ]],["HP - Chariot Elevateur T15 MAPO 3    H3.20 vendu",[  "Nouvelle Section"
  // ]],["Véhicules de Service",[  "HP - ISUZU : 61 TU 3365  (Hors Service)","HP - ISUZU : 72 TU 7717 vendue","HP - ISUZU : 78 TU 6297  (Hors Service)","HP - ISUZU : 71 TU 2773 vendue","HP - Peugeot J5 : 62 TU 6904 vendue","HP - Renault Express : 81 TU 7983 (Hors Service)","HP - Renault Express : 81 TU 1127 (Hors Service)","HP - Renault Kangoo : 89 TU 463 (Hors Service)","HP - Renault Kangoo : 89 TU 2822 (Hors Service)","HP - Peugeot 405 : 71 TU 4312 vendue","HP - Peugeot 405 : 69 TU 6137 vendue","HP - R19 Europa : 81 TU 1124 (Hors Service)","HP - R19 Europa : 96 TU 7776 (Vendu )","HP - 205 Junior : 64 TU 9401 (Hors Service)","HP - 406 : 90 TU 3915 transférée","HP - Renault Kangoo : 91 TU 4081 (Hors Service)","HP - Peugeot BOXER : 77 TU 7401 (Hors Service)","HP - Renault Express : 81 TU 511 (Hors Service)","HP - Clio Classique : 110 TU 2286 (vendue)","HP - Clio Classique : 110 TU 2287 (transférée au s","HP - Clio Classique : 110 TU 2288 vendue","HP - Clio Classique : 109 TU 1781 vendue","HP - Clio Classique : 113 TU 1829 vendue","HP - Renault Kangoo : 93 TU 675 vendue","Utilitaires","HP - Renault Lagouna : 87 TU353  (Transférer à Dal","HP - Renault Lagouna : 120 TU 5139 vendue","HP - Renault Mégane :   124 TU 6122 vendue","Renault Mégane  : 8485   TU 125","HP - renaut mégane 127","renault mégane 4030 TU 130","Clio Classique 133TU 5581","Clio classique 134 TU 343","Clio classique 137 TU 3904","HP - SYMBOL 9053 TU 139 (déplacée au rue de niger)","SYMBOL 141 TU 838","SYMBOL 1521 TU 144","SYMBOL 8310 TU 145","HP - Tricycle Piagio Tu 138  vendue","Nissan TU 148","SYMBOL 9757  TU 151","SYMBOL1.2 E   2356 TU  155","Renault Fluence  ....TU 158 (mai 2012)","Dacia  6947 TU 160 (août 2012)","Renault Fluence 7219  TU163 (Janvier 2013)","SYMBOL 6042 TU 141","Renault Fluence 9515  TU170 (Janvier 2014)","SYMBOL 885  TU 171 (Février 2014)","SYMBOL 5495 TU 171 (mars 2014)","SYMBOL 8610 TU 192(Octobre 2016)","SYMBOL 3242 TU 197 (05/2017)","Citroën Berlingo  RS 172154","SYMBOL 5724 TU 204","SYMBOL 5725 TU 204","SYMBOL 1618 TU 205","NISSAN MICRA 2634 TU213","RENAULT CLIO 5 LIFE PLUS  9006 TU 229","RENAULT CLIO 5 LIFE PLUS  9007 TU 229","RENAULT CLIO 5 LIFE PLUS  9008 TU 229"
  // ]],["Divers",[  "Ancienne Usine","Administration","Cuisine & Restaurant","Autre","Salle d'archive + Salle de formation","HP - Parc de bascules","débrousailleuse","Parc des caméras de surveillance","Tondeuse à Gazon","Vestiaires"
  // ]],["Administration",[  "SALLE DES SERVEURS ","Centrale Détection Incendie"
  // ]],["SALLE DES SERVEURS ",[  "Serveur Power Edge R730 Rack 2U","Climatiseur LG 12000 1","Climatiseur LG 12000 2","Onduleur Eaton 4KVA","NVR"]],
  // ["Salle d'archive + Salle de formation",[  "logement gardien"]],
  // ["HP - Parc de bascules",[  "bascule portée 5 kg N°982008 magasin PC","bascule portée 150kg N°974116 magasin PC","bascule portée150 kgN°014149 magasin PC","bascule portée150 kg N°004013 teinture mégisserie","bascule portée 5kg N°972359 laboratoire","bascule portée  150kg N°972215"]],
  // ["bascule portée  150kg N°972215",[  "bascule portée 150 kg N°106033","bascule portée 150 kg N°106031","bascule portée 150 kg N°106034","bascule portée 10 kg N° 104678"]],
  // ["bascule portée 10 kg N° 104678",[  "Nouvelle Section"]],
  // ["Pressage",[  "Presse MOSTARDINI N°1 MP4M","Presse KRAUSE 1500 N°2","Presse MOSTARDINI N°3 MP6M","HP - Presse Mostardini MP6  Année 2012 - retournée"]],
  // ["Rotopressage",[  "Rotopress N°1 RG18","Rotopress MOSTARDINI N°2 WS 3000","Rotopress MOSTARDINI N°3 W2 1800 avec Dispositif d"]],
  // ["Polissage",[  "Polisseuse FICINI POLAR 1500"]],
  // ["Lissage",[  "HP - Lisseuse RIAT N°1 (Eliminée)","HP - Lisseuse RIAT N°2 (Eliminée)"]],
  // ["Utilitaires",[  "bascule salle d'essai 5 kg N°104633","cabine de couleur 120 cm 5 light","Bascule de précision ACOM JW-1C 3000g-0.1g"]],
  // ["HP - Vernissage",[  "Machine à Rideau KUENY IGM1K","Pompe à Chaleur","Bascule SONELECT 60Kg  (Déplacée au MPC)","Bascule SONELECT 5Kg    (Déplacée)","Utilitaires"]],
  // ["Magasin Produits Finis",[  "Mesureuse WEGA NOVA2 N°1 486","HP - Mesureuse WEGA NOVA2 N°2 487 (Déplacée)","Mesureuse WEGA NOVA3 N°2","Bascule SONELECT 014149  150Kg  N°1","Utilitaires"]],
  // ["Laboratoire",[  "Laboratoire d'essais","Laboratoire d'analyses physico-chimiques"]],
  // ["Laboratoire d'essais",[  "HP - Foulon en Bois 1m3 N°1 démonté","HP - Foulon en Bois 1m3 N°2 démonté","HP - Foulon en Bois 600l N°3 démonté","HP - Foulon en Bois 600l N°4 démonté","Foulon FAVE 5 GAC1 Mat 313","HP - Coudreuse 150 litres démontée","Séchoir - Confection locale","Bascule N°620315","Banc d'essai N°1 Frottement Veslic KUENY","Banc d'essai N°2 Flexomètre","Cabine de pistoletage CARLESSI","Foulon \"ALFA-LOGIC 1000 \" 1.7x1.20m N°6","Foulon \"ALFA-LOGIC 1500 \" 1.85x1.4m N°7","Utilitaires","Sèche Linge BOSCH WTA 4000 BY","Foulon Inox GS 1200 N°1  1208","Foulon Inox GS 1200 N°2  1209","Foulon Inox GS 1200 N°3  1210","Foulon Inox GS 1200 N°4  1211","cabine de pistoletage tannerie","Foulon Inox Xinda GS 1400  N°7 Mat 090159","Foulon Inox Xinda GS 1400 N°6 Mat 090160","Foulon Inox Xinda GS 1400  N°8 Mat 090161","Foulon Inox Xinda GS 1400 N°5 Mat 090162"]],
  // ["Laboratoire d'analyses physico-chimiques",[  "Agitateur","Flexomètre","Veslic","Dynamomètre","Lastomètre","Cabine Evaluation Couleurs","Broyeur RETSCH","Bascule Eléctronique 310g","Hotte Aspirante","Penetromètre Type BALLY","HP - Tanomètre IUP 16","HP - Thermohygromètre HD 8501","Mésureur d'épaisseur PL60","Utilitaires","HP - machine de souplesse ZIPOR","shaking machine Mod STM","Appareil  d'abrasion Martindale","Réfrigération Brandt","Rampe d'extraction Lintex Equipement","Suntest CPS+","Spectrophotomètre"]],
  // ["Magasin",[  "Magasin Produits Chimiques","Réception Peaux Brutes","Salle d'Agréage","Nouvelle section","Magasin Peaux"]],
  // ["Magasin Peaux",[  "Bas. SONE. Inox 2T N°017124","HP - Bascule SONELECT Inox 2T N°960001 éliminée","Unité de salage des peaux","Chambre Froide","Foulon Déssaleur STENI"]],
  // ["Foulon Déssaleur STENI",[  "Tapis de chargement largeur 1 m"]],
  // ["Tannerie",[  "Teinture","Corroyage","Finissage","HP - Vernissage","Magasin Produits Finis","Rivière"]],
  // ["Rivière",[  "Foulon Inox : marque DOSE MASCHINENBAU GMBH","Trempe","Echarnage","Tannage","Essorage","Refondage","Dérayage","Utilitaires","dégrilleur khrystall   type CHI 1360/500/6"]],
  // ["Trempe",[  "HP - Bascule SONELECT 2 T  N° 96 0000 éliminée","Bascule 150 kg N°10CX12002179","Foulon en Bois N°1","Foulon en Bois N°2","Foulon OMC D3/L3 N°3 ","Foulon en Bois N°4","Foulon en Bois N°5","Foulon OMC D3/L3 N°6","Foulon \" Vulcan \" D4.0/L4.0 N°7","HP - Bascule Mécanique 1T","Dégrilleur à tambour rotatif LRS Réf077 (transfére","Sédementateur D2.4m  (Décanteur)","Bascule SONELECT Inox 2T N°017124"]],
  // ["Echarnage",[  "Echarneuse POLETTO N°1 S3200","Echarneuse POLETTO N°2 3200"]],
  // ["Tannage",[  "Foulon en Bois N°8","HP - Foulon en Bois N°9 (transférer N°4 pelain)","Foulon en Bois N°10","HP - Foulon en Bois N°11 éliminé","Foulon en Bois N°12","HP - Italmix W6F éliminée","Foulon \" Vulcan \" D4.0/L4.0 N°13","Installation d'aspiration des gaz","dégrilleur inox 316 L CHI 1500/500/6 2014","Foulon de tannage 3x3 N° 11"]],
  // ["Essorage",[  "HP - Essoreuse 3P2700  (Transférer au Mornaguia)","Essoreue PRN 6/3000 (Transférer au Mégisserie)","HP - Chaîne à Pince ITALPROGETTI Long 26m (Eliminé","Empilateur FBP AP 3011 (Transférer au Mégisserie)","Barre de mesure REFLEX (Transférer au Mégisserie)","ESSOREUSE BLUESTAR H5 *R* 1 S3000 MM SERIAL 2122","MESUREUSE SPECTRA 300","STACKER 3000 MM SERIAL 1223"]],
  // ["Refondage",[  "Refondeuse N°1 MOSCONI ZENIT 3000","Refondeuse N°2 MOSCONI SIRIO 3000","Table X N°1 FBP S150","Table X N°2 FBP S150","Empilateur SA 30"]],
  // ["Dérayage",[  "HP - Dérayeuse RIZZI N°1 RLA9/477  (Eliminée )","HP - Dérayeuse RIZZI N°2 RLA9/475  (Eliminée )","HP - Déray. RIZZI  RLA9/476   (Déplacée)","HP - Tapis Transporteur    (Eliminée)","HP - Tapis Transporteur    (Eliminée)","HP - Tapis Transporteur    (Eliminée)","HP - Dérayeyse FLAMAR N°3 PUNTA 5  1500 (Eliminée","HP - Dérayeuse FLAMAR N°4 Mod. Aut. BX 300 avec sy","Dérayeuse FLAMAR N°5 PUNTA 8 Aut. 1.79m","dérayeuse FLAMAR N° 6 PUNTA 8","dérayeuse FLAMAR N°  7  PUNTA8","Mesureuse wet blue 2012"
  // ]],["Utilitaires",[  "Nouvelle Section"]  ]]
  // );