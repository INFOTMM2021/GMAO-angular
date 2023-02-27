import { Component, OnInit } from '@angular/core';
import { RapportService } from 'src/app/Shared/Services/Rapports/rapport.service';

@Component({
  selector: 'app-report-viewer',
  templateUrl: './report-viewer.component.html',
  styleUrls: ['./report-viewer.component.css']
})
export class ReportViewerComponent implements OnInit {
  pdfSource: any;
  constructor(private rptService:RapportService) { }

  ngOnInit(): void {
    this.rptService.getDemandeTravailReport()
    .subscribe(data => {this.pdfSource = data;
    });
  }

}
