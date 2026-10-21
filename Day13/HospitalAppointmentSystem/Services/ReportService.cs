using System;
using System.Collections.Generic;
using System.Text;


using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Services
{
    public class ReportService
    {
        private readonly AppointmentService _appointmentService;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public ReportService(
            AppointmentService appointmentService,
            PatientService patientService,
            DoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        // 1. Filter appointments by status
        public List<Appointment> GetAppointmentsByStatus(
            AppointmentStatus status)
        {
            return _appointmentService.GetAllAppointments()
                .Where(a => a.Status == status)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();
        }

        // 2. Filter appointments by date
        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return _appointmentService.GetAllAppointments()
                .Where(a => a.AppointmentDateTime.Date == date.Date)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();
        }

        // 3. Get appointments for a selected patient
        public List<Appointment> GetAppointmentsByPatient(string patientId)
        {
            return _appointmentService.GetAllAppointments()
                .Where(a => a.PatientId.Equals(
                    patientId,
                    StringComparison.OrdinalIgnoreCase))
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();
        }

        // 4. Get appointments for a selected doctor
        public List<Appointment> GetAppointmentsByDoctor(string doctorId)
        {
            return _appointmentService.GetAllAppointments()
                .Where(a => a.DoctorId.Equals(
                    doctorId,
                    StringComparison.OrdinalIgnoreCase))
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();
        }

        // 5. Doctor-wise scheduled appointment counts
        public Dictionary<string, int> GetScheduledCountByDoctor()
        {
            return _appointmentService.GetAllAppointments()
                .Where(a => a.Status == AppointmentStatus.Scheduled)
                .GroupBy(a => a.DoctorId)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // 6. Doctor-wise completed appointment counts
        public Dictionary<string, int> GetCompletedCountByDoctor()
        {
            return _appointmentService.GetAllAppointments()
                .Where(a => a.Status == AppointmentStatus.Completed)
                .GroupBy(a => a.DoctorId)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // 7. Calculate completed consultation revenue
        public decimal GetCompletedConsultationRevenue()
        {
            List<Appointment> completedAppointments =
                _appointmentService.GetAllAppointments()
                    .Where(a => a.Status == AppointmentStatus.Completed)
                    .ToList();

            return completedAppointments.Sum(a =>
                _doctorService.GetDoctorById(a.DoctorId)?.ConsultationFee ?? 0m);
        }

        // 8. Doctor(s) with the most completed appointments
        public List<string> GetDoctorsWithMostCompletedAppointments()
        {
            var counts = _appointmentService.GetAllAppointments()
                .Where(a => a.Status == AppointmentStatus.Completed)
                .GroupBy(a => a.DoctorId)
                .Select(g => new
                {
                    DoctorId = g.Key,
                    Count = g.Count()
                })
                .ToList();

            if (counts.Count == 0)
                return new List<string>();

            int maximum = counts.Max(x => x.Count);

            return counts
                .Where(x => x.Count == maximum)
                .Select(x => x.DoctorId)
                .ToList();
        }
    }
}

