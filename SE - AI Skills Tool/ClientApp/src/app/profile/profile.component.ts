import { Component, Inject } from '@angular/core';
import { AuthService } from "../services/auth-service/auth.service";
import {HttpClient, HttpEvent, HttpParams} from "@angular/common/http";
import {MessageResponseDto} from "../interfaces/message-dto";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  public courses: any = []; // TODO: change to object
  private userDTo: UserDTo = {};
  constructor(public authService: AuthService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.courses = [0,0,0,0,0,0,0,0] // TODO: get linked courses here
    while (!authService.isAuthenticated) { }
    this.userDTo.Id = authService.ID;
    http.post<CourseResponseDTo[]>(baseUrl + 'User/GetUserCourses', this.userDTo!).subscribe({
      next: (res: CourseResponseDTo[]) => {
        this.courses = res;
        console.log(res);
      }
    });
  }
}

interface UserDTo {
  Id?: string;
}

interface CourseResponseDTo {
  Description?: string;
  Importance?: string;
  Id?: string;
  Keywords?: string;
  Title?: string;
  Tags?: string;
  Url?: string;
  IconUrl?: string;
  IsCostRequired?: boolean;
}
