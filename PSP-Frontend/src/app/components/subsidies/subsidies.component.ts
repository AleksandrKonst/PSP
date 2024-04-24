import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';


export interface Flight {
  airline: string;
  route: string;
  maxPrice: string;
}

@Component({
  selector: 'app-subsidies',
  templateUrl: './subsidies.component.html',
  styleUrls: ['./subsidies.component.scss']
})
export class SubsidiesComponent {

  secondDataSource: Flight[] = [
    { airline: 'S7, «Уральские авиалинии»', route: 'Владивосток — Иркутск', maxPrice: '5500 ₽' },
    { airline: 'S7', route: 'Владивосток — Новосибирск', maxPrice: '5900 ₽' },
    { airline: 'S7', route: 'Владивосток — Южно-Сахалинск', maxPrice: '6000 ₽' },
    { airline: 'S7', route: 'Владивосток — Якутск', maxPrice: '6400 ₽' },
    { airline: '«Уральские авиалинии»', route: 'Владивосток — Екатеринбург', maxPrice: '6400 ₽' }
  ];


   constructor(public dialog: MatDialog) {}
   displayedColumns: string[] = ['airline', 'route', 'maxPrice'];
   dataSource: Flight[] = [
    { airline: 'Аэрофлот', route: 'Анадырь — Москва', maxPrice: '10 200 ₽' },
    { airline: 'Аэрофлот', route: 'Благовещенск — Москва', maxPrice: '9 700 ₽' },
    { airline: 'Уральские авиалинии', route: 'Благовещенск — Санкт-Петербург', maxPrice: '9 700 ₽' },
    { airline: 'Аэрофлот', route: 'Владивосток — Москва', maxPrice: '10 200 ₽' },
    { airline: 'Аэрофлот', route: 'Владивосток — Санкт-Петербург', maxPrice: '10 200 ₽' },
    { airline: 'Аэрофлот', route: 'Магадан — Москва', maxPrice: '10 200 ₽' },
    { airline: 'Алроса', route: 'Мирный — Москва', maxPrice: '8 700 ₽' },
    { airline: 'Алроса', route: 'Мирный — Санкт-Петербург', maxPrice: '8 700 ₽' },
    { airline: 'Якутия', route: 'Нерюнгри — Москва', maxPrice: '9 700 ₽' },
    { airline: 'Якутия', route: 'Певек — Москва', maxPrice: '9 700 ₽' },
    { airline: 'Аэрофлот', route: 'Петропавловск-Камчатский — Москва', maxPrice: '10 200 ₽' },
    { airline: 'Сибирь', route: 'Петропавловск-Камчатский — Санкт-Петербург', maxPrice: '10 200 ₽' },
    { airline: 'Алроса', route: 'Полярный — Москва', maxPrice: '8 700 ₽' },
    { airline: 'S7', route: 'Улан-Удэ — Москва', maxPrice: '9 200 ₽' },
    { airline: 'Аэрофлот', route: 'Хабаровск — Москва', maxPrice: '10 200 ₽' },
    { airline: 'Аэрофлот', route: 'Хабаровск — Санкт-Петербург', maxPrice: '10 200 ₽' },
    { airline: 'S7', route: 'Чита — Москва', maxPrice: '9 200 ₽' },
    { airline: 'iFly', route: 'Чита — Санкт-Петербург', maxPrice: '9 200 ₽' },
    { airline: 'Аэрофлот', route: 'Южно-Сахалинск — Москва', maxPrice: '10 200 ₽' },
    { airline: 'Аэрофлот', route: 'Якутск — Москва', maxPrice: '9 200 ₽' },
    { airline: 'Якутия', route: 'Якутск — Санкт-Петербург', maxPrice: '9 200 ₽' }
    ];

}
