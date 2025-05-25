using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyApp.Controllers;
using PharmacyApp.DTOs;
using PharmacyApp.Exceptions;
using PharmacyApp.Services;

namespace PharmacyAppTests;

public class PatientsControllerTests
{
    private readonly Mock<IDbService> _mockService;
    private readonly PatientsController _controller;

    public PatientsControllerTests()
    {
        _mockService = new Mock<IDbService>();
        _controller = new PatientsController(_mockService.Object);
    }

    [Fact]
    public async Task GetPatientsAsync_ReturnsBadRequest_WhenIdIsLessThan1()
    {
        // Act
        var result = await _controller.GetPatientsAsync(0);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Id must be greater than 0", badRequest.Value);
    }

    [Fact]
    public async Task GetPatientsAsync_ReturnsNotFound_WhenPatientNotExists()
    {
        // Arrange
        int patientId = 123;
        _mockService
            .Setup(s => s.GetPatientDetailsAsync(patientId))
            .ThrowsAsync(new NotFoundException("Patient not found"));

        // Act
        var result = await _controller.GetPatientsAsync(patientId);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Patient not found", notFound.Value);
    }

    [Fact]
    public async Task GetPatientsAsync_ReturnsOk_WithPatientData()
    {
        // Arrange
        int patientId = 1;
        var patientData = new PatientGetDto
        {
            IdPatient = patientId,
            FirstName = "John",
            LastName = "Doe",
            Birthdate = new DateTime(1990, 1, 1)
        };

        _mockService
            .Setup(s => s.GetPatientDetailsAsync(patientId))
            .ReturnsAsync(patientData);

        // Act
        var result = await _controller.GetPatientsAsync(patientId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(patientData, okResult.Value);
    }
    
    [Fact]
    public async Task GetPatientsAsync_ReturnsSortedPrescriptions_WithFullDetails()
    {
        // Arrange
        int patientId = 1;
        var patientData = new PatientGetDto
        {
            IdPatient = patientId,
            FirstName = "John",
            LastName = "Doe",
            Birthdate = new DateTime(1990, 1, 1),
            Prescriptions = new List<PrescriptionGetDto>
            {
                new()
                {
                    IdPrescription = 2,
                    Date = new DateTime(2023, 1, 1),
                    DueDate = new DateTime(2023, 1, 5),
                    Medicament = new List<MedicamentPrescriptionGetDto>
                    {
                        new() { IdMedicament = 1, Name = "Med1", Dose = 1, Description = "Desc1", Type = "Type1" }
                    },
                    Doctor = new DoctorGetDto { IdDoctor = 1, FirstName = "Doc1", LastName = "Last1", Email = "doc1@example.com" }
                },
                new()
                {
                    IdPrescription = 1,
                    Date = new DateTime(2023, 1, 2),
                    DueDate = new DateTime(2023, 1, 6),
                    Medicament = new List<MedicamentPrescriptionGetDto>
                    {
                        new() { IdMedicament = 2, Name = "Med2", Dose = 2, Description = "Desc2", Type = "Type2" }
                    },
                    Doctor = new DoctorGetDto { IdDoctor = 2, FirstName = "Doc2", LastName = "Last2", Email = "doc2@example.com" }
                }
            }
        };

        _mockService
            .Setup(s => s.GetPatientDetailsAsync(patientId))
            .ReturnsAsync(patientData);

        // Act
        var result = await _controller.GetPatientsAsync(patientId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedData = Assert.IsType<PatientGetDto>(okResult.Value);
        Assert.Equal(2, returnedData.Prescriptions.Count);
        var prescriptionsList = returnedData.Prescriptions.ToList();
        Assert.True(prescriptionsList[0].DueDate < prescriptionsList[1].DueDate);
        Assert.NotNull(prescriptionsList[0].Doctor);
        Assert.NotNull(prescriptionsList[0].Medicament);
    }
}