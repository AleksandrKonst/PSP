using Application.AutoMapperProfiles;
using Application.MediatR.Queries;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

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
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}