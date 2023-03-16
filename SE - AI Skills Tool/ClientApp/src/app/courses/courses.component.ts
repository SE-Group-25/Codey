import {Component, Inject, Input} from '@angular/core';
import { AuthService } from "../services/auth-service/auth.service";
import { HttpClient } from "@angular/common/http";
import { UserDto } from "../interfaces/user-dto";
import { CourseResponseDto } from '../interfaces/course-dto';
import { AddCourseToUserDto } from '../interfaces/add-course-to-user-dto';
import { ChatbotService } from '../services/chatbotservice/chatbot.service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent {
  @Input() public profile: boolean = false;
  @Input() public courses: any = [];

  public addedCourses: CourseResponseDto[] = [];
  public coursesSearched: boolean = false;
  private userDTo: UserDto = {};


  transferObject!: AddCourseToUserDto;

  constructor(public authService: AuthService, private _chatbot: ChatbotService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    if (this.authService.isAuthenticated && !this.coursesSearched) this.getUserCourses();
  }

  ngAfterViewChecked() {
    if (!this.authService.isAuthenticated && this.coursesSearched) this.resetCourses();
    if (this.authService.isAuthenticated && !this.coursesSearched) this.getUserCourses();
  }

  resetCourses() {
    this.coursesSearched = false;
    this.addedCourses = [];
  }

  getUserCourses() {
    try {
      this.userDTo.Id = this.authService.ID;
      this.http.post<any>(this.baseUrl + 'User/GetUserCourses', this.userDTo!).subscribe({
        next: (res: any) => {
          this.addedCourses = res;
        }
      });
      this.coursesSearched = true;
    } catch (e: unknown) {
      console.log("Courses not found.\n" + (e as Error).message);
    }
    console.log(this.addedCourses);
  }

  courseAdded(course: CourseResponseDto) {
    return this.addedCourses.includes(course);
  }

  addCourseToUser(course: CourseResponseDto) {
    // make call to add the course to user database.
    this.addedCourses.push(course);
    this.transferObject = {course: course, userId: this.authService.ID};
    this._chatbot.addCourseToUser("User/AddCourseToUser", this.transferObject).subscribe({next: res => console.log("")})
  }
}
