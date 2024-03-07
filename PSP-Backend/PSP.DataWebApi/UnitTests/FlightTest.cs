using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Queries.FlightQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class FlightTest
{
    [Fact]
    public async void GetFlightById()
    {
        var mockRepo = new Mock<IFlightRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync(180))
            .ReturnsAsync(FlightMocks.GetFlight());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetFlightById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetFlightById.Query(180), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = FlightMocks.GetFlightDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetFlightCount()
    {
        var mockRepo = new Mock<IFlightRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetFlightCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetFlightCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetFlights()
    {
        var mockRepo = new Mock<IFlightRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<Flight>() {FlightMocks.GetFlight()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetFlights.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetFlights.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<FlightDTO>() {FlightMocks.GetFlightDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}