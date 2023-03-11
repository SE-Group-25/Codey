import { Component } from '@angular/core';
import { AuthService } from "../services/auth-service/auth.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  public courses: Array<any> = []; // TODO: change to object

  constructor(public authService: AuthService) {
    this.courses = [0,0,0,0,0,0,0,0] // TODO: get linked courses here
  }
}
