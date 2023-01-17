import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-desk-reserve-dialog',
  templateUrl: './desk-reserve-dialog.component.html',
  styleUrls: ['./desk-reserve-dialog.component.css']
})
export class DeskReserveDialogComponent implements OnInit {
  
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
 
  constructor(
    public dialogRef: MatDialogRef<DeskReserveDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: string) {

  }

  ngOnInit(): void {
  }
  
  onNoClick(): void {
    this.dialogRef.close();
  }


}
