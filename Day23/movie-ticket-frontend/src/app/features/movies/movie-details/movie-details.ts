import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Navbar } from '../../../shared/components/navbar/navbar';

@Component({
  selector: 'app-movie-details',
  imports: [Navbar,FormsModule],
  templateUrl: './movie-details.html',
  styleUrl: './movie-details.scss'
})
export class MovieDetails {

  movieId: string | null = null;

  selectedTheatre = '';
  selectedDate = '';
  selectedShow = '';

  theatres = [
    'PVR Cinemas',
    'INOX',
    'AGS Cinemas'
  ];

  dates = [
    '2026-11-04',
    '2026-11-05',
    '2026-11-06'
  ];

  showtimes = [
    '10:00 AM',
    '01:30 PM',
    '06:30 PM',
    '09:30 PM'
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.movieId = this.route.snapshot.paramMap.get('id');
  }

  continueToSeats() {
    if (
      !this.selectedTheatre ||
      !this.selectedDate ||
      !this.selectedShow
    ) {
      alert('Please select theatre, date and showtime.');
      return;
    }

    alert('Show selected successfully! Seat selection is coming next.');
  }
}