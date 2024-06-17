using BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule;
using FluentValidation.TestHelper;

namespace BookingSystem.Application.UnitTests.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class GetWeeklyScheduleQueryValidatorTests
    {
        private const string ValidWeekStartDate = "2024-06-17";
        private readonly GetWeeklyScheduleQueryValidator _sut;
        public GetWeeklyScheduleQueryValidatorTests()
        {
            _sut = new GetWeeklyScheduleQueryValidator();
        }

        [Fact]
        public void When_Validate_Query_And_Valid_Date_Then_Return_Successful_Validation()
        {
            // Arrange
            var getWeeklyScheduleQuery = new GetWeeklyScheduleQuery
            {
                Date = ValidWeekStartDate
            };

            // Act
            var result = _sut.TestValidate(getWeeklyScheduleQuery);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("2024-06")]
        [InlineData("Test")]
        public void When_Validate_Query_And_Wrong_Date_Then_Return_Failed_Validation(
            string date)
        {
            // Arrange
            var getWeeklyScheduleQuery = new GetWeeklyScheduleQuery
            {
                Date = date
            };

            // Act
            var result = _sut.TestValidate(getWeeklyScheduleQuery);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Date);
        }

        [Theory]        
        [InlineData("2024-06-11")]
        [InlineData("2024-06-09")]
        public void When_Validate_Query_And_Date_Is_Not_Monday_Then_Return_Failed_Validation(
            string date)
        {
            // Arrange
            var getWeeklyScheduleQuery = new GetWeeklyScheduleQuery
            {
                Date = date
            };

            // Act
            var result = _sut.TestValidate(getWeeklyScheduleQuery);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Date)
                  .WithErrorMessage("Date must be a Monday.")
                  .Only();
        }
    }
}
