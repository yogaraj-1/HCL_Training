import { Component ,input} from '@angular/core';
import { studentRecord } from '../../services/student-service';
@Component({
  selector: 'app-student-card',
  imports: [],
  templateUrl: './student-card.html',
  styleUrl: './student-card.css',
})
export class StudentCard {
  student=input.required<studentRecord>();
}
