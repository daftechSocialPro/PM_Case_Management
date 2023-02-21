import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddProgramsComponent } from './add-programs/add-programs.component';
import { Program } from './Program';
import { ProgramService } from './programs.services';

@Component({
  selector: 'app-programs',
  templateUrl: './programs.component.html',
  styleUrls: ['./programs.component.css']
})
export class ProgramsComponent implements OnInit {


  Programs: Program[] = []
  constructor(private modalService: NgbModal, private programService: ProgramService) { }
  ngOnInit(): void {

    this.listPrograms()
  }

  listPrograms() {

    this.programService.getPrograms().subscribe({
      next: (res) => {
        this.Programs = res
      },
      error: (err) => {
        console.error(err)
      }
    })
  }



  addProgram() {
    let modalRef = this.modalService.open(AddProgramsComponent, { size: 'xl', backdrop: 'static' })

    modalRef.result.then((res) => {
      this.listPrograms()
    })

  }



}
