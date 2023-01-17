import { TokenService } from './token.service';
import { DeskAllocation } from './../../objects/DeskAllocation';
import { DeskReservationViewModel } from './../../objects/DeskReservationViewModel';
import { DeskViewModel } from './../../objects/DeskViewModel';
import { DeskGridItem } from './../../objects/DeskIGridItem';
import { environment } from './../../../environments/environment';
import { Desk } from './../../objects/Desk';
import { HttpRequestService } from './http-request.service';
import { Injectable } from "@angular/core";
import { tap, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
})
export class OfficeDeskService {

    private desksReadUrl = '/api/read/desks';
    private desksAdminUrl = '/api/admin/desks';
    private reservationUrl = '/api/users/desks/allocations';

    constructor(private tokenService: TokenService, private httpClient: HttpClient, private httpRequestService: HttpRequestService) {

    }

    async getOfficeDesks(): Promise<Observable<Desk[]>> {
        let url = environment.apiUrl + this.desksReadUrl;

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        return this.httpClient.get<Desk[]>(url, options).pipe(
            tap(data => console.log(data))
        );

    }

    async getOfficeDesksReservations(): Promise<Observable<DeskAllocation[]>> {
        let url = environment.apiUrl + this.reservationUrl;

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        return this.httpClient.get<DeskAllocation[]>(url, options).pipe(
            tap(data => console.log(data))
        );

    }

    async addOfficeDesk(deskGridItem: DeskGridItem): Promise<string> {
        let url = environment.apiUrl + this.desksAdminUrl;
        let deskToAdd = new DeskViewModel(deskGridItem.name, deskGridItem.item.x, deskGridItem.item.y, deskGridItem.rotationIndex);

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        let response = await this.httpRequestService.makePostRequest(deskToAdd, options, url);
        // console.log("add office desk response: ");
        // console.log(response);
        
        if(response.status == 403 || response.status == 401) {
            return "Action not permitted for your user role!";
        }
        else if(response.error == null) {
            return "success";
        }
        else {
            return response.error.errorMessage;
        }
    }

    async updateOfficeDesk(deskGridItem: DeskGridItem): Promise<string> {
        let url = environment.apiUrl + this.desksAdminUrl + '/' + deskGridItem.oldName;
        let deskToAdd = new DeskViewModel(deskGridItem.name, deskGridItem.item.x, deskGridItem.item.y, deskGridItem.rotationIndex);

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        let response = await this.httpRequestService.makeUpdateRequest(deskToAdd, options, url);
        // console.log("update office desk response: ");
        // console.log(response);

        if(response.status == 403 || response.status == 401) {
            return "Action not permitted for your user role!";
        }
        else if(response.error == null) {
            return "success";
        }
        else {
            return response.error.errorMessage;
        }
    }

    async deleteOfficeDesk(deskGridItem: DeskGridItem): Promise<string> {
        let url = environment.apiUrl + this.desksAdminUrl + '/' + deskGridItem.oldName;

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        let response = await this.httpRequestService.makeDeleteRequest(options, url);
        // console.log("delete office desk response: ");
        // console.log(response);

        if(response.status == 403 || response.status == 401) {
            return "Action not permitted for your user role!";
        }
        else if(response.error == null) {
            return "success";
        }
        else {
            return response.error.errorMessage;
        }
    }

    async reserveOfficeDesk(deskName: string, date: any): Promise<string> {
        let url = environment.apiUrl + this.reservationUrl;

        let startDate = new Date(date.start);
        let reservedFrom = [startDate.getFullYear(), 
            (startDate.getMonth() + 1).toString().padStart(2, '0'), 
            startDate.getDate().toString().padStart(2, '0')]
            .join('-') + 'T00:00:00';
        let endDate = new Date(date.end);
        let reservedUntil = [endDate.getFullYear(), 
            (endDate.getMonth() + 1).toString().padStart(2, '0'), 
            endDate.getDate().toString().padStart(2, '0')]
            .join('-') + 'T00:00:00';

        // console.log("reservation dates from service: " + reservedFrom + " - " + reservedUntil);

        let reservationToMake = new DeskReservationViewModel(deskName, reservedFrom, reservedUntil);

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        let response = await this.httpRequestService.makePostRequest(reservationToMake, options, url);
        // console.log("reservation office desk response: ");
        // console.log(response);

        if(response.status == 403 || response.status == 401) {
            return "Action not permitted for your user role!";
        }
        else if(response.error == null) {
            return "success";
        }
        else {
            return response.error.errorMessage;
        }
    }

}