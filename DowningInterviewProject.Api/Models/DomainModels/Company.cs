// <copyright file="Company.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The domain entity for companies.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string? CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [Required]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the share price.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? SharePrice { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="ICollection{T}"/> of <see cref="Investor"/>.
        /// </summary>
        [InverseProperty("Company")]
        public virtual ICollection<Investor> Investors { get; set; } = new List<Investor>();
    }
}