using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.SubsidizedRouteCommands;
using Application.MediatR.Queries.SubsidizedRouteQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class SubsidizedRouteTest
{
    [Fact]
    public async void GetSubsidizedRouteById()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync(1))
            .ReturnsAsync(SubsidizedRouteMocks.GetSubsidizedRoute());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetSubsidizedRouteById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetSubsidizedRouteById.Query(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = SubsidizedRouteMocks.GetSubsidizedRouteDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetSubsidizedRouteCount()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetSubsidizedRouteCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetSubsidizedRouteCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetSubsidizedRoutes()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<SubsidizedRoute>() {SubsidizedRouteMocks.GetSubsidizedRoute()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetSubsidizedRoutes.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetSubsidizedRoutes.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<SubsidizedRouteDTO>() {SubsidizedRouteMocks.GetSubsidizedRouteDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetSubsidizedRouteByAppendix()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.GetAllByAppendixAsync(1))
            .ReturnsAsync( new List<SubsidizedRoute>() {SubsidizedRouteMocks.GetSubsidizedRoute()});
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetSubsidizedRouteByAppendix.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetSubsidizedRouteByAppendix.Query(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<SubsidizedRouteDTO>() {SubsidizedRouteMocks.GetSubsidizedRouteDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreateSubsidizedRoute()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<SubsidizedRoute>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateSubsidizedRoute.Handler>>();
        
        var handler = new CreateSubsidizedRoute.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateSubsidizedRoute.Command(SubsidizedRouteMocks.GetSubsidizedRouteDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeleteSubsidizedRoute()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeleteSubsidizedRoute.Handler>>();
        
        var handler = new DeleteSubsidizedRoute.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteSubsidizedRoute.Command(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdateSubsidizedRoute()
    {
        var mockRepo = new Mock<ISubsidizedRouteRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<SubsidizedRoute>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdateSubsidizedRoute.Handler>>();
        
        var handler = new UpdateSubsidizedRoute.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdateSubsidizedRoute.Command(SubsidizedRouteMocks.GetSubsidizedRouteDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}