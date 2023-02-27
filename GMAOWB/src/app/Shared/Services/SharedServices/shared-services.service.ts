import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedServicesService {
Designation : string;
  constructor() { }

  setDesignation(data: string){
    this.Designation=data;
  }
  getDesignation()
  {return this.Designation}
}
