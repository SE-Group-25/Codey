import { CourseResponseDto } from './course-dto';

export interface AddCourseToUserDto {
  userId: string;
  course: CourseResponseDto;
}
