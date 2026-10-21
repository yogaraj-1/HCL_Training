
using System;

namespace TrainingManagementSystem.Models
{
    public class InvalidLearnerIdException : Exception
    {
        public InvalidLearnerIdException(string message)
            : base(message) { }
    }
}
