import { Component } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-about',
  imports: [DecimalPipe],
  templateUrl: './about.html',
  styleUrl: './about.css'
})
export class AboutComponent {
  name = 'Yogaraj P';
  degree = 'B.Tech Information Technology';
  college = 'PSNA College of Engineering and Technology';
  location = 'Madurai, Tamil Nadu, India';
  cgpa = 8.7;
  graduation = 'May 2027';
  careerGoal =
    'Aspiring Full Stack Developer interested in building scalable, user-friendly web applications using modern technologies.';

  interests: string[] = [
    'Full Stack Development',
    'Web Development',
    'Backend Development',
    'Problem Solving',
    'AI/Automation'
  ];
}
