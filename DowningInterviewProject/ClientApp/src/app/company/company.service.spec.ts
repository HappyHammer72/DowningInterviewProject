import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from 'src/environments/environment';
import { CompanyService } from './company.service';
import { Company } from './company';

const mockCompany: Company = {
  name: 'Company 1',
  code: 'X',
  createdDate: new Date(),
  sharePrice: 1,
};

const mockCompanies: Company[] = [
  {
    name: 'Company 1',
    code: 'X',
    createdDate: new Date(),
    sharePrice: 1,
  },
  {
    name: 'Company 2',
    code: 'Y',
    createdDate: new Date(),
    sharePrice: 2,
  },
  {
    name: 'Company 3',
    code: 'Z',
    createdDate: new Date(),
    sharePrice: 3,
  },
];

describe('CompanyService', () => {
  let httpTestingController: HttpTestingController;
  let companyService: CompanyService;
  const apiUrl = `${environment.serverUrl}/api/company`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CompanyService],
    });
    httpTestingController = TestBed.inject(HttpTestingController);
    companyService = TestBed.inject(CompanyService);
  });

  it('should call getAllAsync with the correct url', () => {
    companyService.getAllAsync().subscribe((data) => {
      expect(data[0]).toEqual(mockCompanies[0]);
    });
    const req = httpTestingController.expectOne(`${apiUrl}`);
    req.flush(mockCompanies);
    expect(req.request.method).toBe('GET');
    httpTestingController.verify();
  });

  it('should call insert with the correct url', () => {
    companyService.insertAsync(mockCompany).subscribe((data) => {
      expect(JSON.stringify(data)).toEqual(JSON.stringify(mockCompany));
    });
    const req = httpTestingController.expectOne(`${apiUrl}`);
    req.flush(mockCompany);
    expect(req.request.method).toBe('POST');
    httpTestingController.verify();
  });
});
