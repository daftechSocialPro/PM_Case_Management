import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Task } from './task'

@Injectable({
    providedIn: 'root',
})
export class TaskService {
    constructor(private http: HttpClient) { }
    BaseURI: string = environment.baseUrl + "/PM/Task"


    //task 

    createTask(task: Task) {
        return this.http.post(this.BaseURI, task)
    }

   

}