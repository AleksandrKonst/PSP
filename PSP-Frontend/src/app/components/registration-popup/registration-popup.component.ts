import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-registration-popup',
  templateUrl: './registration-popup.component.html',
  styleUrls: ['./registration-popup.component.scss']
})
export class RegistrationPopupComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<RegistrationPopupComponent>) { }

  ngOnInit(): void {
  }

  closePopup(): void {
    this.dialogRef.close();
  }
}
