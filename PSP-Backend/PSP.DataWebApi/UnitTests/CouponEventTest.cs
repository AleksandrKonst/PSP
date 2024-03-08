using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.CouponEventCommands;
using Application.MediatR.Queries.CouponEventQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class CouponEventTest
{
    [Fact]
    public async void GetCouponEventById()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync(1))
            .ReturnsAsync(CouponEventMocks.GetCouponEvent);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetCouponEventById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetCouponEventById.Query(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = CouponEventMocks.GetCouponEventDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetCouponEventCount()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetCouponEventCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetCouponEventCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetCouponEvents()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<CouponEvent>() {CouponEventMocks.GetCouponEvent()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetCouponEvents.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetCouponEvents.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<CouponEventDTO>() {CouponEventMocks.GetCouponEventDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreateCouponEvent()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<CouponEvent>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateCouponEvent.Handler>>();
        
        var handler = new CreateCouponEvent.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateCouponEvent.Command(CouponEventMocks.GetCouponEventDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeleteCouponEvent()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeleteCouponEvent.Handler>>();
        
        var handler = new DeleteCouponEvent.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteCouponEvent.Command(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdateCouponEvent()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<CouponEvent>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdateCouponEvent.Handler>>();
        
        var handler = new UpdateCouponEvent.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdateCouponEvent.Command(CouponEventMocks.GetCouponEventDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}