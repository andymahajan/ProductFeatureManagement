import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments';
import { FeatureDto } from '../models/featuredto.model';
import { Feature } from '../models/feature.model';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private apiUrl = `${environment.apiUrl}/features`; // Use apiUrl from environment

  constructor(private http: HttpClient) { }

  getAllFeatures(): Observable<FeatureDto[]> {
    return this.http.get<FeatureDto[]>(this.apiUrl);
  }

  getFeatureById(id: number): Observable<Feature> {
    return this.http.get<Feature>(`${this.apiUrl}/${id}`);
  }

  addFeature(feature: Feature): Observable<Feature> {
    return this.http.post<Feature>(this.apiUrl, feature);
  }

  updateFeature(feature: Feature): Observable<Feature> {
    return this.http.put<Feature>(`${this.apiUrl}/${feature.featuresId}`, feature);
  }

  deleteFeature(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
