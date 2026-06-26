using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.DTOs.Set
{
    public class SetDto
    {
        public Guid SetId { get; set; }

        public decimal Weight { get; set; }

        public int Reps { get; set; }

        public int? RPE { get; set; }
    }
}
