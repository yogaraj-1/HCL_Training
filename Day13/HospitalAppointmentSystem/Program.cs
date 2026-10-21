
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Repositories;
using HospitalAppointmentSystem.Services;
using HospitalAppointmentSystem.Data;
namespace HospitalAppointmentSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {      
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService =
                new PatientService(patientRepository);

            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);

            AppointmentRepository appointmentRepository =
    new AppointmentRepository();

            AppointmentService appointmentService = new AppointmentService(
                appointmentRepository,
                patientService,
                doctorService);

            ReportService reportService = new ReportService(
        appointmentService,
        patientService,
        doctorService);

            JsonDataStorage dataStorage = new JsonDataStorage(
    patientRepository,
    doctorRepository,
    appointmentRepository);

            try
            {
                dataStorage.LoadData();
                Console.WriteLine("Data loaded successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Could not load saved data: {ex.Message}");
                return;
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Saved data is invalid: {ex.Message}");
                return;
            }

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n===== HOSPITAL APPOINTMENT MANAGEMENT =====");
                Console.WriteLine("1. Patient Management");
                Console.WriteLine("2. Doctor Management");
                Console.WriteLine("3. Appointment Management");
                Console.WriteLine("4. Reports");
                Console.WriteLine("5. Save Data");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PatientMenu(patientService, appointmentService);
                        break;

                    case "2":
                        DoctorMenu(doctorService, appointmentService);
                        break;

                    case "3":
                        AppointmentMenu(appointmentService);
                        break;

                    case "4":
                        ReportMenu(reportService);
                        break;

                    case "5":
                        try
                        {
                            dataStorage.SaveData();
                            Console.WriteLine("All data saved successfully.");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Could not save data: {ex.Message}");
                        }
                        break;

                    case "0":
                        try
                        {
                            dataStorage.SaveData();
                            Console.WriteLine("Data saved successfully.");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Warning: Data could not be saved: {ex.Message}");
                        }

                        isRunning = false;
                        Console.WriteLine("Thank you for using the application!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void PatientMenu(PatientService patientService,AppointmentService appointmentService)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== PATIENT MANAGEMENT =====");
                Console.WriteLine("1. Register Patient");
                Console.WriteLine("2. Display All Patients");
                Console.WriteLine("3. Search Patient by ID");
                Console.WriteLine("4. Search Patient by Name");
                Console.WriteLine("5. Update Patient");
                Console.WriteLine("6. Delete Patient");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterPatient(patientService);
                        break;

                    case "2":
                        DisplayPatients(patientService);
                        break;

                    case "3":
                        SearchPatientById(patientService);
                        break;

                    case "4":
                        SearchPatientByName(patientService);
                        break;

                    case "5":
                        UpdatePatient(patientService);
                        break;

                    case "6":
                        DeletePatient(patientService,appointmentService);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RegisterPatient(PatientService service)
        {
            Console.WriteLine("\n--- Register Patient ---");

            Console.Write("Patient ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Full Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Age must be a valid number.");
                return;
            }

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine() ?? "";

            Console.Write("Email (optional): ");
            string? email = Console.ReadLine();

            Patient patient = new Patient(
                id.Trim(),
                name.Trim(),
                age,
                phone.Trim(),
                NormalizeEmail(email));

            Console.WriteLine(
                service.RegisterPatient(patient));
        }

        static void DisplayPatients(PatientService service)
        {
            List<Patient> patients = service.GetAllPatients();

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients registered yet.");
                return;
            }

            Console.WriteLine("\n--- All Patients ---");

            foreach (Patient patient in patients)
            {
                PrintPatient(patient);
            }
        }

        static void SearchPatientById(PatientService service)
        {
            Console.Write("Enter Patient ID: ");
            string id = Console.ReadLine() ?? "";

            Patient? patient = service.GetPatientById(id);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            PrintPatient(patient);
        }

        static void SearchPatientByName(PatientService service)
        {
            Console.Write("Enter full or partial name: ");
            string name = Console.ReadLine() ?? "";

            List<Patient> patients =
                service.SearchPatientsByName(name);

            if (patients.Count == 0)
            {
                Console.WriteLine("No matching patients found.");
                return;
            }

            foreach (Patient patient in patients)
            {
                PrintPatient(patient);
            }
        }

        static void UpdatePatient(PatientService service)
        {
            Console.Write("Enter Patient ID to update: ");
            string id = Console.ReadLine() ?? "";

            Patient? existingPatient = service.GetPatientById(id);

            if (existingPatient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            Console.WriteLine("Press Enter to keep the existing value.");

            Console.Write($"Full Name ({existingPatient.FullName}): ");
            string name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                name = existingPatient.FullName;
            }

            Console.Write($"Age ({existingPatient.Age}): ");
            string ageInput = Console.ReadLine() ?? "";
            int age;

            if (string.IsNullOrWhiteSpace(ageInput))
            {
                age = existingPatient.Age;
            }
            else if (!int.TryParse(ageInput, out age))
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            Console.Write(
                $"Phone Number ({existingPatient.PhoneNumber}): ");
            string phone = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = existingPatient.PhoneNumber;
            }

            Console.Write(
                $"Email ({existingPatient.Email ?? "Not provided"}): ");
            string? emailInput = Console.ReadLine();

            string? email = string.IsNullOrWhiteSpace(emailInput)
                ? existingPatient.Email
                : emailInput.Trim();

            Console.WriteLine(service.UpdatePatient(
                id,
                name,
                age,
                phone,
                email));
        }

        static void DeletePatient(PatientService service, AppointmentService appointmentService)
        {
            Console.Write("Enter Patient ID to delete: ");
            string id = Console.ReadLine() ?? "";

            Console.Write(
                "Are you sure you want to delete this patient? (Y/N): ");

            string? confirmation = Console.ReadLine();

            if (!string.Equals(
                    confirmation?.Trim(),
                    "Y",
                    StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }

            // TEMPORARY: Appointment reference checking is not
            // connected until we implement the appointment module.
            Console.WriteLine(service.DeletePatient(
                id,
                patientId => appointmentService.HasAppointmentsForPatient(patientId)));
        }

        static void PrintPatient(Patient patient)
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine($"Patient ID : {patient.PatientId}");
            Console.WriteLine($"Name       : {patient.FullName}");
            Console.WriteLine($"Age        : {patient.Age}");
            Console.WriteLine($"Phone      : {patient.PhoneNumber}");
            Console.WriteLine($"Email      : {patient.Email ?? "Not provided"}");
            Console.WriteLine("------------------------------");
        }

        static string? NormalizeEmail(string? email)
        {
            return string.IsNullOrWhiteSpace(email)
                ? null
                : email.Trim();
        }


        static void DoctorMenu(DoctorService service, AppointmentService appointmentService)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== DOCTOR MANAGEMENT =====");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Display All Doctors");
                Console.WriteLine("3. Search Doctor by ID");
                Console.WriteLine("4. Search by Name or Specialization");
                Console.WriteLine("5. Update Doctor");
                Console.WriteLine("6. Delete Doctor");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDoctor(service);
                        break;

                    case "2":
                        DisplayDoctors(service);
                        break;

                    case "3":
                        SearchDoctorById(service);
                        break;

                    case "4":
                        SearchDoctors(service);
                        break;

                    case "5":
                        UpdateDoctor(service);
                        break;

                    case "6":
                        DeleteDoctor(service ,appointmentService);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }


        static void AddDoctor(DoctorService service)
        {
            Console.Write("Doctor ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Full Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Specialization: ");
            string specialization = Console.ReadLine() ?? "";

            Console.Write("Consultation Fee: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal fee))
            {
                Console.WriteLine("Enter a valid consultation fee.");
                return;
            }

            Doctor doctor = new Doctor(
                id.Trim(),
                name.Trim(),
                specialization.Trim(),
                fee);

            Console.WriteLine(service.AddDoctor(doctor));
        }

        static void DisplayDoctors(DoctorService service)
        {
            List<Doctor> doctors = service.GetAllDoctors();

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors registered yet.");
                return;
            }

            foreach (Doctor doctor in doctors)
            {
                PrintDoctor(doctor);
            }
        }

        static void SearchDoctorById(DoctorService service)
        {
            Console.Write("Enter Doctor ID: ");
            string id = Console.ReadLine() ?? "";

            Doctor? doctor = service.GetDoctorById(id);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            PrintDoctor(doctor);
        }

        static void SearchDoctors(DoctorService service)
        {
            Console.Write("Enter name or specialization: ");
            string term = Console.ReadLine() ?? "";

            List<Doctor> doctors = service.SearchDoctors(term);

            if (doctors.Count == 0)
            {
                Console.WriteLine("No matching doctors found.");
                return;
            }

            foreach (Doctor doctor in doctors)
            {
                PrintDoctor(doctor);
            }
        }

        static void PrintDoctor(Doctor doctor)
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine($"Doctor ID       : {doctor.DoctorId}");
            Console.WriteLine($"Name            : {doctor.FullName}");
            Console.WriteLine($"Specialization  : {doctor.Specialization}");
            Console.WriteLine($"Consultation Fee: {doctor.ConsultationFee:F2}");
            Console.WriteLine("------------------------------");
        }

        static void UpdateDoctor(DoctorService service)
        {
            Console.Write("Enter Doctor ID to update: ");
            string id = Console.ReadLine() ?? "";

            Doctor? existing = service.GetDoctorById(id);

            if (existing == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            Console.Write($"Full Name ({existing.FullName}): ");
            string name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
                name = existing.FullName;

            Console.Write($"Specialization ({existing.Specialization}): ");
            string specialization = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(specialization))
                specialization = existing.Specialization;

            Console.Write($"Consultation Fee ({existing.ConsultationFee}): ");
            string feeInput = Console.ReadLine() ?? "";

            decimal fee;

            if (string.IsNullOrWhiteSpace(feeInput))
            {
                fee = existing.ConsultationFee;
            }
            else if (!decimal.TryParse(feeInput, out fee))
            {
                Console.WriteLine("Invalid consultation fee.");
                return;
            }

            Console.WriteLine(service.UpdateDoctor(
                id, name, specialization, fee));
        }

        static void DeleteDoctor(DoctorService service, AppointmentService appointmentService)
        {
            Console.Write("Enter Doctor ID to delete: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Confirm deletion (Y/N): ");
            string? confirmation = Console.ReadLine();

            if (!string.Equals(
                confirmation?.Trim(),
                "Y",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }

            // Temporary until AppointmentService is implemented.
            Console.WriteLine(service.DeleteDoctor(
                id,
                doctorId => appointmentService.HasAppointmentsForDoctor(doctorId)));
        }

        static void AppointmentMenu(AppointmentService service)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== APPOINTMENT MANAGEMENT =====");
                Console.WriteLine("1. Book Appointment");
                Console.WriteLine("2. Display All Appointments");
                Console.WriteLine("3. Search Appointment by ID");
                Console.WriteLine("4. Reschedule Appointment");
                Console.WriteLine("5. Cancel Appointment");
                Console.WriteLine("6. Complete Appointment");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookAppointment(service);
                        break;

                    case "2":
                        DisplayAppointments(service);
                        break;

                    case "3":
                        SearchAppointment(service);
                        break;

                    case "4":
                        RescheduleAppointment(service);
                        break;

                    case "5":
                        CancelAppointment(service);
                        break;

                    case "6":
                        CompleteAppointment(service);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void BookAppointment(AppointmentService service)
        {
            Console.Write("Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Patient ID: ");
            string patientId = Console.ReadLine() ?? "";

            Console.Write("Doctor ID: ");
            string doctorId = Console.ReadLine() ?? "";

            Console.Write("Appointment date and time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParseExact(
                Console.ReadLine(),
                "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime dateTime))
            {
                Console.WriteLine("Invalid date/time format.");
                return;
            }

            Console.Write("Reason: ");
            string reason = Console.ReadLine() ?? "";

            Appointment appointment = new Appointment(
                id.Trim(),
                patientId.Trim(),
                doctorId.Trim(),
                dateTime,
                reason.Trim(),
                AppointmentStatus.Scheduled);

            Console.WriteLine(service.BookAppointment(appointment));
        }

        static void DisplayAppointments(AppointmentService service)
        {
            List<Appointment> appointments = service.GetAllAppointments();

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            foreach (Appointment appointment in appointments)
            {
                PrintAppointment(appointment);
            }
        }

        static void SearchAppointment(AppointmentService service)
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Appointment? appointment = service.GetAppointmentById(id);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            PrintAppointment(appointment);
        }

        static void RescheduleAppointment(AppointmentService service)
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("New date/time (yyyy-MM-dd HH:mm): ");

            if (!DateTime.TryParseExact(
                Console.ReadLine(),
                "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime newDateTime))
            {
                Console.WriteLine("Invalid date/time format.");
                return;
            }

            Console.WriteLine(
                service.RescheduleAppointment(id, newDateTime));
        }

        static void CancelAppointment(AppointmentService service)
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Console.WriteLine(service.CancelAppointment(id));
        }

        static void CompleteAppointment(AppointmentService service)
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Console.WriteLine(service.CompleteAppointment(id));
        }

        static void PrintAppointment(Appointment appointment)
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine($"Appointment ID : {appointment.AppointmentId}");
            Console.WriteLine($"Patient ID     : {appointment.PatientId}");
            Console.WriteLine($"Doctor ID      : {appointment.DoctorId}");
            Console.WriteLine($"Date and Time  : {appointment.AppointmentDateTime:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Reason         : {appointment.Reason}");
            Console.WriteLine($"Status         : {appointment.Status}");
            Console.WriteLine("------------------------------");
        }


        static void ReportMenu(ReportService service)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== REPORTS =====");
                Console.WriteLine("1. Appointments by Status");
                Console.WriteLine("2. Appointments by Date");
                Console.WriteLine("3. Appointments by Patient");
                Console.WriteLine("4. Appointments by Doctor");
                Console.WriteLine("5. Scheduled Count by Doctor");
                Console.WriteLine("6. Completed Count by Doctor");
                Console.WriteLine("7. Completed Consultation Revenue");
                Console.WriteLine("8. Doctor(s) with Most Completed Appointments");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter status: Scheduled, Completed, or Cancelled");
                        string? statusInput = Console.ReadLine();

                        if (Enum.TryParse(
                            statusInput,
                            true,
                            out AppointmentStatus status)
                            && Enum.IsDefined(typeof(AppointmentStatus), status))
                        {
                            DisplayAppointments(
                                service.GetAppointmentsByStatus(status));
                        }
                        else
                        {
                            Console.WriteLine("Invalid appointment status.");
                        }
                        break;

                    case "2":
                        Console.Write("Enter date (yyyy-MM-dd): ");

                        if (DateTime.TryParseExact(
                            Console.ReadLine(),
                            "yyyy-MM-dd",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime date))
                        {
                            DisplayAppointments(service.GetAppointmentsByDate(date));
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter Patient ID: ");
                        DisplayAppointments(
                            service.GetAppointmentsByPatient(
                                Console.ReadLine() ?? ""));
                        break;

                    case "4":
                        Console.Write("Enter Doctor ID: ");
                        DisplayAppointments(
                            service.GetAppointmentsByDoctor(
                                Console.ReadLine() ?? ""));
                        break;

                    case "5":
                        DisplayDoctorCounts(service.GetScheduledCountByDoctor());
                        break;

                    case "6":
                        DisplayDoctorCounts(service.GetCompletedCountByDoctor());
                        break;

                    case "7":
                        Console.WriteLine(
                            $"Completed consultation revenue: " +
                            $"{service.GetCompletedConsultationRevenue():C}");
                        break;

                    case "8":
                        List<string> topDoctors =
                            service.GetDoctorsWithMostCompletedAppointments();

                        if (topDoctors.Count == 0)
                        {
                            Console.WriteLine("No completed appointments found.");
                        }
                        else
                        {
                            Console.WriteLine("Doctor(s) with the most completed appointments:");

                            foreach (string doctorId in topDoctors)
                                Console.WriteLine(doctorId);
                        }
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void DisplayAppointments(List<Appointment> appointments)
        {
            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            foreach (Appointment appointment in appointments)
                PrintAppointment(appointment);
        }

        static void DisplayDoctorCounts(Dictionary<string, int> counts)
        {
            if (counts.Count == 0)
            {
                Console.WriteLine("No matching appointments found.");
                return;
            }

            foreach (var item in counts)
                Console.WriteLine($"Doctor ID: {item.Key} | Count: {item.Value}");
        }

    }
}
