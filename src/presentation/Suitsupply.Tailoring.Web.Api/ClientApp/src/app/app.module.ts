import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AlterationsComponent } from './alterations/alterations.component';
import { NewAlterationsComponent } from "./alterations/new/new-alteration.component";
import {ViewAlterationsComponent} from "./alterations/view/view-alteration.component";

import { ShorteningPipe} from "./pipes/shortening.pipe";

@NgModule({
  declarations: [
    // Components
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AlterationsComponent,
    NewAlterationsComponent,
    ViewAlterationsComponent,
    // Pipes
    ShorteningPipe
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'app', component: HomeComponent},
      {path: 'alterations', component: AlterationsComponent},
      {path: 'alterations/new', component: NewAlterationsComponent},
      {path: 'alterations/view/:id', component: ViewAlterationsComponent}
    ]),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
