using System;
using System.Collections.Generic;
using System.Text;


using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;

namespace HospitalAppointmentSystem.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _repository;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public AppointmentService(
            AppointmentRepository repository,
            PatientService patientService,
            DoctorService doctorService)
        {
            _repository = repository;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        // Book an appointment
        public string BookAppointment(Appointment appointment)
        {
            if (string.IsNullOrWhiteSpace(appointment.AppointmentId))
                return "Appointment ID cannot be empty.";

            if (_repository.GetAppointmentById(
                appointment.AppointmentId) != null)
                return "Appointment ID already exists.";

            if (_patientService.GetPatientById(
                appointment.PatientId) == null)
                return "Patient does not exist.";

            if (_doctorService.GetDoctorById(
                appointment.DoctorId) == null)
                return "Doctor does not exist.";

            if (appointment.AppointmentDateTime <= DateTime.Now)
                return "Appointment date and time must be in the future.";

            if (string.IsNullOrWhiteSpace(appointment.Reason))
                return "Appointment reason cannot be empty.";

            if (HasDoctorConflict(
                appointment.DoctorId,
                appointment.AppointmentDateTime))
                return "Doctor already has a scheduled appointment at this time.";

            appointment.Status = AppointmentStatus.Scheduled;

            bool added = _repository.AddAppointment(appointment);

            return added
                ? "Appointment booked successfully."
                : "Unable to book appointment.";
        }

        // Get all appointments
        public List<Appointment> GetAllAppointments()
        {
            return _repository.GetAllAppointments();
        }

        // Get appointment by ID
        public Appointment? GetAppointmentById(string appointmentId)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
                return null;

            return _repository.GetAppointmentById(appointmentId.Trim());
        }

        // Reschedule an appointment
        public string RescheduleAppointment(
            string appointmentId,
            DateTime newDateTime)
        {
            Appointment? appointment =
                GetAppointmentById(appointmentId);

            if (appointment == null)
                return "Appointment not found.";

            if (appointment.Status != AppointmentStatus.Scheduled)
                return "Only scheduled appointments can be rescheduled.";

            if (newDateTime <= DateTime.Now)
                return "New appointment date and time must be in the future.";

            if (HasDoctorConflict(
                appointment.DoctorId,
                newDateTime,
                appointment.AppointmentId))
                return "Doctor already has a scheduled appointment at this time.";

            appointment.AppointmentDateTime = newDateTime;

            return "Appointment rescheduled successfully.";
        }

        // Cancel an appointment
        public string CancelAppointment(string appointmentId)
        {
            Appointment? appointment =
                GetAppointmentById(appointmentId);

            if (appointment == null)
                return "Appointment not found.";

            if (appointment.Status != AppointmentStatus.Scheduled)
                return "Only scheduled appointments can be cancelled.";

            appointment.Status = AppointmentStatus.Cancelled;

            return "Appointment cancelled successfully.";
        }

        // Complete an appointment
        public string CompleteAppointment(string appointmentId)
        {
            Appointment? appointment =
                GetAppointmentById(appointmentId);

            if (appointment == null)
                return "Appointment not found.";

            if (appointment.Status != AppointmentStatus.Scheduled)
                return "Only scheduled appointments can be completed.";

            appointment.Status = AppointmentStatus.Completed;

            return "Appointment marked as completed.";
        }

        // Check whether a doctor has an appointment at the same time
        private bool HasDoctorConflict(
            string doctorId,
            DateTime dateTime,
            string? excludedAppointmentId = null)
        {
            return _repository.GetAllAppointments().Any(a =>
                a.DoctorId.Equals(
                    doctorId,
                    StringComparison.OrdinalIgnoreCase)
                &&
                a.AppointmentDateTime == dateTime
                &&
                a.Status == AppointmentStatus.Scheduled
                &&
                !a.AppointmentId.Equals(
                    excludedAppointmentId ?? "",
                    StringComparison.OrdinalIgnoreCase));
        }

        // Used by PatientService before deleting a patient
        public bool HasAppointmentsForPatient(string patientId)
        {
            return _repository.HasAppointmentsForPatient(patientId);
        }

        // Used by DoctorService before deleting a doctor
        public bool HasAppointmentsForDoctor(string doctorId)
        {
            return _repository.HasAppointmentsForDoctor(doctorId);
        }
    }
}

