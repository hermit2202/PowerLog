namespace PowerLog.Core.DTOs.PersonalRecord
{
    public class PersonalRecordDto
    {
        public Guid PersonalRecordId { get; set; }

        public Guid UserId { get; set; }

        public Guid ExerciseId { get; set; }

        public decimal Weight { get; set; }

        public int Reps { get; set; }

        public int? Rpe { get; set; }

        public DateTime RecordDate { get; set; }
    }
}
