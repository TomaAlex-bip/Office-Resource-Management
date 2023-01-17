import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthenticationService } from './../../shared/services/authentication.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  hide: boolean = true;
  hide2: boolean = true;

  username: string  = "";
  password: string  = "";
  repeatPassword : string = "";

  registerErrorMessage: string | null = null;

  @Output() onLoginClicked: EventEmitter<void> = new EventEmitter<void>();
  @Output() onRegisterSuccess: EventEmitter<void> = new EventEmitter<void>();
  
  constructor(private _authenticationService: AuthenticationService, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  onLogin() {
    this.onLoginClicked.emit();
  }

  async onRegister() {
    if(this.username.length <= 0 || this.password.length <= 0 || this.repeatPassword.length <= 0) {
      this.registerErrorMessage = "Please complete all the fields";
      return;
    }

    if(this.password != this.repeatPassword) {
      this.registerErrorMessage = "Passwords do not match";
      return;
    }

    let response = await this._authenticationService.registerUser(this.username, this.password);

    if(response == 'success') {
      this.registerErrorMessage = null;
      // this.username = "";
      this.password = "";
      this.repeatPassword = "";
      // this.onRegisterSuccess.emit();
      this._snackBar.open("Registered successfully! Now you can Login.", undefined, { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] } );
    }
    else {
      this.registerErrorMessage = response;
    }
  }

}
