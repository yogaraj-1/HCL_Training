using CampusPlacementManagement.Api.Enums;

namespace CampusPlacementManagement.Api.Models
{
    public class InterviewSchedule
    {
        public int InterviewScheduleId { get; set; }

        public int ApplicationId { get; set; }

        public int InterviewRoundId { get; set; }

        public DateTime ScheduledAt { get; set; }

        public string? MeetingLink { get; set; }

        public string? Venue { get; set; }

        public InterviewStatus Status { get; set; }

        public string? Feedback { get; set; }

        public JobApplication Application { get; set; } = null!;

        public InterviewRound InterviewRound { get; set; } = null!;
    }
}