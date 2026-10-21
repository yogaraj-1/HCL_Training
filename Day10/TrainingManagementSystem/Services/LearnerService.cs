
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class LearnerService
    {
        private readonly Dictionary<string, Learner> learners;

        public LearnerService(Dictionary<string, Learner> learners)
        {
            this.learners = learners;
        }

        public bool RegisterLearner(
            string learnerId,
            string name,
            string contactNumber,
            string email)
        {
            // Validate learner ID.
            if (string.IsNullOrWhiteSpace(learnerId))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return false;
            }

            // Validate learner name.
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Learner name cannot be empty.");
                return false;
            }

            // Validate contact number.
            if (!IsValidContactNumber(contactNumber))
            {
                Console.WriteLine(
                    "Invalid contact number. Enter exactly 10 digits.");
                return false;
            }

            // Validate email.
            if (string.IsNullOrWhiteSpace(email) ||
                !email.Contains('@') ||
                !email.Contains('.'))
            {
                Console.WriteLine("Enter a valid email address.");
                return false;
            }

            learnerId = learnerId.Trim();

            // Prevent duplicate learner IDs.
            if (learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner ID already exists.");
                return false;
            }

            Learner learner = new Learner(
                learnerId,
                name.Trim(),
                contactNumber.Trim(),
                email.Trim());

            learners.Add(learnerId, learner);

            Console.WriteLine("Learner registered successfully!");
            return true;
        }

        private bool IsValidContactNumber(string contactNumber)
        {
            if (string.IsNullOrWhiteSpace(contactNumber))
            {
                return false;
            }

            contactNumber = contactNumber.Trim();

            if (contactNumber.Length != 10)
            {
                return false;
            }

            foreach (char digit in contactNumber)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            return true;
        }

        public void DisplayAllLearners()
        {
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered yet.");
                return;
            }

            Console.WriteLine(
                "\n{0,-15} {1,-25} {2,-15} {3,-30}",
                "Learner ID",
                "Name",
                "Contact",
                "Email");

            Console.WriteLine(new string('-', 90));

            foreach (Learner learner in learners.Values)
            {
                learner.DisplayLearner();
            }
        }

        public void SearchLearner(string learnerId)
        {
            if (learners.TryGetValue(learnerId.Trim(), out Learner? learner))
            {
                learner.DisplayLearner();
            }
            else
            {
                throw new InvalidLearnerIdException("Invalid learner ID!");
            }
        }
    }
}
