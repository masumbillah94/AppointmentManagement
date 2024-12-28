using Data.SqlServer.AppContext;
using Domain.Abstractions.Appointments;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using Domain.Dto.Common;
using Domain.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Repository.AppointmentRepositories
{
    public class AppointmentRepository : IAppointmentRepository

    {
        private readonly AppDbContext _context;
        private readonly IUserContext _userContext;

        public AppointmentRepository(AppDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Appointment> AddEntityAsync(Appointment entity, CancellationToken cancellationToken = default)
        {
            entity.IsDelete = false;
            entity.CreateBy = _userContext.UserName;
            entity.CreatedDate = DateTime.Now;
            await _context.Appointments.AddAsync(entity, cancellationToken);
            return entity!;
        }

        public async Task<PaginatedResult<AppointmentReadDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var totalRows = await _context.Appointments.CountAsync(e => !e.IsDelete, cancellationToken);

            var appointments = await _context.Appointments
                .AsNoTracking()
                .OrderBy(a => a.Id) // Ensure consistent ordering
                .Where(e => !e.IsDelete)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AppointmentReadDto
                {
                    Id = a.Id,
                    PatientName = a.PatientName,
                    PatientEmail = a.PatientEmail,
                    PatientPhone = a.PatientPhone,
                    PatientGender = a.PatientGender,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    AppointmentDate = a.AppointmentDate
                })
                .ToListAsync(cancellationToken);

            return new PaginatedResult<AppointmentReadDto>
            {
                Items = appointments,
                TotalRows = totalRows
            };
        }

        public async Task<AppointmentReadDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var appointment = await _context.Appointments
                .AsNoTracking()
                .Where(a => a.Id == id && !a.IsDelete)
                .Select(a => new AppointmentReadDto
                {
                    Id = a.Id,
                    PatientName = a.PatientName,
                    PatientEmail = a.PatientEmail,
                    PatientPhone = a.PatientPhone,
                    PatientGender = a.PatientGender,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    AppointmentDate = a.AppointmentDate
                })
                .FirstOrDefaultAsync(cancellationToken);

            return appointment!;
        }

        public async Task<Appointment> UpdateEntityAsync(Appointment entity, CancellationToken cancellationToken = default)
        {
            var existingAppointment = await _context.Appointments.AsNoTracking().FirstOrDefaultAsync(e=> e.Id.Equals(entity.Id) && !e.IsDelete, cancellationToken);

            if (existingAppointment != null)
            {
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = _userContext.UserName;
                _context.Appointments.Update(entity);
                _context.Entry(entity).Property(x => x.CreateBy).IsModified = false;
                _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(entity).Property(x => x.IsDelete).IsModified = false;
            }

            return entity;
        }

        public async Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Appointments.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id) && !e.IsDelete, cancellationToken);
            if (entity != null)
            {
                entity.IsDelete = true;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = _userContext.UserName;
                _context.Appointments.Update(entity);
                _context.Entry(entity).Property(x => x.CreateBy).IsModified = false;
                _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            }
        }
    }
}
