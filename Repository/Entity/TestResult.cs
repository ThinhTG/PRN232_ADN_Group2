using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entity
{
    public class TestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime ResultDate { get; set; }
        public string Description { get; set; }
        public Appointment Appointment { get; set; }
    }
} 