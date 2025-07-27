import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Event {
  id: number;
  title: string;
  description: string;
  eventDate: string;
  location?: string;
  address?: string;
  organizerId: number;
  organizerName: string;
  createdAt: string;
  isActive: boolean;
  maxAttendees: number;
  currentAttendees: number;
  eventType?: string;
  isUserRegistered: boolean;
}

export interface EventDetail extends Event {
  attendees: EventAttendee[];
}

export interface EventAttendee {
  id: number;
  userId: number;
  userName: string;
  registeredAt: string;
  isConfirmed: boolean;
}

export interface CreateEventRequest {
  title: string;
  description: string;
  eventDate: string;
  location?: string;
  address?: string;
  maxAttendees: number;
  eventType?: string;
}

export interface UpdateEventRequest {
  title: string;
  description: string;
  eventDate: string;
  location?: string;
  address?: string;
  maxAttendees: number;
  eventType?: string;
}

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private apiUrl = 'http://localhost:5106/api/events';

  constructor(private http: HttpClient) { }

  getEvents(page: number = 1, pageSize: number = 10): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  getEvent(id: number): Observable<EventDetail> {
    return this.http.get<EventDetail>(`${this.apiUrl}/${id}`);
  }

  createEvent(request: CreateEventRequest): Observable<Event> {
    return this.http.post<Event>(this.apiUrl, request);
  }

  updateEvent(id: number, request: UpdateEventRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteEvent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  registerForEvent(id: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${id}/register`, {});
  }

  unregisterFromEvent(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}/register`);
  }
} 