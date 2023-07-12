// Copyright (c) TV Rentals. All rights reserved.

namespace DowningInterviewProject.Respository.UnitTests.RepositoryTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DowningInterviewProject.Api.Exceptions;
    using DowningInterviewProject.Api.Models.DataTransferObjects;
    using DowningInterviewProject.Api.Models.DomainModels;
    using DowningInterviewProject.Api.Models.InfoModels;
    using DowningInterviewProject.Api.Repositories;
    using DowningInterviewProject.Api.Repositories.Interfaces;
    using DowningInterviewProject.Respository.UnitTests.Helpers;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Xunit;

    /// <summary>
    /// The unit tests for Companies.
    /// </summary>
    public class CompaniesRepositoryTests
    {
        private readonly DowningInvestmentDatabaseContext context;
        private readonly ICompanyRepository repository;
        private List<Company>? entities;
        private List<CompanyInfo>? infoEntities;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompaniesRepositoryTests"/> class.
        /// </summary>
        public CompaniesRepositoryTests()
        {
            // Create a new instance of the test database for use in all tests.
            var options = new DbContextOptionsBuilder<DowningInvestmentDatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            context = new DowningInvestmentDatabaseContext(options);
            repository = new CompanyRepository(context);
            SetupData();
        }

        /// <summary>
        /// Test to call the get all method with the show deleted items parameter set to false.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetAllTest()
        {
            // Arrange

            // Act
            var actual = await repository.GetAllCompaniesAsync();

            // Assert
            Assert.Equal(3, actual.Count);
            for (var i = 0; i < 3; i++)
            {
                var infoEntity = infoEntities!.FirstOrDefault(x => x.Id == actual[i].Id);
                Assert.NotNull(infoEntity);
                UnitTestingHelper.AssertPublicPropertiesEqual(infoEntity, actual[i]);
            }
        }

        /// <summary>
        /// Test to call the insert method passing in a valid new entity.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task InsertTest()
        {
            // Arrange
            var newEntity = new CompanyDto
            {
                Name = "CompanyName",
                CreatedDate = new DateTime(2023, 7, 11, 20, 19, 5),
                Code = "CODE4",
                SharePrice = 4,
            };

            // Act
            var expected = await repository.InsertCompanyAsync(newEntity);

            // Assert
            newEntity.Id = 4;
            UnitTestingHelper.AssertPublicPropertiesEqual(newEntity, expected);
        }

        /// <summary>
        /// Test to call the insert method passing in a new entity with the same code as an existing one.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task InvalidInsertEntityExistsTest()
        {
            // Arrange
            var newEntity = new CompanyDto();

            // Act            

            // Assert            
            await Assert.ThrowsAsync<EntityValidationException>(async () => await repository.InsertCompanyAsync(newEntity));
        }

        /// <summary>
        /// Test to call the insert method passing in an invalid new entity.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task InvalidInsertTest()
        {
            // Arrange
            var newEntity = new CompanyDto();

            // Act            

            // Assert            
            await Assert.ThrowsAsync<EntityValidationException>(async () => await repository.InsertCompanyAsync(newEntity));
        }

        private List<CompanyInfo> ConvertToInfoEntity()
        {
            return entities!.Select(c => new CompanyInfo(c.Id,
                                                        c.CompanyName!,
                                                        c.Code!,
                                                        c.SharePrice!.Value,
                                                        c.CreatedDate!.Value)).ToList();
        }

        private void SetupData()
        {
            var entity1 = new Company
            {
                Id = 1,
                CompanyName = "CompanyName",
                CreatedDate = new DateTime(2023, 7, 11, 19, 58, 37),
                Code = "CODE1",
                SharePrice = 1,
            };

            var entity2 = new Company
            {
                Id = 2,
                CompanyName = "CompanyName",
                CreatedDate = new DateTime(2023, 7, 11, 19, 58, 37),
                Code = "CODE2",
                SharePrice = 1,
            };

            var entity3 = new Company
            {
                Id = 3,
                CompanyName = "CompanyName",
                CreatedDate = new DateTime(2023, 7, 11, 19, 58, 37),
                Code = "CODE3",
                SharePrice = 1,
            };

            context.AddRange(entity1, entity2, entity3);
            context.SaveChanges();
            entities = new List<Company> { entity1, entity2, entity3 };
            infoEntities = ConvertToInfoEntity();
        }
    }
}
