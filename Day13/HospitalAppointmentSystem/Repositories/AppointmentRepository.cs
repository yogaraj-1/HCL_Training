using System;
using System.Collections.Generic;
using System.Text;


using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Repositories
{
    public class AppointmentRepository
    {
        private readonly List<Appointment> _appointments =
            new List<Appointment>();

        // Add a new appointment
        public bool AddAppointment(Appointment appointment)
        {
            bool exists = _appointments.Any(a =>
                a.AppointmentId.Equals(
                    appointment.AppointmentId,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return false;
            }

            _appointments.Add(appointment);
            return true;
        }

        // Get all appointments
        public List<Appointment> GetAllAppointments()
        {
            return _appointments.ToList();
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            _appointments.Clear();
            _appointments.AddRange(appointments);
        }

        // Get appointment by ID
        public Appointment? GetAppointmentById(string appointmentId)
        {
            return _appointments.FirstOrDefault(a =>
                a.AppointmentId.Equals(
                    appointmentId,
                    StringComparison.OrdinalIgnoreCase));
        }

        // Update an appointment
        public bool UpdateAppointment(Appointment updatedAppointment)
        {
            Appointment? existing = GetAppointmentById(
                updatedAppointment.AppointmentId);

            if (existing == null)
            {
                return false;
            }

            existing.PatientId = updatedAppointment.PatientId;
            existing.DoctorId = updatedAppointment.DoctorId;
            existing.AppointmentDateTime =
                updatedAppointment.AppointmentDateTime;
            existing.Reason = updatedAppointment.Reason;
            existing.Status = updatedAppointment.Status;

            return true;
        }

        // Check whether any appointment references a patient
        public bool HasAppointmentsForPatient(string patientId)
        {
            return _appointments.Any(a =>
                a.PatientId.Equals(
                    patientId,
                    StringComparison.OrdinalIgnoreCase));
        }

        // Check whether any appointment references a doctor
        public bool HasAppointmentsForDoctor(string doctorId)
        {
            return _appointments.Any(a =>
                a.DoctorId.Equals(
                    doctorId,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
