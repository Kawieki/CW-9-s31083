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
}