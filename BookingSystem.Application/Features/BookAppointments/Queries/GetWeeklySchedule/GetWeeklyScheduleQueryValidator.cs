using FluentValidation;

namespace BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class GetWeeklyScheduleQueryValidator : AbstractValidator<GetWeeklyScheduleQuery>
    {
        public GetWeeklyScheduleQueryValidator()
        {
            RuleFor(query => query.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(BeValidDate).WithMessage("{PropertyName} must be a valid date. Please use valid format: YYYY-MM-DD.")
                .DependentRules(() => {
                    RuleFor(query => query.Date)
                        .Must(BeMonday).WithMessage("{PropertyName} must be a Monday.");                        
                });               
        }

        private bool BeValidDate(string date)
        {
            var success = DateOnly.TryParse(date, out _);

            return success;
        }

        private bool BeMonday(string date)
        {
            var dateOnly = DateOnly.Parse(date);
                        
            var isMonday = dateOnly.DayOfWeek == DayOfWeek.Monday;

            return isMonday;
        }
    }
}