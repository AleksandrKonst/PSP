using Application.AutoMapperProfiles;
using Application.MediatR.Queries;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Test.Mocks;

namespace Test;

public class RouteTest
{
    [Fact]
    public async void GetSortedRoute()
    {
        var mockRepo = new Mock<IFlightRepository>();
        mockRepo.Setup(repo => repo.GetAllForClientAsync("Москва", "Владивосток", new DateTime(2023, 3, 1).ToUniversalTime()))
            .ReturnsAsync(RouteMocks.GetFlight());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<GetSortedRoute.Handler>>();

        var handler = new GetSortedRoute.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);

        var queryResult = await handler.Handle(new GetSortedRoute.Query("Владивосток", "Москва", new DateOnly(2023, 3, 1)), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = RouteMocks.GetFlightViewModel();

        Assert.Equal(trueResult.Count, result.Count());
        Assert.Equal(trueResult.First().ArrivePlace, result.First().ArrivePlace);
        Assert.Equal(trueResult.First().DepartPlace, result.First().DepartPlace);
        Assert.Equal(trueResult.First().ArriveDatetimePlan, result.First().ArriveDatetimePlan);
        Assert.Equal(trueResult.First().DepartDatetimePlan, result.First().DepartDatetimePlan);
        Assert.Equal(trueResult.First().FareCode, result.First().FareCode);
        Assert.Equal(trueResult.First().FlightSegments.Count, result.First().FlightSegments.Count);
    }
}