import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class TokenService {

    constructor(private oidcSecurityService: OidcSecurityService) {
    }

    async getToken(): Promise<string> {
        return new Promise<string>(resolve => {
            this.oidcSecurityService.getAccessToken().subscribe( (data) => {
                resolve(data);
            });
        });
    }

}