import { Component, Input } from '@angular/core';
import { Stat } from '../../core/models/stat.model';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.html',
  styleUrl: './stats.css'
})
export class StatsComponent {
  // The parent (HomeComponent) sends the data in: <app-stats [stats]="stats" />
  @Input() stats: Stat[] = [];
}
