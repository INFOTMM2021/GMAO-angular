import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-parametre-preventive',
  templateUrl: './parametre-preventive.component.html',
  styleUrls: ['./parametre-preventive.component.css']
})
export class ParametrePreventiveComponent implements OnInit {


  public AddParaPrev: FormGroup;

  constructor() { }

  ngOnInit(): void {
  }



  onFormSubmit(){

  }
}
