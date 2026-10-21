using System;
using System.Collections.Generic;
using System.Text;


namespace HospitalAppointmentSystem.Models
{
    public class Patient
    {
        public string PatientId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }

        public Patient(
            string patientId,
            string fullName,
            int age,
            string phoneNumber,
            string? email = null)
        {
            PatientId = patientId;
            FullName = fullName;
            Age = age;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}

