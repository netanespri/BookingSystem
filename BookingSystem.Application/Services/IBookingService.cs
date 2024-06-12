using BookingSystem.Domain.Schedule;

namespace BookingSystem.Application.Services
{
    public interface IBookingService
    {
        Task<WeeklySchedule> GetWeeklySchedule(DateOnly date);
    }
}
