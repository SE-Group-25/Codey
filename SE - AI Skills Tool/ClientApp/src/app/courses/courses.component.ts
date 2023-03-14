import {Component, Inject, Input} from '@angular/core';
import { AuthService } from "../services/auth-service/auth.service";
import { HttpClient } from "@angular/common/http";
import { UserDto } from "../interfaces/user-dto";

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent {
  @Input() public profile: boolean = false;
  @Input() public courses: any = [];

  constructor(public authService: AuthService) { }

  addCourseToUser(course: any) {
    // TODO: make call to add the course to user database.
  }
}
