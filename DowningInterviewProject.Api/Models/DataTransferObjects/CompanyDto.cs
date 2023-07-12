// <copyright file="CompanyDto.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Models.DataTransferObjects
{
    /// <summary>
    /// The data transfer object for companies.
    /// </summary>
    public class CompanyDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the share price.
        /// </summary>
        public decimal? SharePrice { get; set; }
    }
}
