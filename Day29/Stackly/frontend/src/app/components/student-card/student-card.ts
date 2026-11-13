import { Component ,input} from '@angular/core';
import { studentRecord } from '../../services/student-service';
import { DatePipe ,UpperCasePipe,CurrencyPipe,NgClass} from '@angular/common';
@Component({
  selector: 'app-student-card',
  imports: [DatePipe,UpperCasePipe,CurrencyPipe,NgClass],
  templateUrl: './student-card.html',
  styleUrl: './student-card.css',
})
export class StudentCard {
  student=input.required<studentRecord>();
  isHovered: boolean = false;
}
