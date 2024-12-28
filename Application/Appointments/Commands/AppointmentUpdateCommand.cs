using Application.Common.Models;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Doctors;
using Domain.Dto.Appointments;
using Application.Appointments.Validators;

namespace Application.Appointments.Commands
{
    public class AppointmentUpdateCommand : IRequest<ResponseDetail<AppointmentReadDto>>
    {
        #region Public Properties
        [Required]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string PatientName { get; set; } = string.Empty;
        [EmailAddress]
        [MaxLength(50)]
        public string PatientEmail { get; set; } = string.Empty;
        [MaxLength(30)]
        public string PatientPhone { get; set; } = string.Empty;
        [MaxLength(30)]
        public string PatientGender { get; set; } = string.Empty;

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "The DoctorId must be a positive number greater than 0.")]
        public long DoctorId { get; set; }
        [FutureDate, Required]
        public DateTime? AppointmentDate { get; set; }
    }

    #endregion Public Properties

}
