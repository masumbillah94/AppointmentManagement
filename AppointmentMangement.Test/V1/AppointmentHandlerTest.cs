//namespace AppointmentMangement.Test.V1
//{
//    public class AppointmentHandlerTest
//    {
//        [Fact]
//        public void Test1()
//        {

//        }
//    }
//}

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Application.Appointments.Commands;
using Application.Appointments.Handlers;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using Domain.Entities.Appointments;
using Repository.Base;
using Domain.Abstractions.Appointments;
using Domain.Abstractions.Users;
using Microsoft.AspNetCore.Http.HttpResults;

public class AppointmentAddCommandHandlerTests
{
    private readonly Mock<IRepositoryFacade> _repositoryFacade;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AppointmentAddCommandHandler _handler;

    public AppointmentAddCommandHandlerTests()
    {

        _repositoryFacade = new Mock<IRepositoryFacade>();
        _mockMapper = new Mock<IMapper>();
        // Initialize the handler
        _handler = new AppointmentAddCommandHandler(_repositoryFacade.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenAppointmentIsAddedSuccessfully()
    {
        // Arrange
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


        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Appointment added successfully", response.Message);

    }

    [Fact]
    public async Task Handle_ShouldReturnInvalidResponse_WhenSaveChangesFails()
    {
        // Arrange
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


        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(response.Success);
        Assert.Equal("Appointment Not Created", response.Message);
        Assert.Equal(0, response.Count);
        Assert.Null(response.Exception);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrorResponse_WhenExceptionIsThrown()
    {
        // Arrange
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

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Unexpected error", response.Message);
        Assert.Equal(exception, response.Exception);
    }
}

