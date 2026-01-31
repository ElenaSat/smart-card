import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, catchError, of, finalize, map } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable()
export abstract class BaseCrudService<T, TCreate = Partial<T>> {
    protected items$ = new BehaviorSubject<T[]>([]);
    protected loading$ = new BehaviorSubject<boolean>(false);
    protected error$ = new BehaviorSubject<string | null>(null);

    protected abstract endpoint: string;
    protected abstract idField: keyof T;

    constructor(protected http: HttpClient) { }

    get items(): Observable<T[]> {
        return this.items$.asObservable();
    }

    get loading(): Observable<boolean> {
        return this.loading$.asObservable();
    }

    get error(): Observable<string | null> {
        return this.error$.asObservable();
    }

    protected get baseUrl(): string {
        return `${environment.apiUrl}/${this.endpoint}`;
    }

    getAll(): Observable<T[]> {
        this.loading$.next(true);
        this.error$.next(null);
        return this.http.get<T[]>(this.baseUrl).pipe(
            tap(items => this.items$.next(items)),
            catchError(err => {
                this.error$.next(err?.message || 'Error loading data');
                return of([]);
            }),
            finalize(() => this.loading$.next(false))
        );
    }

    getById(id: number): Observable<T | null> {
        this.loading$.next(true);
        this.error$.next(null);
        return this.http.get<T>(`${this.baseUrl}/${id}`).pipe(
            catchError(err => {
                this.error$.next(err?.message || 'Error loading item');
                return of(null);
            }),
            finalize(() => this.loading$.next(false))
        );
    }

    create(item: TCreate): Observable<number> {
        this.loading$.next(true);
        this.error$.next(null);
        return this.http.post<number>(this.baseUrl, item).pipe(
            tap(() => this.refresh()),
            catchError(err => {
                this.error$.next(err?.message || 'Error creating item');
                return of(-1);
            }),
            finalize(() => this.loading$.next(false))
        );
    }

    update(id: number, item: T): Observable<boolean> {
        this.loading$.next(true);
        this.error$.next(null);
        return this.http.put(`${this.baseUrl}/${id}`, item).pipe(
            map(() => true),
            tap(() => this.refresh()),
            catchError(err => {
                this.error$.next(err?.message || 'Error updating item');
                return of(false);
            }),
            finalize(() => this.loading$.next(false))
        );
    }

    delete(id: number): Observable<boolean> {
        this.loading$.next(true);
        this.error$.next(null);
        return this.http.delete(`${this.baseUrl}/${id}`).pipe(
            map(() => true),
            tap(() => this.refresh()),
            catchError(err => {
                this.error$.next(err?.message || 'Error deleting item');
                return of(false);
            }),
            finalize(() => this.loading$.next(false))
        );
    }

    refresh(): void {
        this.getAll().subscribe();
    }
}

