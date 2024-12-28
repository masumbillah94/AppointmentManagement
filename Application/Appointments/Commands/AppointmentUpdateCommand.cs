using Application.Common.Models;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Doctors;
using Domain.Dto.Appointments;

namespace Application.Appointments.Commands
{
    public class AppointmentUpdateCommand : IRequest<ResponseDetail<AppointmentReadDto>>
    {
        #region Public Properties

        public long Id { get; set; }
        [MaxLength(50)]
        public string PatientName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string PatientEmail { get; set; } = string.Empty;
        [MaxLength(30)]
        public string PatientPhone { get; set; } = string.Empty;
        [MaxLength(30)]
        public string PatientGender { get; set; } = string.Empty;
        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
    }

    #endregion Public Properties

}
