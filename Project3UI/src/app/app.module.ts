import { environment } from 'src/environments/environment';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { BodyComponent } from './body/body.component';
import { OfficeGridComponent } from './shared/components/office-grid/office-grid.component';
import { DeskNamingDialogComponent } from './shared/components/desk-naming-dialog/desk-naming-dialog.component';
import { DeskDeleteDialogComponent } from './shared/components/desk-delete-dialog/desk-delete-dialog.component';
import { DeskReserveDialogComponent } from './shared/components/desk-reserve-dialog/desk-reserve-dialog.component';
import { DeskRequestsListComponent } from './shared/components/desk-requests-list/desk-requests-list.component';
import { RegistrationComponent } from './body/registration/registration.component';

import { DateOnlyPipe } from './shared/pipes/date-only.pipe';
import { GridsterModule } from 'angular-gridster2';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatButtonModule} from '@angular/material/button';
import {MatSliderModule} from '@angular/material/slider';
import {FormsModule} from '@angular/forms';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTabsModule} from '@angular/material/tabs';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatMenuModule} from '@angular/material/menu';
import {MatDialogModule} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatTableModule} from '@angular/material/table';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    BodyComponent,
    OfficeGridComponent,
    DeskNamingDialogComponent,
    DeskDeleteDialogComponent,
    DeskReserveDialogComponent,
    DateOnlyPipe,
    DeskRequestsListComponent,
    RegistrationComponent
  ],
  imports: [
    RouterModule.forRoot([]),
    AuthModule.forRoot({
      config: [
        {
          configId: 'users',
          authority: environment.authUrl,
          redirectUrl: window.location.origin,
          postLogoutRedirectUri: window.location.origin,
          clientId: 'officeClient.users',
          scope: 'openid office.users office.read office.admin',
          responseType: 'code',
          disablePkce: false,
          logLevel: environment.production==true ? LogLevel.None : LogLevel.Debug,
        },
        // {
        //   configId: 'admin',
        //   authority: environment.authUrl,
        //   redirectUrl: window.location.origin,
        //   postLogoutRedirectUri: window.location.origin,
        //   clientId: 'officeClient.admin',
        //   scope: 'openid office.admin office.read',
        //   responseType: 'code',
        //   logLevel: LogLevel.Debug
        // }
      ]
    }),
    BrowserModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    GridsterModule,
    MatSliderModule,
    FormsModule,
    MatTooltipModule,
    MatTabsModule,
    BrowserAnimationsModule,
    MatMenuModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatSnackBarModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
