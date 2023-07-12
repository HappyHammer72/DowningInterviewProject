// <copyright file="ICompanyRepository.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Repositories.Interfaces
{
    using DowningInterviewProject.Api.Models.DataTransferObjects;
    using DowningInterviewProject.Api.Models.InfoModels;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The interface for the company repository.
    /// </summary>
    public interface ICompanyRepository : IDisposable
    {
        /// <summary>
        /// Gets all companies from the database.
        /// </summary>
        /// <returns>A list of <see cref="CompanyInfo"/>.</returns>
        Task<List<CompanyInfo>> GetAllCompaniesAsync();

        /// <summary>
        /// Inserts a company into the database.
        /// </summary>
        /// <param name="companyDto">The <see cref="CompanyDto"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<CompanyDto> InsertCompanyAsync([FromBody] CompanyDto companyDto);
    }
}
