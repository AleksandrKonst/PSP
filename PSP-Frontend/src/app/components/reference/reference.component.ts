import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reference',
  templateUrl: './reference.component.html',
  styleUrl: './reference.component.scss'
})
export class ReferenceComponent implements OnInit{
openResolution84(): void {
  window.open('https://favt.gov.ru/public/materials//3/e/a/2/c/3ea2ccaf97cfbbf5f93258d6c04f58c2.pdf', '_blank');
}
openResolution1372(): void {
  window.open('https://favt.gov.ru/public/materials//a/b/b/d/2/abbd261056d0f248583561d3fed5b807.pdf', '_blank');
  
}
  ngOnInit(): void {
    
  }
  openInstructions(): void {
    window.open('https://www.aeroflot.ru/media/aflfiles/agents/instruktsiia_po_oformleniiu_subsidirovannykh_perevozok_07_12_22.pdf', '_blank');
  }

}
