import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, delay, of } from 'rxjs';
import { ContactMessage, ContactResponse } from '../models/contact-message.model';

@Injectable({ providedIn: 'root' })
export class ContactService {
  private http = inject(HttpClient);

  /** Flip to true once POST /api/contact exists. */
  private useBackend = false;
  private endpoint = '/api/contact';

  sendMessage(message: ContactMessage): Observable<ContactResponse> {
    if (this.useBackend) {
      return this.http.post<ContactResponse>(this.endpoint, message);
    }
    // Simulated success for now.
    return of({ success: true, message: `Thanks ${message.name}! Your message was sent.` }).pipe(
      delay(800)
    );
  }
}
