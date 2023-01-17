import { Observable } from 'rxjs';
import { DeskReserveDialogComponent } from './../desk-reserve-dialog/desk-reserve-dialog.component';
import { DeskDeleteDialogComponent } from './../desk-delete-dialog/desk-delete-dialog.component';
import { OfficeDeskService } from './../../services/office-desk.service';
import { environment } from './../../../../environments/environment';
import { DeskNamingDialogComponent } from './../desk-naming-dialog/desk-naming-dialog.component';
import { DeskGridItem } from './../../../objects/DeskIGridItem';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { GridsterConfig, GridsterItem, GridsterItemComponent, GridType, CompactType, GridsterPush } from 'angular-gridster2';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Desk } from 'src/app/objects/Desk';
import { User } from 'src/app/objects/User';

@Component({
  selector: 'app-office-grid',
  templateUrl: './office-grid.component.html',
  styleUrls: ['./office-grid.component.css']
})
export class OfficeGridComponent implements OnInit {

  @Input() user: User | null = null;

  options!: GridsterConfig
  dashboard: DeskGridItem[] = [];
  dashboardDeletedItems: DeskGridItem[] = [];
  dashboardUpdatedItems: DeskGridItem[] = [];
  dashboardAddedItems: DeskGridItem[] = [];

  deskRotationPhases = ["desk-container-0", "desk-container-90", "desk-container-180", "desk-container-270"];

  minDeskSize = 60;
  minimizeDeskThresholdFont = 100;
  minimizeDeskThresholdButton = 80;
  maxDeskSize = 200;
  private zoomSteps = 5;
  zoomStep = 20;
  sliderValue: number = 150;

  saveErrorMessage: string | null = null;

  loadingInitialData: boolean = false;
  savingData: boolean = false;

  @Input() userRoleChange: Observable<number> | undefined;

  constructor(public dialog: MatDialog, private _snackBar: MatSnackBar, private officeDeskService: OfficeDeskService) { }

  ngOnInit(): void {

    this.userRoleChange?.subscribe(role => {
      if(role == 1) {
        this.options.draggable = {
          enabled: true
        };
      }
      else {
        this.options.draggable = {
          enabled: false
        };
      }
      this.changedOptions();
    });

    this.zoomStep = (this.maxDeskSize - this.minDeskSize) / this.zoomSteps;
    this.sliderValue = this.minDeskSize + (this.maxDeskSize - this.minDeskSize) / 2;

    let enabledDrag = false;
    if(this.user?.role == 'admin') {
      enabledDrag = true;
    }

    this.options = {
      gridType: GridType.Fixed,
      compactType: CompactType.None,
      // displayGrid: 'none',
      displayGrid: 'onDrag&Resize',
      pushItems: true,
      draggable: {
        enabled: enabledDrag
      },
      resizable: {
        enabled: false
      },
      
      defaultItemCols: 1,
      defaultItemRows: 1,
      
      minItemArea: 1,
      maxItemArea: 1,

      minCols: 30,
      minRows: 30,

      fixedColWidth: this.sliderValue,
      fixedRowHeight: this.sliderValue

    };

    this.dashboard = [
      new DeskGridItem("IT-00357", false, 0, { cols: 1, rows: 1, y: 0, x: 0 }, null, null, null ),
      new DeskGridItem("IT-20567", false, 0, { cols: 1, rows: 1, y: 0, x: 2 }, null, null, null ),
      new DeskGridItem("HR-00077", true,  0, { cols: 1, rows: 1, y: 0, x: 4 }, "Dorel Bobel", "16.07.2022", "24.07.2022" ),
      new DeskGridItem("IT-30557", false, 0, { cols: 1, rows: 1, y: 1, x: 4 }, null, null, null ),
      new DeskGridItem("NET-0234", true,  0, { cols: 1, rows: 1, y: 4, x: 5 }, "Marcel Ionel", "22.07.2022", "23.07.2022" ),
      new DeskGridItem("NET-0763", false, 0, { cols: 1, rows: 1, y: 2, x: 1 }, null, null, null ),
      new DeskGridItem("NET-0345", true,  0, { cols: 1, rows: 1, y: 5, x: 5 }, "Gigicu Bobica", "26.07.2022", "26.07.2022" ),
      new DeskGridItem("JAVA-023", true,  0, { cols: 1, rows: 1, y: 3, x: 2 }, "Mitica Romica", "26.07.2022", "26.07.2022" ),
      new DeskGridItem("JAVA-234", true,  0, { cols: 1, rows: 1, y: 2, x: 2 }, "Marcel Ionel", "30.07.2022", "09.08.2022" ),
      new DeskGridItem("JS-00065", false, 0, { cols: 1, rows: 1, y: 3, x: 4 }, null, null, null ),
      new DeskGridItem("JS-00777", false, 0, { cols: 1, rows: 1, y: 0, x: 6 }, null, null, null )
    ];

    this.loadDeskDashboard();
  }

  async loadDeskDashboard() {
    this.loadingInitialData = true;
    this.dashboard = [];
    let rawDesks: Desk[] = [];
    let getDesksResponse = await this.officeDeskService.getOfficeDesks();
    getDesksResponse.subscribe({

      next: desks => {
        rawDesks = desks;
        // console.log("received desk data");
        // console.log(rawDesks);

        for(let rawDesk of rawDesks) {
          let deskGridItem = new DeskGridItem(rawDesk.name, rawDesk.occupyingUser? true : false, rawDesk.orientation, 
            { cols: 1, rows: 1, y: rawDesk.gridPositionY, x: rawDesk.gridPositionX }, 
            rawDesk.occupyingUser, rawDesk.startDate, rawDesk.endDate );
          
          this.dashboard.push(deskGridItem);
        }
        this.loadingInitialData = false;
      },

      error: err => console.log(err)
    });

  }

  changedOptions(): void {
    if (this.options.api && this.options.api.optionsChanged) {
      this.options.api.optionsChanged();
    }
  }

  updateItem($event: MouseEvent, item: any) {
    $event.preventDefault();
    $event.stopPropagation();

    if (this.dashboardUpdatedItems.indexOf(item) != -1) {
      this.dashboardUpdatedItems.splice(this.dashboardUpdatedItems.indexOf(item), 1);
    }
    this.dashboardUpdatedItems.push(item);
  }

  removeItem($event: MouseEvent, item: any): void {
    $event.preventDefault();
    $event.stopPropagation();
    this.openDeleteDialog(item);
  }

  deleteDesk(desk: DeskGridItem) {
    this.dashboardDeletedItems.push(desk);
    this.dashboard.splice(this.dashboard.indexOf(desk), 1);
  }

  addItem(): void {
    const dialogRef = this.dialog.open(DeskNamingDialogComponent, { width: '350px'});

    dialogRef.afterClosed().subscribe(result => {
      if(result != undefined) { 
        let newDesk = new DeskGridItem(result, false, 0, { x: 0, y: 0, cols: 1, rows: 1 }, null, null, null );
        this.dashboard.push(newDesk);
        this.dashboardAddedItems.push(newDesk);
      }
    });
  }

  zoomIn(): void {
    if (this.options.fixedColWidth != undefined && this.options.fixedRowHeight != undefined) {
      this.sliderValue += this.zoomStep;
      if(this.sliderValue > this.maxDeskSize) {
        this.sliderValue = this.maxDeskSize
      }
      this.options.fixedRowHeight = this.sliderValue;
      this.options.fixedColWidth = this.sliderValue;
    }
    this.changedOptions();
  }

  zoomChanged(): void {
    if (this.options.fixedColWidth != undefined && this.options.fixedRowHeight != undefined) {
      this.options.fixedRowHeight = this.sliderValue;
      this.options.fixedColWidth = this.sliderValue;
    }
    this.changedOptions();
  }

  zoomOut(): void {
    if (this.options.fixedColWidth != undefined && this.options.fixedRowHeight != undefined) {
      this.sliderValue -= this.zoomStep;
      if(this.sliderValue < this.minDeskSize) {
        this.sliderValue = this.minDeskSize;
      }
      this.options.fixedRowHeight = this.sliderValue;
      this.options.fixedColWidth = this.sliderValue;
    }
    this.changedOptions();
  }

  rotateItem(desk: DeskGridItem): void {
    desk.rotationIndex ++;
    if (desk.rotationIndex >= this.deskRotationPhases.length) {
      desk.rotationIndex = 0;
    }
    if (this.dashboardUpdatedItems.indexOf(desk) != -1) {
      this.dashboardUpdatedItems.splice(this.dashboardUpdatedItems.indexOf(desk), 1);
    }
    this.dashboardUpdatedItems.push(desk);
  }

  openNamingDialog(desk: DeskGridItem): void {
    const dialogRef = this.dialog.open(DeskNamingDialogComponent, { width: '350px', data: desk.name});

    dialogRef.afterClosed().subscribe(result => {
      // console.log(result);
      if(result != undefined) { 
        this.renameDesk(desk, result);
      }
    });
  }

  openReservationDialog(desk: DeskGridItem): void {
    const dialogRef = this.dialog.open(DeskReserveDialogComponent, { width: '500px', data: desk.name});

    dialogRef.afterClosed().subscribe(result => {
      // console.log('reservation desk result:');
      // console.log(result);
      if(result != undefined) { 
        this.reserveDesk(desk, result);
      }
    });
  }

  openDeleteDialog(desk: DeskGridItem): void {
    const dialogRef = this.dialog.open(DeskDeleteDialogComponent, { width: '350px', data: desk.name});

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if(result == 'yes') { 
        this.deleteDesk(desk);
      }
    });
  }

  renameDesk(desk: DeskGridItem, newDeskName: string): void {
    console.log(desk);
    desk.name = newDeskName;
    if (this.dashboardUpdatedItems.indexOf(desk) != -1) {
      this.dashboardUpdatedItems.splice(this.dashboardUpdatedItems.indexOf(desk), 1);
    }
    this.dashboardUpdatedItems.push(desk);
  }

  async reserveDesk(desk: DeskGridItem, date: any) {
    let dateObj = JSON.parse(date);

    console.log("you reserved desk: " + desk.name + " for period: ");
    console.log(dateObj);

    if(dateObj.start == null || dateObj.end == null) {
      this._snackBar.open("Please choose a valid period!", undefined, { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
    }
    else {
      let status = await this.officeDeskService.reserveOfficeDesk(desk.name, dateObj);
      if(status == 'success') {
        this._snackBar.open("Desk " + desk.name + " reserved successfully!", undefined, { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
        await this.loadDeskDashboard();
      }
      else {
        this._snackBar.open("Error: " + status, undefined, { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
      }
    }
  }

  async saveDashboard(): Promise<void> {

    this.savingData = true;

    console.log("Desks to be deleted: ");
    console.log(this.dashboardDeletedItems);
    console.log("Desks to be updated: ");
    console.log(this.dashboardUpdatedItems);
    console.log("Desks to be added: ");
    console.log(this.dashboardAddedItems);

    let ok = true;
    
    let successfullyDeletedItems: DeskGridItem[] = [];
    let successfullyUpdatedItems: DeskGridItem[] = [];
    let successfullyAddedItems: DeskGridItem[] = [];

    for (let deskToAdd of this.dashboardAddedItems) {
      if(this.dashboardDeletedItems.indexOf(deskToAdd) != -1) {
        this.dashboardAddedItems.splice(this.dashboardAddedItems.indexOf(deskToAdd), 1);
        this.dashboardDeletedItems.splice(this.dashboardDeletedItems.indexOf(deskToAdd), 1);
        if(this.dashboardUpdatedItems.indexOf(deskToAdd) != -1) {
          this.dashboardUpdatedItems.splice(this.dashboardUpdatedItems.indexOf(deskToAdd), 1);
        }
      }
    }

    for (let deskToUpdate of this.dashboardUpdatedItems) {
      if(this.dashboardDeletedItems.indexOf(deskToUpdate) != -1) {
        this.dashboardUpdatedItems.splice(this.dashboardUpdatedItems.indexOf(deskToUpdate), 1);
      }
    }

    if(this.dashboardAddedItems.length + this.dashboardDeletedItems.length + this.dashboardUpdatedItems.length <= 0) {
      console.log("No changes to save!");
      this.saveErrorMessage = null;
      this._snackBar.open("No changes made. Nothing to save!", undefined, { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
      this.savingData = false;
      return;
    }

    for (let deskToDelete of this.dashboardDeletedItems) {
      let status = await this.officeDeskService.deleteOfficeDesk(deskToDelete);
      if(status == 'success') {
        successfullyDeletedItems.push(deskToDelete);
      }
      else {
        this.saveErrorMessage = status;
        ok = false;
        break;
      }
    }
    for (let deletedDesk of successfullyDeletedItems) {
      this.dashboardDeletedItems.splice(this.dashboardDeletedItems.indexOf(deletedDesk), 1);
    }

    for (let deskToAdd of this.dashboardAddedItems) {
      let status = await this.officeDeskService.addOfficeDesk(deskToAdd);
      if(status == 'success') {
        successfullyAddedItems.push(deskToAdd);
      }
      else {
        this.saveErrorMessage = status;
        ok = false;
        break;
      }
    }
    for (let addedDesk of successfullyAddedItems) {
      this.dashboardAddedItems.splice(this.dashboardAddedItems.indexOf(addedDesk), 1);
    }

    for (let deskToUpdate of this.dashboardUpdatedItems) {
      let status = await this.officeDeskService.updateOfficeDesk(deskToUpdate);
      if(status == 'success') {
        successfullyUpdatedItems.push(deskToUpdate);
      }
      else {
        this.saveErrorMessage = status;
        ok = false;
        break;
      }
    }
    for (let updatedDesk of successfullyUpdatedItems) {
      this.dashboardUpdatedItems.splice(this.dashboardUpdatedItems.indexOf(updatedDesk), 1);
    }

    if(ok) {
      console.log("Saved!");
      this.saveErrorMessage = null;
      this._snackBar.open("Saved successfully!", undefined, { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
    }
    else {
      console.log("Could not save!");
    }

    this.savingData = false;
  }
}

