import { computed, Signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { BehaviorSubject, Observable, catchError, map, of, startWith, switchMap } from 'rxjs';

export type LoadState<T> =
  | { status: 'loading' }
  | { status: 'error' }
  | { status: 'success'; data: T };

/** Turns any Observable into a stream of loading -> success | error states. */
export function toLoadState<T>() {
  return (source$: Observable<T>): Observable<LoadState<T>> =>
    source$.pipe(
      map((data): LoadState<T> => ({ status: 'success', data })),
      catchError(() => of<LoadState<T>>({ status: 'error' })),
      startWith<LoadState<T>>({ status: 'loading' })
    );
}

export interface Loader<T> {
  data: Signal<T | null>;
  loading: Signal<boolean>;
  error: Signal<boolean>;
  reload: () => void;
}

/**
 * Wraps a service call in signals (data / loading / error) with a reload().
 * Must be called in an injection context (e.g. a class field initializer).
 * No manual subscribe(): toSignal() cleans up automatically.
 */
export function createLoader<T>(factory: () => Observable<T>): Loader<T> {
  const trigger = new BehaviorSubject<void>(undefined);
  const state = toSignal(
    trigger.pipe(switchMap(() => factory().pipe(toLoadState<T>()))),
    { requireSync: true }
  );
  return {
    data: computed(() => {
      const s = state();
      return s.status === 'success' ? s.data : null;
    }),
    loading: computed(() => state().status === 'loading'),
    error: computed(() => state().status === 'error'),
    reload: () => trigger.next()
  };
}
