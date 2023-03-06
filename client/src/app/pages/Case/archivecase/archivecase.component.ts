import { Component, OnInit } from '@angular/core';
import { CaseService } from '../case.service';
import { ICaseView } from '../encode-case/Icase';

@Component({
  selector: 'app-archivecase',
  templateUrl: './archivecase.component.html',
  styleUrls: ['./archivecase.component.css']
})
export class ArchivecaseComponent implements OnInit {

  completedCases!: ICaseView[]

  constructor(private caseService: CaseService) { }
  ngOnInit(): void {
    this.getCompletedCases()
  }

  getCompletedCases() {

    this.caseService.getCompletedCases().subscribe({
      next: (res) => {
        this.completedCases = res
      }, error: (err) => {
        console.log(err)
      }
    })
  }






}
