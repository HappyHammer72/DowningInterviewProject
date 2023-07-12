// <copyright file="CompanyController.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Controllers
{
    using DowningInterviewProject.Api.Models;
    using DowningInterviewProject.Api.Models.DataTransferObjects;
    using DowningInterviewProject.Api.Repositories;
    using DowningInterviewProject.Api.Repositories.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The controller to manage companies.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly ILogger<CompanyRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="companyRepository">The <see cref="ICompanyRepository"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        public CompanyController(ICompanyRepository companyRepository, ILogger<CompanyRepository> logger)
        {
            this.companyRepository = companyRepository;
            this.logger = logger;
        }

        /// <summary>
        /// The method to return all the companies from the database.
        /// </summary>
        /// <returns>A list of companies.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return this.Ok(await this.companyRepository.GetAllCompaniesAsync());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                return this.Problem();
            }
        }

        /// <summary>
        /// The method to add a company to the database.
        /// </summary>
        /// <param name="companyDto">The company to be added.</param>
        /// <returns>A list of companies.</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] CompanyDto companyDto)
        {
            try
            {
                var result = await this.companyRepository.InsertCompanyAsync(companyDto);
                return this.Created($"/api/company/{result.Id}", result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                return this.Problem(ex.Message);
            }
        }
    }
}
