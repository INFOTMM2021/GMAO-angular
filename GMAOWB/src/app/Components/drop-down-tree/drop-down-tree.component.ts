import { Component, OnInit } from '@angular/core';
import { TreeviewItem, TreeviewConfig } from 'ngx-treeview';
import { Observable } from 'rxjs';
import { CentreCout } from 'src/app/Models/CentreCout';
import { CentreCoutService } from 'src/app/Shared/Services/CentreCout/centre-cout.service';

@Component({
  selector: 'app-drop-down-tree',
  templateUrl: './drop-down-tree.component.html',
  styleUrls: ['./drop-down-tree.component.css']
})
export class DropDownTreeComponent implements OnInit {
  value = 11;
  items: Observable <CentreCout[]>;
  config = TreeviewConfig.create({
    hasFilter: true,
    hasCollapseExpand: true
  });

  constructor(private centreCoutService: CentreCoutService) { }

  ngOnInit(): void {
    this.items = this.centreCoutService.GetAllCentreCouts();
  }

  onValueChange(value: number): void {
    console.log('valueChange raised with value: ' + value);
  }

  loadBooks1(): void {
    this.items = this.centreCoutService.GetAllCentreCouts();
    this.value = 11;
  }


}
