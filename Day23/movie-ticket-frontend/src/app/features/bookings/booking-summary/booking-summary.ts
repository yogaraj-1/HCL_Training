import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Navbar } from '../../../shared/components/navbar/navbar';

@Component({
  selector: 'app-booking-summary',
  imports: [Navbar],
  templateUrl: './booking-summary.html',
  styleUrl: './booking-summary.scss'
})
export class BookingSummary {

  movieName = 'Leo';
  theatreName = 'PVR Cinemas';
  showDate = '04 November 2026';
  showTime = '06:30 PM';

  selectedSeats = ['A1', 'A2', 'B3'];

  ticketPrice = 200;
  convenienceFee = 30;

  get subtotal() {
    return this.selectedSeats.length * this.ticketPrice;
  }

  get totalAmount() {
    return this.subtotal + this.convenienceFee;
  }

  constructor(private router: Router) {}

  confirmBooking() {
    alert('Booking confirmed successfully!');
    this.router.navigate(['/my-bookings']);
  }

  goBack() {
    this.router.navigate(['/seat-selection']);
  }
}