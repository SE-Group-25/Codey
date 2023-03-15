import {AfterViewChecked, Component, Inject} from '@angular/core';
import {UserDto} from "../interfaces/user-dto";
import {AuthService} from "../services/auth-service/auth.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements AfterViewChecked {
  public coursesSearched: boolean = false;
  public userCourses: any = [];
  private userDTo: UserDto = {};
  constructor(public authService: AuthService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngAfterViewChecked() {
    if (!this.authService.isAuthenticated && this.coursesSearched) this.resetCourses();
    if (this.authService.isAuthenticated && !this.coursesSearched) this.getUserCourses();
  }

  resetCourses() {
    this.coursesSearched = false;
    this.userCourses = [];
  }

  getUserCourses() {
    try {
      this.userDTo.Id = this.authService.ID;
      this.http.post<any>(this.baseUrl + 'User/GetUserCourses', this.userDTo!).subscribe({
        next: (res: any) => {
          this.userCourses = res;
        }
      });
      this.coursesSearched = true;
    } catch (e: unknown) {
      console.log("Courses not found.\n" + (e as Error).message);
    }
  }
}
