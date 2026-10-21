using System;
using System.Collections.Generic;
using System.Text;


using System.Text.Json;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;

namespace HospitalAppointmentSystem.Data
{
    public class JsonDataStorage
    {
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly AppointmentRepository _appointmentRepository;

        private readonly string _dataFolder;

        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions
            {
                WriteIndented = true
            };

        public JsonDataStorage(
            PatientRepository patientRepository,
            DoctorRepository doctorRepository,
            AppointmentRepository appointmentRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;

            _dataFolder = Path.Combine(
                AppContext.BaseDirectory,
                "DataFiles");

            Directory.CreateDirectory(_dataFolder);
        }

        public void SaveData()
        {
            string patientsFile = Path.Combine(
                _dataFolder, "patients.json");

            string doctorsFile = Path.Combine(
                _dataFolder, "doctors.json");

            string appointmentsFile = Path.Combine(
                _dataFolder, "appointments.json");

            File.WriteAllText(
                patientsFile,
                JsonSerializer.Serialize(
                    _patientRepository.GetAllPatients(), _options));

            File.WriteAllText(
                doctorsFile,
                JsonSerializer.Serialize(
                    _doctorRepository.GetAllDoctors(), _options));

            File.WriteAllText(
                appointmentsFile,
                JsonSerializer.Serialize(
                    _appointmentRepository.GetAllAppointments(), _options));
        }

        public void LoadData()
        {
            List<Patient> patients = LoadFile<Patient>("patients.json");
            List<Doctor> doctors = LoadFile<Doctor>("doctors.json");
            List<Appointment> appointments =
                LoadFile<Appointment>("appointments.json");

            _patientRepository.LoadPatients(patients);
            _doctorRepository.LoadDoctors(doctors);
            _appointmentRepository.LoadAppointments(appointments);
        }

        private List<T> LoadFile<T>(string fileName)
        {
            string filePath = Path.Combine(_dataFolder, fileName);

            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<T>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<T>>(
                    json, _options) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException(
                    $"The file '{fileName}' contains invalid JSON.", ex);
            }
        }
    }
}

