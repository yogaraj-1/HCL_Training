
namespace TrainingManagementSystem.Models
{
    public class Learner
    {
        public string LearnerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public Learner(
            string learnerId,
            string name,
            string contactNumber,
            string email)
        {
            LearnerId = learnerId;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
        }

        public void DisplayLearner()
        {
            Console.WriteLine(
                "{0,-15} {1,-25} {2,-15} {3,-30}",
                LearnerId,
                Name,
                ContactNumber,
                Email);
        }
    }
}
