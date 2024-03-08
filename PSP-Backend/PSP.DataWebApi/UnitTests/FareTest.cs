using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.FareCommands;
using Application.MediatR.Queries.FareQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class FareTest
{
    [Fact]
    public async void GetFareById()
    {
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("5NI"))
            .ReturnsAsync(FareMocks.GetFare());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetFareById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetFareById.Query("5NI"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = FareMocks.GetFareDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetFareCount()
    {
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetFareCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetFareCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetFares()
    {
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<Fare>() {FareMocks.GetFare()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetFares.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetFares.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<FareDTO>() {FareMocks.GetFareDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreateFare()
    {
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Fare>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateFare.Handler>>();
        
        var handler = new CreateFare.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateFare.Command(FareMocks.GetFareDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeleteFare()
    {
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeleteFare.Handler>>();
        
        var handler = new DeleteFare.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteFare.Command("5NI"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdateFare()
    {
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Fare>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdateFare.Handler>>();
        
        var handler = new UpdateFare.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdateFare.Command(FareMocks.GetFareDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}