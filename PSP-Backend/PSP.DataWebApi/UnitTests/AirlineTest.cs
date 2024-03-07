using Application.AutoMapperProfiles;
using Application.MediatR.Queries.AirlineQueries;
using AutoMapper;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Moq;
using UnitTests.Mocks;

namespace UnitTests;

public class AirlineTest
{
    [Fact]
    public async void GetSortedRoute()
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
        var trueResult = AirlineMocks.GetFlightViewModel();
    }
}