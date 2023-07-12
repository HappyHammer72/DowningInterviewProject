// <copyright file="Program.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api
{
    using System;

    using DowningInterviewProject.Api.Models.DomainModels;
    using DowningInterviewProject.Api.Repositories;
    using DowningInterviewProject.Api.Repositories.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that is run when the application starts.
        /// </summary>
        /// <param name="args">Any arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DowningInvestmentDatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}