
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;

namespace HospitalAppointmentSystem.Services
{
    public class PatientService
    {
        private readonly PatientRepository _repository;

        public PatientService(PatientRepository repository)
        {
            _repository = repository;
        }

        // Register patient
        public string RegisterPatient(Patient patient)
        {
            string validationError = ValidatePatient(patient);

            if (validationError != "")
            {
                return validationError;
            }

            bool added = _repository.AddPatient(patient);

            return added
                ? "Patient registered successfully!"
                : "Patient ID already exists.";
        }

        // Display all patients
        public List<Patient> GetAllPatients()
        {
            return _repository.GetAllPatients();
        }

        // Retrieve patient by ID
        public Patient? GetPatientById(string patientId)
        {
            if (string.IsNullOrWhiteSpace(patientId))
            {
                return null;
            }

            return _repository.GetPatientById(patientId.Trim());
        }

        // Search by name
        public List<Patient> SearchPatientsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<Patient>();
            }

            return _repository.SearchPatientsByName(name.Trim());
        }

        // Update patient details
        public string UpdatePatient(
            string patientId,
            string fullName,
            int age,
            string phoneNumber,
            string? email)
        {
            Patient? existingPatient = GetPatientById(patientId);

            if (existingPatient == null)
            {
                return "Patient not found.";
            }

            Patient updatedPatient = new Patient(
                existingPatient.PatientId,
                fullName.Trim(),
                age,
                phoneNumber.Trim(),
                NormalizeEmail(email));

            string error = ValidatePatient(updatedPatient);

            if (error != "")
            {
                return error;
            }

            _repository.UpdatePatient(
                existingPatient.PatientId,
                updatedPatient);

            return "Patient updated successfully.";
        }

        // Delete only when no appointments reference the patient
        public string DeletePatient(
            string patientId,
            Func<string, bool> hasAppointmentReferences)
        {
            Patient? patient = GetPatientById(patientId);

            if (patient == null)
            {
                return "Patient not found.";
            }

            if (hasAppointmentReferences(patient.PatientId))
            {
                return "Cannot delete patient because appointments reference this patient.";
            }

            _repository.DeletePatient(patient.PatientId);

            return "Patient deleted successfully.";
        }

        // Shared validation
        private string ValidatePatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.PatientId))
            {
                return "Patient ID cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(patient.FullName))
            {
                return "Patient name cannot be empty.";
            }

            if (patient.Age < 0 || patient.Age > 120)
            {
                return "Age must be between 0 and 120.";
            }

            if (string.IsNullOrWhiteSpace(patient.PhoneNumber))
            {
                return "Phone number cannot be empty.";
            }

            if (patient.PhoneNumber.Length != 10 ||
                !patient.PhoneNumber.All(char.IsDigit))
            {
                return "Phone number must contain exactly 10 digits.";
            }

            return "";
        }

        private string? NormalizeEmail(string? email)
        {
            return string.IsNullOrWhiteSpace(email)
                ? null
                : email.Trim();
        }
    }
}
