using FluentValidation;

namespace BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment
{
    public class BookAppointmentCommandValidator : AbstractValidator<BookAppointmentCommand>
    {
        public BookAppointmentCommandValidator()
        {
            RuleFor(command => command.FacilityId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(command => command.Start)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(command => command.End)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(command => command.Patient)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .DependentRules(() => {
                    RuleFor(command => command.Patient.Name)
                        .NotEmpty().WithMessage("{PropertyName} is required.");
                    RuleFor(command => command.Patient.SecondName)
                        .NotEmpty().WithMessage("{PropertyName} is required.");
                    RuleFor(command => command.Patient.Email)
                        .NotEmpty().WithMessage("{PropertyName} is required.");
                    RuleFor(command => command.Patient.Phone)
                        .NotEmpty().WithMessage("{PropertyName} is required.");
                });
        }
    }
}