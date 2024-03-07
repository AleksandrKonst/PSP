using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Queries.CityQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class CityTest
{
    [Fact]
    public async void GetCityById()
    {
        var mockRepo = new Mock<ICityRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("MOW"))
            .ReturnsAsync(CityMocks.GetCity());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetCityById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetCityById.Query("MOW"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = CityMocks.GetCityDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetCityCount()
    {
        var mockRepo = new Mock<ICityRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetCityCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetCityCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetCities()
    {
        var mockRepo = new Mock<ICityRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<City>() {CityMocks.GetCity()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetCities.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetCities.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<CityDTO>() {CityMocks.GetCityDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}