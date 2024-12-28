

namespace Domain.Dto.Appointments
{
    public class AppointmentReadDto
    {
        public long Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public string PatientPhone { get; set; } = string.Empty;
        public string PatientGender { get; set; } = string.Empty;
        public long DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public DateTime? AppointmentDate { get; set; }
    }
}
