// <copyright file="CompanyInfo.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Models.InfoModels
{
    /// <summary>
    /// The info class for companies.
    /// </summary>
    public class CompanyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyInfo"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="currentSharePrice">The current share price.</param>
        /// <param name="createdDate">The created date.</param>
        public CompanyInfo(int id, string name, string code, decimal? currentSharePrice, DateTime createdDate)
        {
            Id = id;
            Name = name;
            Code = code;
            CurrentSharePrice = currentSharePrice;
            CreatedDate = createdDate;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the current share price.
        /// </summary>
        public decimal? CurrentSharePrice { get; init; }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        public DateTime CreatedDate { get; init; }
    }
}
