using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.AirportCommands;
using Application.MediatR.Queries.AirportQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class AirportTest
{
    [Fact]
    public async void GetAirportById()
    {
        var mockRepo = new Mock<IAirportRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("SVO"))
            .ReturnsAsync(AirportMocks.GetAirport());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetAirportById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetAirportById.Query("SVO"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = AirportMocks.GetAirportDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetAirportCount()
    {
        var mockRepo = new Mock<IAirportRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetAirportCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetAirportCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetAirports()
    {
        var mockRepo = new Mock<IAirportRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<Airport>() {AirportMocks.GetAirport()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetAirports.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetAirports.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<AirportDTO>() {AirportMocks.GetAirportDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreateAirport()
    {
        var mockRepo = new Mock<IAirportRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Airport>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateAirport.Handler>>();
        
        var handler = new CreateAirport.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateAirport.Command(AirportMocks.GetAirportDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeleteAirport()
    {
        var mockRepo = new Mock<IAirportRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeleteAirport.Handler>>();
        
        var handler = new DeleteAirport.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteAirport.Command("VVO"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdateAirport()
    {
        var mockRepo = new Mock<IAirportRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Airport>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdateAirport.Handler>>();
        
        var handler = new UpdateAirport.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdateAirport.Command(AirportMocks.GetAirportDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}