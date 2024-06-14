﻿namespace BookingSystem.Domain.Appointment
{
    public class Appointment
    {
        public Guid FacilityId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Comments { get; set; }
        public Patient Patient { get; set; }
    }
}
