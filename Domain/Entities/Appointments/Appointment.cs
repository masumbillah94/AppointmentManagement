using Domain.Entities.Base;
using Domain.Entities.Doctors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Appointments
{
    public class Appointment : BaseEntity<long>
    {
        [MaxLength(50)]
        public string PatientName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string PatientEmail { get; set; } = string.Empty;
        [MaxLength(30)]
        public string PatientPhone { get; set; } = string.Empty;
        [MaxLength(30)]
        public string PatientGender { get; set; } = string.Empty;
        [ForeignKey(nameof(Doctor))]
        public long DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
