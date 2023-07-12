import { CommonModule } from '@angular/common';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { of } from 'rxjs';
import { AddCompanyComponent } from './add-company.component';
import { CompanyService } from '../company.service';

describe('AddCompanyComponent', () => {
  let fixture: ComponentFixture<AddCompanyComponent>;
  let component: AddCompanyComponent;
  let mockRouter: Router;

  beforeEach(() => {
    let mockCompanyService: jasmine.SpyObj<CompanyService>;
    mockCompanyService = jasmine.createSpyObj('CompanyService', [
      'insertAsync',
    ]);
    mockCompanyService.insertAsync.and.returnValues(of());
    mockRouter = jasmine.createSpyObj(['navigate']);

    TestBed.configureTestingModule({
      imports: [CommonModule, ReactiveFormsModule, RouterModule],
      declarations: [AddCompanyComponent],
      providers: [
        { provide: CompanyService, useValue: mockCompanyService },
        { provide: Router, useValue: mockRouter },
        FormBuilder,
      ],
    });
  });

  describe('when adding a company', () => {
    beforeEach(() => {
      fixture = TestBed.createComponent(AddCompanyComponent);
      fixture.detectChanges();
      component = fixture.componentInstance;
    });

    it('should create component', () => {
      // Assert
      expect(component).toBeTruthy();
    });

    describe('form validation', () => {
      beforeEach(() => {
        // Arrange
        component.editForm.patchValue({
          name: 'Company 1',
          code: 'X',
          createdDate: new Date(),
          sharePrice: 1,
        });
        component.editForm.markAsDirty();
        fixture.detectChanges();
      });

      it('should be valid when form is valid', () => {
        // Assert
        expect(component.editForm.valid).toBeTrue();
      });

      it('should be invalid when name is empty', () => {
        // Act
        component.editForm.patchValue({
          name: '',
        });

        // Assert
        expect(component.editForm.valid).toBeFalse();
        expect(component.form['name'].valid).toBeFalse();
      });

      it('should be invalid when code is empty', () => {
        // Act
        component.editForm.patchValue({
          code: '',
        });

        // Assert
        expect(component.editForm.valid).toBeFalse();
        expect(component.form['code'].valid).toBeFalse();
      });

      it('should be invalid when code is over 10 characters', () => {
        // Act
        component.editForm.patchValue({
          code: 'xxxxxxxxxx1',
        });

        // Assert
        expect(component.editForm.valid).toBeFalse();
        expect(component.form['code'].valid).toBeFalse();
      });

      it('should be invalid when name is over 100 characters', () => {
        // Act
        component.editForm.patchValue({
          name: 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx1',
        });

        // Assert
        expect(component.editForm.valid).toBeFalse();
        expect(component.form['name'].valid).toBeFalse();
      });
    });
  });
});
