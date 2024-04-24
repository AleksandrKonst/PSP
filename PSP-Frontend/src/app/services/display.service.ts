import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DisplayService {
  showButtonPanel: boolean = true;
  showSubsidies: boolean = false;

  constructor() { }
}
