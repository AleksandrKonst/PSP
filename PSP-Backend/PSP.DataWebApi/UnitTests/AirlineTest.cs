using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Queries.AirlineQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class AirlineTest
{
    [Fact]
    public async void GetAirlineById()
    {
        var mockRepo = new Mock<IAirlineRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("SU"))
            .ReturnsAsync(AirlineMocks.GetAirline());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetAirlineById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetAirlineById.Query("SU"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = AirlineMocks.GetAirlineDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetAirlineCount()
    {
        var mockRepo = new Mock<IAirlineRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetAirlineCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetAirlineCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetAirlines()
    {
        var mockRepo = new Mock<IAirlineRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<Airline>() {AirlineMocks.GetAirline()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetAirlines.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetAirlines.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<AirlineDTO>() {AirlineMocks.GetAirlineDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}