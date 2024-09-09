import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments';
import { Complexity } from '../models/complexity.model';

@Injectable({
  providedIn: 'root'
})
export class ComplexityService {
  private apiUrl = `${environment.apiUrl}/Complexity`;

  constructor(private http: HttpClient) { }

  getAllComplexities(): Observable<Complexity[]> {
    return this.http.get<Complexity[]>(`${this.apiUrl}`);
  }

}
