using BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment;
using BookingSystem.Domain.Appointment;

namespace BookingSystem.Application.Mappings
{
    public static class BookAppointmentCommandMapping
    {
        public static Appointment ToAppointment(this BookAppointmentCommand bookAppointmentCommand)
        {
            var appointment = new Appointment
            {
                FacilityId = bookAppointmentCommand.FacilityId,
                Start = bookAppointmentCommand.Start,
                End = bookAppointmentCommand.End,
                Comments = bookAppointmentCommand.Comments,
                Patient = new Domain.Appointment.Patient
                {
                    Name = bookAppointmentCommand.Patient.Name,
                    SecondName = bookAppointmentCommand.Patient.SecondName,
                    Email = bookAppointmentCommand.Patient.Email,
                    Phone = bookAppointmentCommand.Patient.Phone
                }
            };

            return appointment;
        }
    }
}