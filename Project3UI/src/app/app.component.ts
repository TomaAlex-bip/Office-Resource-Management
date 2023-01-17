import { AuthenticationService } from './shared/services/authentication.service';
import { Component, Input, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Subject } from 'rxjs';
import { User } from './objects/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  currentUser: User | null = null;

  @Input() changeUserRoleSubject: Subject<number> = new Subject<number>();

  constructor(public oidcSecurityService: OidcSecurityService, private authenticationService: AuthenticationService) {

  }

  async ngOnInit() {
    this.oidcSecurityService.checkAuth().subscribe(async ({ isAuthenticated, userData, idToken, accessToken }) => {
      if (accessToken != null && accessToken != undefined) {
        this.currentUser = new User(userData.username, userData.role);
        console.log(this.currentUser);
      }
    });
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
    this.currentUser = null;
  }



}
