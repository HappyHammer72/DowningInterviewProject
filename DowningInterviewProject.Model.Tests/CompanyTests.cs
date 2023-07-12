// Copyright (c) TV Rentals. All rights reserved.

namespace DowningInterviewProject.Model.Tests
{
    using DowningInterviewProject.Api.Models.DomainModels;
    using DowningInterviewProject.Model.Tests.Helpers;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Xunit;

    /// <summary>
    /// The unit tests for Companies.
    /// </summary>
    public class CompanyTests
    {
        /// <summary>
        /// Valid test.
        /// </summary>
        [Fact]
        public void CompanyValid()
        {
            // Arrange
            var results = new List<ValidationResult>();
            var entity = GetValidCompany();

            // Act
            var context = new ValidationContext(entity);
            var isValid = Validator.TryValidateObject(entity, context, results, true);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Negative Test for an string that is too long on column CompanyName.
        /// </summary>
        [Fact]
        public void CompanyCompanyNameTooLong()
        {
            // Arrange
            var expected = "The field CompanyName must be a string with a maximum length of 100.";
            var results = new List<ValidationResult>();
            var entity = GetValidCompany();
            entity.CompanyName = StringHelpers.GetSpecificLengthString(100 + 1);

            // Act
            var context = new ValidationContext(entity);
            var isValid = Validator.TryValidateObject(entity, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Equal(expected, results[0].ErrorMessage);
        }

        /// <summary>
        /// Negative Test for an string that is too long on column Code.
        /// </summary>
        [Fact]
        public void CompanyCodeTooLong()
        {
            // Arrange
            var expected = "The field Code must be a string with a maximum length of 100.";
            var results = new List<ValidationResult>();
            var entity = GetValidCompany();
            entity.Code = StringHelpers.GetSpecificLengthString(100 + 1);

            // Act
            var context = new ValidationContext(entity);
            var isValid = Validator.TryValidateObject(entity, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Equal(expected, results[0].ErrorMessage);
        }

        /// <summary>
        /// Negative Test for an null value on column SharePrice.
        /// </summary>
        [Fact]
        public void CompanySharePriceIsNull()
        {
            // Arrange
            var results = new List<ValidationResult>();

            // Act
            var entity = GetValidCompany();
            entity.SharePrice = null;

            var context = new ValidationContext(entity);
            var isValid = Validator.TryValidateObject(entity, context, results, true);

            // Assert
            Assert.True(isValid);
        }

        private Company GetValidCompany()
        {
            return new Company
            {
                Id = 1,
                CompanyName = "CompanyName",
                CreatedDate = new DateTime(2023, 7, 11, 19, 58, 37),
                Code = "Code",
                SharePrice = 1,
            };
        }
    }
}
