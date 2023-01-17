import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-desk-naming-dialog',
  templateUrl: './desk-naming-dialog.component.html',
  styleUrls: ['./desk-naming-dialog.component.css']
})
export class DeskNamingDialogComponent implements OnInit {

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
