import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-movie-card',
  imports: [RouterLink],
  templateUrl: './movie-card.html',
  styleUrl: './movie-card.scss'
})
export class MovieCard {
  @Input() id!: number;
  @Input() title!: string;
  @Input() genre!: string;
  @Input() rating!: number;
  @Input() poster!: string;
}