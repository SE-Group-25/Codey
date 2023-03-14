import { Component, Inject } from '@angular/core';
import { AuthService } from "../services/auth-service/auth.service";
import { HttpClient } from "@angular/common/http";
import { UserDto } from "../interfaces/user-dto";
import { CourseResponseDto } from "../interfaces/course-dto";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  public courses: any = []; // TODO: change to object
  private userDTo: UserDto = {};
  constructor(public authService: AuthService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    while (!authService.isInitialised) { }
    this.userDTo.Id = authService.ID;
    http.post<any>(baseUrl + 'User/GetUserCourses', this.userDTo!).subscribe({
      next: (res: any) => {
        this.courses = res;
        console.log(res);
      }
    });
  }
}
