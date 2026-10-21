
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Repositories
{
    public class PatientRepository
    {
        private readonly List<Patient> _patients = new List<Patient>();

        // Register a patient
        public bool AddPatient(Patient patient)
        {
            bool exists = _patients.Any(p =>
                p.PatientId.Equals(
                    patient.PatientId,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return false;
            }

            _patients.Add(patient);
            return true;
        }

        public void LoadPatients(List<Patient> patients)
        {
            _patients.Clear();
            _patients.AddRange(patients);
        }

        // Display all patients
        public List<Patient> GetAllPatients()
        {
            return _patients.ToList();
        }

        // Retrieve a patient by ID
        public Patient? GetPatientById(string patientId)
        {
            return _patients.FirstOrDefault(p =>
                p.PatientId.Equals(
                    patientId,
                    StringComparison.OrdinalIgnoreCase));
        }

        // Search patients by full or partial name
        public List<Patient> SearchPatientsByName(string name)
        {
            return _patients
                .Where(p => p.FullName.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Update an existing patient
        public bool UpdatePatient(string patientId, Patient updatedPatient)
        {
            Patient? existingPatient = GetPatientById(patientId);

            if (existingPatient == null)
            {
                return false;
            }

            existingPatient.FullName = updatedPatient.FullName;
            existingPatient.Age = updatedPatient.Age;
            existingPatient.PhoneNumber = updatedPatient.PhoneNumber;
            existingPatient.Email = updatedPatient.Email;

            return true;
        }

        // Delete a patient
        public bool DeletePatient(string patientId)
        {
            Patient? patient = GetPatientById(patientId);

            if (patient == null)
            {
                return false;
            }

            _patients.Remove(patient);
            return true;
        }
    }
}
