import { Directive, ElementRef, HostListener, Input, inject } from '@angular/core';

/** Highlights ONLY the element it is placed on while the mouse is over it. */
@Directive({ selector: '[appHighlight]' })
export class HighlightDirective {
  private el = inject(ElementRef<HTMLElement>);

  @Input('appHighlight') color = '';

  @HostListener('mouseenter') onEnter(): void {
    this.setBackground(this.color || 'rgba(56, 189, 248, 0.25)');
  }

  @HostListener('mouseleave') onLeave(): void {
    this.setBackground('');
  }

  private setBackground(value: string): void {
    this.el.nativeElement.style.backgroundColor = value;
  }
}
