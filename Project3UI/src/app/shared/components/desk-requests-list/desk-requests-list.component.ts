import { OfficeDeskService } from './../../services/office-desk.service';
import { DeskAllocation } from './../../../objects/DeskAllocation';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-desk-requests-list',
  templateUrl: './desk-requests-list.component.html',
  styleUrls: ['./desk-requests-list.component.css']
})
export class DeskRequestsListComponent implements OnInit {

  deskReservations: DeskAllocation[] = [];
  displayedColumns: string[] = ['username', 'fromDate', 'untilDate', 'deskName'];
  loading = false;

  constructor(private officeDeskService: OfficeDeskService) { }

  async ngOnInit(): Promise<void>{
    await this.loadDeskReservationsData()
  }

  async loadDeskReservationsData() {
    this.loading = true;
    let response = await this.officeDeskService.getOfficeDesksReservations();
    response.subscribe({
      
      next: (data) => {
        this.deskReservations = data;
        this.loading = false;
      },

      error: (err) => {
        console.log(err);
      }

    })
  }

}
