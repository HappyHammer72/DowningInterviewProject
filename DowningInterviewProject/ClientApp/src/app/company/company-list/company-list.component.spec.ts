import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { CompanyService } from '../company.service';
import { CompanyListComponent } from './company-list.component';
import { Router } from '@angular/router';
import { Company } from '../company';
import { FormsModule } from '@angular/forms';
import { Location } from '@angular/common';
import { RouterTestingModule } from '@angular/router/testing';

describe('CompanyListComponent', () => {
  let fixture: ComponentFixture<CompanyListComponent>;
  let component: CompanyListComponent;
  let mockRouter: Router;

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

  beforeEach(() => {
    let mockCompanyService: jasmine.SpyObj<CompanyService>;
    mockCompanyService = jasmine.createSpyObj('CompanyService', [
      'getAllAsync',
    ]);
    mockCompanyService.getAllAsync.and.returnValues(of(mockCompanies));
    mockRouter = jasmine.createSpyObj(['navigate']);

    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        RouterTestingModule.withRoutes([
          {
            path: 'add-company',
            component: CompanyListComponent,
            pathMatch: 'full',
          },
        ]),
      ],
      declarations: [CompanyListComponent],
      providers: [{ provide: CompanyService, useValue: mockCompanyService }],
    });

    fixture = TestBed.createComponent(CompanyListComponent);
    fixture.detectChanges();
    component = fixture.componentInstance;
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should have correct header', () => {
    const title = fixture.debugElement.query(By.css('h1')).nativeElement;
    expect(title.innerHTML).toBe('Companies');
  });

  it('should be 1 table element', () => {
    const table = fixture.debugElement.queryAll(By.css('table'));
    expect(table.length).toBe(1);
  });

  it('should be 3 table rows', () => {
    const rows = fixture.debugElement.queryAll(By.css('tr'));
    expect(rows.length).toBe(mockCompanies.length + 1); // record count + the header row
  });

  it('should navigate when the user clicks the add button', () => {
    const location: Location = TestBed.get(Location);
    const button = fixture.debugElement.query((e) => e.name === 'button');
    expect(!!button).toBe(true);
    expect(button.nativeElement.textContent.trim()).toBe('Add');
    button.nativeElement.click();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(location.path()).toBe('');
    });
  });
});
