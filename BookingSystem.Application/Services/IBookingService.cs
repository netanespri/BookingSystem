using BookingSystem.Application.Responses;
using BookingSystem.Domain.Appointment;
using BookingSystem.Domain.Schedule;

namespace BookingSystem.Application.Services
{
    public interface IBookingService
    {
        Task<Response<WeeklySchedule>> GetWeeklySchedule(DateOnly date);
        Task<Response<bool>> BookAppointment(Appointment appointment);
    }
}
