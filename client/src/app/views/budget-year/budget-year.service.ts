import { Injectable } from '@angular/core';
import { ProgramBudgetYear } from './budget-year';
import { HttpClient, } from "@angular/common/http";
import { environment } from '../../../environments/environment';
import { SelectList } from '../../shared/model/select-list';
@Injectable({
  providedIn: 'root'
})
export class BudgetYearService {

  constructor(private http: HttpClient) { }
  
  readonly BaseURI = environment.baseUrl + "/BudgetYear";


  // Program Budget Year
  CreateProgramBudgetYear(ProgramBudgetYear: ProgramBudgetYear) {

    return this.http.post(this.BaseURI, ProgramBudgetYear)
  }

  getProgramBudgetYear() {
    return this.http.get<ProgramBudgetYear[]>(this.BaseURI)
  }

  getProgramBudgetYearSelectList() {
    return this.http.get<SelectList[]>(this.BaseURI + "/programbylist")
  }

}
