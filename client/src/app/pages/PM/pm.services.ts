import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';


@Injectable({
    providedIn: 'root',
})
export class PMService {
    constructor() { }
    BaseURI: string = environment.baseUrl + "/PM"




}