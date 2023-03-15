import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from "./login/login.component";
import { ChatbotComponent } from "./chatbot/chatbot.component";
import { CoursesComponent } from "./courses/courses.component";
import {ProfileComponent} from "./profile/profile.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    ChatbotComponent,
    CoursesComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'chatbot', component: ChatbotComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'PageNotFound', component: HomeComponent }, // TODO: replace with PageNotFoundComponent
      { path: '**', redirectTo: 'PageNotFound'}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
