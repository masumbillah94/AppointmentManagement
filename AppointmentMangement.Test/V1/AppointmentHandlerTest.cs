
using AutoMapper;
using Moq;
using Application.Appointments.Commands;
using Application.Appointments.Handlers;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using Domain.Entities.Appointments;

public class AppointmentAddCommandHandlerTests
{
    private readonly Mock<IRepositoryFacade> _repositoryFacade;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AppointmentAddCommandHandler _handler;

    public AppointmentAddCommandHandlerTests()
    {

        _repositoryFacade = new Mock<IRepositoryFacade>();
        _mockMapper = new Mock<IMapper>();
        _handler = new AppointmentAddCommandHandler(_repositoryFacade.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenAppointmentIsAddedSuccessfully()
    {
        
        var appointmentDate = DateTime.Now.AddDays(5);
        var command = new AppointmentAddCommand 
        {
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate,
        };
        var appointment = new Appointment
        {
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate
        };
        var savedAppointment = new Appointment
        {
            Id = 1,
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate,
            CreateBy = "Default",
            CreatedDate = DateTime.Now
        };
        var appointmentDto = new AppointmentReadDto 
        {
            Id = 1,
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate
        };

        _mockMapper.Setup(m => m.Map<Appointment>(command)).Returns(appointment);
        _repositoryFacade.Setup(repo => repo.AppointmentRepo.AddEntityAsync(appointment, It.IsAny<CancellationToken>()))
            .ReturnsAsync(savedAppointment);
        _repositoryFacade.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mockMapper.Setup(m => m.Map<AppointmentReadDto>(savedAppointment)).Returns(appointmentDto);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.True(response.Success);
        Assert.Equal("Appointment added successfully", response.Message);

    }

    [Fact]
    public async Task Handle_ShouldReturnInvalidResponse_WhenSaveChangesFails()
    {
        var appointmentDate = DateTime.Now.AddDays(5);
        var command = new AppointmentAddCommand
        {
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate,
        };
        var appointment = new Appointment
        {
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate
        };
        var savedAppointment = new Appointment
        {
            Id = 1,
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate,
            CreateBy = "Default",
            CreatedDate = DateTime.Now
        };
        var appointmentDto = new AppointmentReadDto
        {
            Id = 1,
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = DateTime.Now.AddDays(5)
        };

        _mockMapper.Setup(m => m.Map<Appointment>(command)).Returns(appointment);
        _repositoryFacade.Setup(repo => repo.AppointmentRepo.AddEntityAsync(appointment, It.IsAny<CancellationToken>()))
            .ReturnsAsync(savedAppointment);
        _repositoryFacade.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        _mockMapper.Setup(m => m.Map<AppointmentReadDto>(savedAppointment)).Returns(appointmentDto);


        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.False(response.Success);
        Assert.Equal("Appointment Not Created", response.Message);
        Assert.Equal(0, response.Count);
        Assert.Null(response.Exception);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrorResponse_WhenExceptionIsThrown()
    {
        
        var appointmentDate = DateTime.Now.AddDays(5);
        var command = new AppointmentAddCommand
        {
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate,
        };
        var appointment = new Appointment
        {
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate
        };
        var savedAppointment = new Appointment
        {
            Id = 1,
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = appointmentDate,
            CreateBy = "Default",
            CreatedDate = DateTime.Now
        };
        var appointmentDto = new AppointmentReadDto
        {
            Id = 1,
            PatientName = "Test Client",
            PatientEmail = "Test Person",
            PatientPhone = "1234567890",
            PatientGender = "Male",
            DoctorId = 1,
            AppointmentDate = DateTime.Now.AddDays(5)
        };
        var exception = new Exception("Unexpected error");

        _mockMapper.Setup(m => m.Map<Appointment>(command)).Returns(appointment);
        _repositoryFacade.Setup(repo => repo.AppointmentRepo.AddEntityAsync(appointment, It.IsAny<CancellationToken>()))
            .ReturnsAsync(savedAppointment);
        _repositoryFacade.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);
        _mockMapper.Setup(m => m.Map<AppointmentReadDto>(savedAppointment)).Returns(appointmentDto);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.False(response.Success);
        Assert.Contains("Unexpected error", response.Message);
        Assert.Equal(exception, response.Exception);
    }
}

