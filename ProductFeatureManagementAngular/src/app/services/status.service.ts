import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Status } from '../models/status.model';
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class StatusService {
  private apiUrl = `${environment.apiUrl}/Status`;

  constructor(private http: HttpClient) { }

  getAllStatuses(): Observable<Status[]> {
    return this.http.get<Status[]>(`${this.apiUrl}`);
  }
}
