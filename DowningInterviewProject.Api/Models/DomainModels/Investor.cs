// <copyright file="Investor.cs" company="Downing Interview Project">
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
    /// The domain entity for investors.
    /// </summary>
    public partial class Investor
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the investor's title.
        /// </summary>
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the investor's first name.
        /// </summary>
        [StringLength(100)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the investor's last name.
        /// </summary>
        [StringLength(100)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Company"/>.
        /// </summary>
        [ForeignKey("CompanyId")]
        [InverseProperty("Investors")]
        public virtual Company Company { get; set; } = null!;
    }
}