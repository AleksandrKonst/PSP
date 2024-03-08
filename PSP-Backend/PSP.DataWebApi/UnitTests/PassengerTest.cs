using Application.AutoMapperProfiles;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.PassengerCommands;
using Application.MediatR.Queries.PassengerQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class PassengerTest
{
    [Fact]
    public async void GetPassengerById()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(PassengerMocks.GetPassenger());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetPassengerById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetPassengerById.Query(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = PassengerMocks.GetPassengerDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetPassengerCount()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetPassengerCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetPassengerCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetPassengers()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<Passenger>() {PassengerMocks.GetPassenger()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetPassengers.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetPassengers.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<PassengerDTO>() {PassengerMocks.GetPassengerDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetPassengerByFio()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.GetByFullNameAsync("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18)))
            .ReturnsAsync(PassengerMocks.GetPassenger());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetPassengerByFio.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetPassengerByFio.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18)), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = PassengerMocks.GetPassengerDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async Task GetPassengerQuota()
    {
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        mockPassengerRepo.Setup(repo => repo
                .GetByFullNameWithCouponEventAsync("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18), new List<int>() {2023}))
            .ReturnsAsync(PassengerMocks.GetPassengerQuota);
        
        var mockQuotaCategoryRepo = new Mock<IQuotaCategoryRepository>();
        mockQuotaCategoryRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(ARMMocks.GetQuotaCategory());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetPassengerQuota.Handler(mockPassengerRepo.Object, mockQuotaCategoryRepo.Object,mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetPassengerQuota.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18), 2023), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = PassengerMocks.GetPassengerQuotaDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreatePassenger()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Passenger>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreatePassenger.Handler>>();
        
        var handler = new CreatePassenger.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreatePassenger.Command(PassengerMocks.GetPassengerDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeletePassenger()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeletePassenger.Handler>>();
        
        var handler = new DeletePassenger.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeletePassenger.Command(1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdatePassenger()
    {
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Passenger>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdatePassenger.Handler>>();
        
        var handler = new UpdatePassenger.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdatePassenger.Command(PassengerMocks.GetPassengerDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}