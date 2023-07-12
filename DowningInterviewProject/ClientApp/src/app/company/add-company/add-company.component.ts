import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../company.service';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Company } from '../company';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: [],
})
export class AddCompanyComponent implements OnInit {
  public editForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    code: new FormControl(''),
    sharePrice: new FormControl(''),
  });
  private company!: Company;
  public errorMessage = '';

  public constructor(
    private service: CompanyService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  public ngOnInit(): void {
    this.editForm = this.formBuilder.group({
      name: [null, [Validators.required, Validators.maxLength(100)]],
      code: [null, [Validators.required, Validators.maxLength(10)]],
      sharePrice: [null, Validators.maxLength(5)],
    });
    this.company = new Company();
  }

  get form(): { [key: string]: AbstractControl } {
    return this.editForm.controls;
  }

  public save(): void {
    this.company = Object.assign(this.company, this.editForm.value);
    this.service.insertAsync(this.company).subscribe({
      next: () => {
        this.router.navigate(['companies']);
      },
      error: (e: HttpErrorResponse) => {
        console.log(e);
        this.errorMessage = e.error.detail;
      },
    });
  }
}
