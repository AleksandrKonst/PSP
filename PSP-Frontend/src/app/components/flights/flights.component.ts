import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { FormGroup } from '@angular/forms';
import { defineLocale, ruLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrls: ['./flights.component.scss']
})
export class FlightsComponent {
addEvent(arg0: string,$event: MatDatepickerInputEvent<any,any>) {
throw new Error('Method not implemented.');
}
  searchForm: FormGroup;
date: any;


  constructor(private formBuilder: FormBuilder, private localeService: BsLocaleService) {
    defineLocale('ru', ruLocale); // определите русскую локаль
    this.localeService.use('ru'); // используйте русскую локаль
    this.searchForm = this.formBuilder.group({
      from: [''],
      to: [''],
      date: [''],
      adultPassengers: [],
      childPassengers: []
    });
  }

  onSubmit(): void {
    console.log(this.searchForm.value);
  }

}
