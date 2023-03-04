import { Component, OnInit } from '@angular/core';
import { CaseService } from '../case.service';
import { IUnsentMessage } from './Imessage';

@Component({
  selector: 'app-list-of-messages',
  templateUrl: './list-of-messages.component.html',
  styleUrls: ['./list-of-messages.component.css']
})
export class ListOfMessagesComponent implements OnInit {

  messages!: IUnsentMessage[] 
  constructor(private caseService : CaseService){}
  ngOnInit(): void {
    
    this.getMessages()
  }

  getMessages(){

    this.caseService.getMessages().subscribe({
      next:(res)=>{
        this.messages = res

        console.log(res)
      },error:(err)=>{

        console.error(err)
      }
    })
  }


  
}
