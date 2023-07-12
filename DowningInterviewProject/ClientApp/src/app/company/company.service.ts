import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Company } from './company';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  private url = `${environment.serverUrl}/api/company`;

  public constructor(private http: HttpClient) {}

  public getAllAsync(): Observable<Company[]> {
    return this.http.get<Company[]>(this.url);
  }

  public insertAsync(company: Company): Observable<Company> {
    company.createdDate = new Date();
    return this.http.post(this.url, company);
  }
}
