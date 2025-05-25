namespace PharmacyAppTests;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyApp.Controllers;
using PharmacyApp.DTOs;
using PharmacyApp.Exceptions;
using PharmacyApp.Services;
using Xunit;

public class PrescriptionsControllerTests
{
    private readonly Mock<IDbService> _mockService;
    private readonly PrescriptionsController _controller;

    public PrescriptionsControllerTests()
    {
        _mockService = new Mock<IDbService>();
        _controller = new PrescriptionsController(_mockService.Object);
    }

    [Fact]
    public async Task CreateNewPrescriptionAsync_ReturnsOk_WhenValid()
    {
        // Arrange
        var dto = GetValidDto();
        _mockService.Setup(s => s.CreateNewPrescriptionAsync(dto)).ReturnsAsync(dto);

        // Act
        var result = await _controller.CreateNewPrescriptionAsync(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(dto, okResult.Value);
    }

    [Fact]
    public async Task CreateNewPrescriptionAsync_ReturnsNotFound_WhenDoctorNotFound()
    {
        // Arrange
        var dto = GetValidDto();
        _mockService.Setup(s => s.CreateNewPrescriptionAsync(dto))
            .ThrowsAsync(new NotFoundException("Doctor does not exist!"));

        // Act
        var result = await _controller.CreateNewPrescriptionAsync(dto);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Doctor does not exist!", notFound.Value);
    }

    [Fact]
    public async Task CreateNewPrescriptionAsync_ReturnsBadRequest_WhenDateInvalid()
    {
        // Arrange
        var dto = GetValidDto();
        _mockService.Setup(s => s.CreateNewPrescriptionAsync(dto))
            .ThrowsAsync(new DateValidationException("The DueDate must be greater than or equal to the Date."));

        // Act
        var result = await _controller.CreateNewPrescriptionAsync(dto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("The DueDate must be greater than or equal to the Date.", badRequest.Value);
    }

    [Fact]
    public async Task CreateNewPrescriptionAsync_ReturnsBadRequest_WhenMaxLimitReached()
    {
        // Arrange
        var dto = GetValidDto();
        _mockService.Setup(s => s.CreateNewPrescriptionAsync(dto))
            .ThrowsAsync(new MaxLimitReached("A prescription can include a maximum of 10 medicaments."));

        // Act
        var result = await _controller.CreateNewPrescriptionAsync(dto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A prescription can include a maximum of 10 medicaments.", badRequest.Value);
    }
    
    [Fact]
    public async Task CreateNewPrescriptionAsync_ReturnsNotFound_WhenMedicamentNotExists()
    {
        // Arrange
        var dto = GetValidDto();
        dto.Medicaments[0].IdMedicament = 2137;
        _mockService.Setup(s => s.CreateNewPrescriptionAsync(dto))
            .ThrowsAsync(new NotFoundException("Medicament with id: 2137 does not exist."));

        // Act
        var result = await _controller.CreateNewPrescriptionAsync(dto);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Medicament with id: 2137 does not exist.", notFound.Value);
    }
    
    [Fact]
    public async Task CreateNewPrescriptionAsync_AddsNewPatient_WhenPatientDoesNotExist()
    {
        // Arrange
        var dto = GetValidDto();
        dto.Patient.IdPatient = 2137;
        _mockService.Setup(s => s.CreateNewPrescriptionAsync(dto)).ReturnsAsync(dto);

        // Act
        var result = await _controller.CreateNewPrescriptionAsync(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(dto, okResult.Value);
        _mockService.Verify(s => s.CreateNewPrescriptionAsync(dto), Times.Once);
    }
    
    private PrescriptionCreateDto GetValidDto() => new()
    {
        Patient = new PatientDto
        {
            IdPatient = 1,
            FirstName = "John",
            LastName = "Doe",
            Birthdate = new DateTime(1990, 1, 1)
        },
        Doctor = new DoctorDto
        {
            IdDoctor = 1
        },
        Date = DateTime.Now,
        DueDate =DateTime.Now.AddDays(5),
        Medicaments =
        [
            new MedicamentDto(idMedicament: 1, dose: 1, details: "Take after meal")
        ]
    };
}
