// <copyright file="DowningInvestmentDatabaseContext.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Models.DomainModels
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The entity framework database context.
    /// </summary>
    public partial class DowningInvestmentDatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DowningInvestmentDatabaseContext"/> class.
        /// </summary>
        public DowningInvestmentDatabaseContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DowningInvestmentDatabaseContext"/> class.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        public DowningInvestmentDatabaseContext(DbContextOptions<DowningInvestmentDatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets a <see cref="DbSet{TEntity}"/> of <see cref="Company"/>.
        /// </summary>
        public virtual DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="DbSet{TEntity}"/> of <see cref="Investor"/>.
        /// </summary>
        public virtual DbSet<Investor> Investors { get; set; }
    }
}