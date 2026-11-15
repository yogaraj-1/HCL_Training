import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { ContactService } from '../../core/services/contact.service';
import { SOCIAL_LINKS } from '../../core/data/social-links';

@Component({
  selector: 'app-contact',
  imports: [ReactiveFormsModule],
  templateUrl: './contact.html',
  styleUrl: './contact.css'
})
export class ContactComponent {
  private contactService = inject(ContactService);

  email = 'rajaduraiyogaraj@gmail.com';
  phone = '+91 63821 42325';
  phoneHref = 'tel:+916382142325';
  location = 'Madurai, Tamil Nadu, India';
  github = SOCIAL_LINKS.find((l) => l.name === 'GitHub')?.url ?? '';
  linkedin = SOCIAL_LINKS.find((l) => l.name === 'LinkedIn')?.url ?? '';

  sending = signal(false);
  status = signal<'idle' | 'success' | 'error'>('idle');
  feedback = signal('');

  contactForm = new FormGroup({
    name: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    email: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] }),
    subject: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    message: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(10)]
    })
  });

  // Shortcuts used by the template
  get name() { return this.contactForm.controls.name; }
  get emailControl() { return this.contactForm.controls.email; }
  get subject() { return this.contactForm.controls.subject; }
  get message() { return this.contactForm.controls.message; }

  onSubmit(): void {
    if (this.contactForm.invalid) {
      this.contactForm.markAllAsTouched();
      return;
    }

    this.sending.set(true);
    this.status.set('idle');

    this.contactService
      .sendMessage(this.contactForm.getRawValue())
      .pipe(finalize(() => this.sending.set(false)))
      .subscribe({
        next: (res) => {
          this.status.set('success');
          this.feedback.set(res.message);
          this.contactForm.reset();
        },
        error: () => {
          this.status.set('error');
          this.feedback.set('Something went wrong. Please try again.');
        }
      });
  }

  onReset(): void {
    this.contactForm.reset();
    this.status.set('idle');
  }
}
