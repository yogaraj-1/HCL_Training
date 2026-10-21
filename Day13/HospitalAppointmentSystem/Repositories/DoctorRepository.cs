using System;
using System.Collections.Generic;
using System.Text;


using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Repositories
{
    public class DoctorRepository
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();

        public bool AddDoctor(Doctor doctor)
        {
            bool exists = _doctors.Any(d =>
                d.DoctorId.Equals(
                    doctor.DoctorId,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return false;
            }

            _doctors.Add(doctor);
            return true;
        }

        public void LoadDoctors(List<Doctor> doctors)
        {
            _doctors.Clear();
            _doctors.AddRange(doctors);
        }
        public List<Doctor> GetAllDoctors()
        {
            return _doctors.ToList();
        }

        public Doctor? GetDoctorById(string doctorId)
        {
            return _doctors.FirstOrDefault(d =>
                d.DoctorId.Equals(
                    doctorId,
                    StringComparison.OrdinalIgnoreCase));
        }

        public List<Doctor> SearchDoctors(string searchTerm)
        {
            return _doctors
                .Where(d =>
                    d.FullName.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    d.Specialization.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool UpdateDoctor(string doctorId, Doctor updatedDoctor)
        {
            Doctor? existingDoctor = GetDoctorById(doctorId);

            if (existingDoctor == null)
            {
                return false;
            }

            existingDoctor.FullName = updatedDoctor.FullName;
            existingDoctor.Specialization = updatedDoctor.Specialization;
            existingDoctor.ConsultationFee = updatedDoctor.ConsultationFee;

            return true;
        }

        public bool DeleteDoctor(string doctorId)
        {
            Doctor? doctor = GetDoctorById(doctorId);

            if (doctor == null)
            {
                return false;
            }

            _doctors.Remove(doctor);
            return true;
        }
    }
}
