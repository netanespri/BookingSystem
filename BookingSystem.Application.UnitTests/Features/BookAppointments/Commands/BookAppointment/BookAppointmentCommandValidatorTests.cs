using AutoFixture.Xunit2;
using BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment;
using FluentValidation.TestHelper;

namespace BookingSystem.Application.UnitTests.Features.BookAppointments.Commands.BookAppointment
{
    public class BookAppointmentCommandValidatorTests
    {
        private readonly BookAppointmentCommandValidator _sut;
        public BookAppointmentCommandValidatorTests()
        {
            _sut = new BookAppointmentCommandValidator();
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Valid_Command_Then_Return_Successful_Validation(
            BookAppointmentCommand bookAppointmentCommand)
        {
            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        public void When_Validate_Command_And_Empty_Comments_Then_Return_Successful_Validation(
            string comments, BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Comments = comments;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Empty_FacilityId_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.FacilityId = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.FacilityId);
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Empty_Start_Date_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Start = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Start);
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Empty_End_Date_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.End = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.End);
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Null_Patient_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Patient = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Patient);
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Empty_Patient_Name_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Patient.Name = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Patient.Name);
        }

        [Theory]
        [AutoData]
        public void When_Validate_Command_And_Empty_Patient_Second_Name_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Patient.SecondName = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Patient.SecondName);
        }

        [AutoData]
        public void When_Validate_Command_And_Empty_Patient_Email_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Patient.Email = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Patient.Email);
        }

        [AutoData]
        public void When_Validate_Command_And_Empty_Patient_Phone_Then_Return_Failed_Validation(
           BookAppointmentCommand bookAppointmentCommand)
        {
            // Arrange
            bookAppointmentCommand.Patient.Phone = default;

            // Act
            var result = _sut.TestValidate(bookAppointmentCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Patient.Phone);
        }
    }
}
