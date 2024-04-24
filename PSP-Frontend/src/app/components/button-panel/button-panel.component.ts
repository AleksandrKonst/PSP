import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterEvent } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-button-panel',
  templateUrl: './button-panel.component.html',
  styleUrls: ['./button-panel.component.scss']
})
export class ButtonPanelComponent implements OnInit {

  constructor(private router: Router) {}  
openRoutes() {
  this.router.navigate(['/routes']);

}
openInfo() {
  this.router.navigate(['/info']);

}
openFlights() {
  this.router.navigate(['/flights']);
}
  showButtonPanel: boolean = true;  
  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.showButtonPanel = this.router.url === '/';
      }
    });
  }
  
}
