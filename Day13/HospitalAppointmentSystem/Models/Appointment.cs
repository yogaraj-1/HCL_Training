using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalAppointmentSystem.Models
{
    public  class Appointment
    {
        public string AppointmentId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Reason { get; set; }

        public AppointmentStatus Status { get; set; }

        public Appointment(string appointmentId, string patientId, string doctorId, DateTime appointmentDateTime, string reason, AppointmentStatus status)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDateTime = appointmentDateTime;
            Reason = reason;
            Status = status;
        }
    }
}
