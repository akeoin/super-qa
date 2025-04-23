import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TestCase, PagedResultDto } from './models/test-case.dto';
import { AppConsts } from '@shared/AppConsts';

@Injectable({
  providedIn: 'root'
})
export class TestCaseService {
  private apiUrl = AppConsts.remoteServiceBaseUrl + '/api/services/app/TestCase';

  constructor(private http: HttpClient) {}

  getTestCases(keyword?: string, status?: string, scenarioId?: number, skipCount?: number, maxResultCount?: number): Observable<PagedResultDto> {
    let params = new HttpParams();

    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    if (status) {
      params = params.set('Status', status);
    }
    if (scenarioId != null) {
      params = params.set('ScenarioId', scenarioId.toString());
    }
    if (skipCount != null) {
      params = params.set('SkipCount', skipCount.toString());
    }
    if (maxResultCount != null) {
      params = params.set('MaxResultCount', maxResultCount.toString());
    }

    console.log('API URL:', `${this.apiUrl}/GetAll`);
    console.log('Request params:', params.toString());

    return this.http.get<PagedResultDto>(`${this.apiUrl}/GetAll`, { params });
  }

  getTestCase(id: number): Observable<TestCase> {
    return this.http.get<TestCase>(`${this.apiUrl}/Get?id=${id}`);
  }

  createTestCase(testCase: TestCase): Observable<TestCase> {
    return this.http.post<TestCase>(`${this.apiUrl}/Create`, testCase);
  }

  updateTestCase(testCase: TestCase): Observable<TestCase> {
    return this.http.put<TestCase>(`${this.apiUrl}/Update`, testCase);
  }

  deleteTestCase(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Delete`, {
      params: new HttpParams().set('id', id.toString())
    });
  }
}
