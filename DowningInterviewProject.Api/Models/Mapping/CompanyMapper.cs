// <copyright file="CompanyMapper.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Models.Mapping
{
    using DowningInterviewProject.Api.Models.DataTransferObjects;
    using DowningInterviewProject.Api.Models.DomainModels;

    /// <summary>
    /// The class to map companies to and from their dto.
    /// </summary>
    public static class CompanyMapper
    {
        /// <summary>
        /// The method to convert a company to a company dto.
        /// </summary>
        /// <param name="company">The <see cref="Company"/>.</param>
        /// <returns>The created <see cref="CompanyDto"/>.</returns>
        public static CompanyDto ConvertTo(this Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Code = company.Code,
                Name = company.CompanyName,
                CreatedDate = company.CreatedDate,
                SharePrice = company.SharePrice,
            };
        }

        /// <summary>
        /// The method to convert a company dto to a company.
        /// </summary>
        /// <param name="companyDto">The <see cref="CompanyDto"/>.</param>
        /// <returns>The created <see cref="Company"/>.</returns>
        public static Company ConvertFrom(this CompanyDto companyDto)
        {
            return new Company
            {
                Id = companyDto.Id,
                Code = companyDto.Code,
                CompanyName = companyDto.Name,
                CreatedDate = companyDto.CreatedDate,
                SharePrice = companyDto.SharePrice,
            };
        }
    }
}
