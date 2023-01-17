import { TokenService } from './token.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpRequestService } from './http-request.service';
import { UserViewModel } from './../../objects/UserViewModel';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserResponse } from 'src/app/objects/UserResponse';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    registerUrl = "/api/users/registration";
    userInfoUrl = "/api/read/userinfo";

    constructor(private httpRequestService: HttpRequestService, private httpClient: HttpClient, private tokenService: TokenService) {

    }

    async registerUser(username: string, password: string): Promise<string> {

        let url = environment.apiUrl + this.registerUrl;
        let userToRegister = new UserViewModel(username, password);

        let response = await this.httpRequestService.makePostRequest(userToRegister, undefined, url);

        if(response.error == null) {
            return "success";
        }
        else if(response.error.errors != undefined) {
            if(response.error.errors["Username"] != undefined){
                return response.error.errors["Username"];
            }
            else if(response.error.errors["Password"] != undefined){
                return response.error.errors["Password"];
            }
            else {
                return "Unexpected Error!";
            }
        }
        else {
            return response.error.errorMessage;
        }
    }

    async getUser(): Promise<UserResponse> {
        let url = environment.apiUrl + this.userInfoUrl;

        let token = await this.tokenService.getToken();
        let options = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        return await new Promise<UserResponse>(resolve => {
            this.httpClient.get<UserResponse>(url, options).subscribe( (data) => {
                    resolve(data);
                }
            );
        });
    }
}