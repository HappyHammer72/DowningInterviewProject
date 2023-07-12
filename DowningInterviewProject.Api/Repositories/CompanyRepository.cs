// <copyright file="CompanyRepository.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Repositories
{
    using System.ComponentModel.DataAnnotations;

    using DowningInterviewProject.Api.Exceptions;
    using DowningInterviewProject.Api.Models.DataTransferObjects;
    using DowningInterviewProject.Api.Models.DomainModels;
    using DowningInterviewProject.Api.Models.InfoModels;
    using DowningInterviewProject.Api.Models.Mapping;
    using DowningInterviewProject.Api.Repositories.Interfaces;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The repository for performing actions on companies.
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DowningInvestmentDatabaseContext context;
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DowningInvestmentDatabaseContext"/>.</param>
        public CompanyRepository(DowningInvestmentDatabaseContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<List<CompanyInfo>> GetAllCompaniesAsync()
        {
            return await this.context.Companies
                .AsNoTracking()
                .OrderBy(c => c.CompanyName)
                .Select(c => new CompanyInfo(c.Id, c.CompanyName!, c.Code!, c.SharePrice, c.CreatedDate!.Value))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<CompanyDto> InsertCompanyAsync(CompanyDto companyDto)
        {
            var company = companyDto.ConvertFrom();
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(company, new ValidationContext(company, null, null), validationResults, true))
            {
                throw new EntityValidationException();
            }

            if (await this.CheckForExistingCode(company.Code!))
            {
                throw new EntityExistsException($"The company with code {company.Code} already exists.");
            }

            this.context.Companies.Add(company);
            await this.context.SaveChangesAsync();
            return company.ConvertTo();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The method to dispose the context.
        /// </summary>
        /// <param name="disposing">If the context is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposedValue = true;
            }
        }

        private async Task<bool> CheckForExistingCode(string code)
        {
            return await this.context.Companies.FirstOrDefaultAsync(c => c.Code!.ToLower() == code.ToLower()) != null;
        }
    }
}
