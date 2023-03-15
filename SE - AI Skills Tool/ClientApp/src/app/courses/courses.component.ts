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

  transferObject!: AddCourseToUserDto;

  constructor(public authService: AuthService, private _chatbot: ChatbotService) { }

  addCourseToUser(course: CourseResponseDto) {
    // TODO: make call to add the course to user database.
    this.transferObject = {course: course, userId: this.authService.ID};
    this._chatbot.addCourseToUser("User/AddCourseToUser", this.transferObject).subscribe({next: res => console.log(res)})
  }
}
