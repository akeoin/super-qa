import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TestCaseDto } from '../models/test-case.dto';

@Injectable({
  providedIn: 'root'
})
export class TestCaseService {
  private apiUrl = '/api/test-cases';

  constructor(private http: HttpClient) { }

  getAll(): Observable<TestCaseDto[]> {
    return this.http.get<TestCaseDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<TestCaseDto> {
    return this.http.get<TestCaseDto>(`${this.apiUrl}/${id}`);
  }

  create(testCase: TestCaseDto): Observable<TestCaseDto> {
    return this.http.post<TestCaseDto>(this.apiUrl, testCase);
  }

  update(id: number, testCase: TestCaseDto): Observable<TestCaseDto> {
    return this.http.put<TestCaseDto>(`${this.apiUrl}/${id}`, testCase);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
} 