import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../company.service';
import { Company } from '../company';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: [],
})
export class CompanyListComponent implements OnInit {
  companies: Company[] | undefined;
  errorMessage: string = '';

  constructor(private service: CompanyService) {}

  ngOnInit(): void {
    this.service.getAllAsync().subscribe({
      next: (data: Company[]) => {
        this.companies = data;
      },
      error: (e) => {
        this.errorMessage = e;
      },
    });
  }
}
