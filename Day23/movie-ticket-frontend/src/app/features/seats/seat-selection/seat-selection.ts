import { Component } from '@angular/core';
import { Navbar } from '../../../shared/components/navbar/navbar';

interface Seat {
  id: number;
  row: string;
  number: number;
  status: 'available' | 'selected' | 'booked';
  price: number;
}

@Component({
  selector: 'app-seat-selection',
  imports: [Navbar],
  templateUrl: './seat-selection.html',
  styleUrl: './seat-selection.scss'
})
export class SeatSelection {

  seats: Seat[] = [];

  constructor() {
    this.generateSeats();
  }

  generateSeats() {
    const rows = ['A', 'B', 'C', 'D', 'E'];

    let id = 1;

    rows.forEach(row => {
      for (let number = 1; number <= 8; number++) {

        const booked = [3, 6].includes(number) && ['A', 'C'].includes(row);

        this.seats.push({
          id: id++,
          row,
          number,
          status: booked ? 'booked' : 'available',
          price: row === 'A' || row === 'B' ? 200 : 150
        });

      }
    });
  }

  selectSeat(seat: Seat) {

    if (seat.status === 'booked') {
      return;
    }

    seat.status =
      seat.status === 'selected' ? 'available' : 'selected';

  }

  get selectedSeats() {
    return this.seats.filter(seat => seat.status === 'selected');
  }

  get totalAmount() {
    return this.selectedSeats.reduce(
      (total, seat) => total + seat.price,
      0
    );
  }

  continueBooking() {

    if (this.selectedSeats.length === 0) {
      alert('Please select at least one seat.');
      return;
    }

    alert(
      `Selected ${this.selectedSeats.length} seats. Total: ₹${this.totalAmount}`
    );

  }

}