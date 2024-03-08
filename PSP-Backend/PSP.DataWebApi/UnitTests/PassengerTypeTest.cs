using Application.AutoMapperProfiles;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.PassengerTypeCommands;
using Application.MediatR.Queries.PassengerTypeQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class PassengerTypeTest
{
    [Fact]
    public async void GetPassengerTypeById()
    {
        var mockRepo = new Mock<IPassengerTypeRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("child"))
            .ReturnsAsync(PassengerTypeMocks.GetPassengerType());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetPassengerTypeById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetPassengerTypeById.Query("child"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = PassengerTypeMocks.GetPassengerTypeDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetPassengerTypeCount()
    {
        var mockRepo = new Mock<IPassengerTypeRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetPassengerTypeCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetPassengerTypeCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetPassengerTypes()
    {
        var mockRepo = new Mock<IPassengerTypeRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<PassengerType>() {PassengerTypeMocks.GetPassengerType()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetPassengerTypes.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetPassengerTypes.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<PassengerTypeDTO>() {PassengerTypeMocks.GetPassengerTypeDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreatePassengerType()
    {
        var mockRepo = new Mock<IPassengerTypeRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<PassengerType>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreatePassengerType.Handler>>();
        
        var handler = new CreatePassengerType.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreatePassengerType.Command(PassengerTypeMocks.GetPassengerTypeDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeletePassengerType()
    {
        var mockRepo = new Mock<IPassengerTypeRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeletePassengerType.Handler>>();
        
        var handler = new DeletePassengerType.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeletePassengerType.Command("age"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdatePassengerType()
    {
        var mockRepo = new Mock<IPassengerTypeRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<PassengerType>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdatePassengerType.Handler>>();
        
        var handler = new UpdatePassengerType.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdatePassengerType.Command(PassengerTypeMocks.GetPassengerTypeDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}