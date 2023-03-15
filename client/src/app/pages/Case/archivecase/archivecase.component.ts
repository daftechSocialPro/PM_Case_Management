import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CaseService } from '../case.service';
import { ICaseView } from '../encode-case/Icase';
import { ArchiveCaseActionComponent } from './archive-case-action/archive-case-action.component';

@Component({
  selector: 'app-archivecase',
  templateUrl: './archivecase.component.html',
  styleUrls: ['./archivecase.component.css']
})
export class ArchivecaseComponent implements OnInit {

  constructor(private modalService : NgbModal) { }
  ngOnInit(): void {

  }



}
