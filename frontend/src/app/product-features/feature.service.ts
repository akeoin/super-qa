import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feature, PagedResultDto } from './models/feature.model';
import { AppConsts } from '@shared/AppConsts';

@Injectable({
    providedIn: 'root'
})
export class FeatureService {
    private apiUrl = AppConsts.remoteServiceBaseUrl + '/api/services/app/Feature';

    constructor(private http: HttpClient) { }

    getFeatures(keyword?: string, status?: string, parentFeatureId?: number, skipCount?: number, maxResultCount?: number): Observable<PagedResultDto> {
        let params = new HttpParams();

        if (keyword) {
            params = params.set('Keyword', keyword);
        }
        if (status) {
            params = params.set('Status', status);
        }
        if (parentFeatureId != null) {
            params = params.set('ParentFeatureId', parentFeatureId.toString());
        }
        if (skipCount != null) {
            params = params.set('SkipCount', skipCount.toString());
        }
        if (maxResultCount != null) {
            params = params.set('MaxResultCount', maxResultCount.toString());
        }

        return this.http.get<PagedResultDto>(`${this.apiUrl}/GetAll`, { params });
    }

    getFeature(id: number): Observable<Feature> {
        return this.http.get<Feature>(`${this.apiUrl}/Get?id=${id}`);
    }

    createFeature(feature: Feature): Observable<Feature> {
        return this.http.post<Feature>(`${this.apiUrl}/Create`, feature);
    }

    updateFeature(feature: Feature): Observable<Feature> {
        return this.http.put<Feature>(`${this.apiUrl}/Update`, feature);
    }

    deleteFeature(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/Delete`, {
            params: new HttpParams().set('id', id.toString())
        });
    }
} 