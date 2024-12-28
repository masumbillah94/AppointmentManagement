using Domain.Entities.Appointments;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Doctors
{
    public class Doctor : BaseEntity<long>
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public virtual List<Appointment> Appointments { get; set; } = null!;
    }
}
