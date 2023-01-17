import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeskNamingDialogComponent } from '../desk-naming-dialog/desk-naming-dialog.component';

@Component({
  selector: 'app-desk-delete-dialog',
  templateUrl: './desk-delete-dialog.component.html',
  styleUrls: ['./desk-delete-dialog.component.css']
})
export class DeskDeleteDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DeskNamingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,) {

  }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
