
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;

namespace HospitalAppointmentSystem.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _repository;

        public DoctorService(DoctorRepository repository)
        {
            _repository = repository;
        }

        // Add doctor
        public string AddDoctor(Doctor doctor)
        {
            string error = ValidateDoctor(doctor);

            if (error != "")
            {
                return error;
            }

            bool added = _repository.AddDoctor(doctor);

            return added
                ? "Doctor added successfully."
                : "Doctor ID already exists.";
        }

        // Get all doctors
        public List<Doctor> GetAllDoctors()
        {
            return _repository.GetAllDoctors();
        }

        // Get doctor by ID
        public Doctor? GetDoctorById(string doctorId)
        {
            if (string.IsNullOrWhiteSpace(doctorId))
            {
                return null;
            }

            return _repository.GetDoctorById(doctorId.Trim());
        }

        // Search by name or specialization
        public List<Doctor> SearchDoctors(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Doctor>();
            }

            return _repository.SearchDoctors(searchTerm.Trim());
        }

        // Update doctor
        public string UpdateDoctor(
            string doctorId,
            string fullName,
            string specialization,
            decimal consultationFee)
        {
            Doctor? existingDoctor = GetDoctorById(doctorId);

            if (existingDoctor == null)
            {
                return "Doctor not found.";
            }

            Doctor updatedDoctor = new Doctor(
                existingDoctor.DoctorId,
                fullName.Trim(),
                specialization.Trim(),
                consultationFee);

            string error = ValidateDoctor(updatedDoctor);

            if (error != "")
            {
                return error;
            }

            _repository.UpdateDoctor(
                existingDoctor.DoctorId,
                updatedDoctor);

            return "Doctor updated successfully.";
        }

        // Delete doctor, provided no appointments reference them
        public string DeleteDoctor(
            string doctorId,
            Func<string, bool> hasAppointmentReferences)
        {
            Doctor? doctor = GetDoctorById(doctorId);

            if (doctor == null)
            {
                return "Doctor not found.";
            }

            if (hasAppointmentReferences(doctor.DoctorId))
            {
                return "Cannot delete doctor because appointments reference this doctor.";
            }

            _repository.DeleteDoctor(doctor.DoctorId);

            return "Doctor deleted successfully.";
        }

        // Validate doctor details
        private string ValidateDoctor(Doctor doctor)
        {
            if (string.IsNullOrWhiteSpace(doctor.DoctorId))
            {
                return "Doctor ID cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(doctor.FullName))
            {
                return "Doctor name cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(doctor.Specialization))
            {
                return "Specialization cannot be empty.";
            }

            if (doctor.ConsultationFee < 0)
            {
                return "Consultation fee cannot be negative.";
            }

            return "";
        }
    }
}
