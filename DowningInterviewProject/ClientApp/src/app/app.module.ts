import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CompanyListComponent } from './company/company-list/company-list.component';
import { UppercaseDirective } from './shared/directives/uppercase-directive';
import { AddCompanyComponent } from './company/add-company/add-company.component';
import { NumbersOnlyDirective } from './shared/directives/numbers-only.directive';

const routes: Routes = [
  { path: 'companies', component: CompanyListComponent },
  { path: 'add-company', component: AddCompanyComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CompanyListComponent,
    AddCompanyComponent,
    NumbersOnlyDirective,
    UppercaseDirective,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
