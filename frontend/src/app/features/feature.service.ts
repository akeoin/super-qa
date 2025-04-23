import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feature } from './models/feature.dto';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private apiUrl = '/api/services/app/Feature';

  constructor(private http: HttpClient) { }

  getFeatures(keyword?: string, skipCount?: number, maxResultCount?: number): Observable<any> {
    let url = `${this.apiUrl}/GetAll`;
    if (keyword || skipCount || maxResultCount) {
      url += `?keyword=${keyword || ''}&skipCount=${skipCount || 0}&maxResultCount=${maxResultCount || 10}`;
    }
    return this.http.get(url);
  }

  getFeature(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Get?id=${id}`);
  }

  createFeature(feature: Feature): Observable<any> {
    return this.http.post(`${this.apiUrl}/Create`, feature);
  }

  updateFeature(feature: Feature): Observable<any> {
    return this.http.put(`${this.apiUrl}/Update`, feature);
  }

  deleteFeature(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Delete?id=${id}`);
  }
} 