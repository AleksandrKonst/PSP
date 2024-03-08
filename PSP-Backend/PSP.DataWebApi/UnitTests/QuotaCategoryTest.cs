using Application.AutoMapperProfiles;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.QuotaCategoryCommands;
using Application.MediatR.Queries.QuotaCategoryQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class QuotaCategoryTest
{
    [Fact]
    public async void GetQuotaCategoryById()
    {
        var mockRepo = new Mock<IQuotaCategoryRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("invalid"))
            .ReturnsAsync(QuotaCategoryMocks.GetQuotaCategory());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetQuotaCategoryById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetQuotaCategoryById.Query("invalid"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = QuotaCategoryMocks.GetQuotaCategoryDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetQuotaCategoryCount()
    {
        var mockRepo = new Mock<IQuotaCategoryRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetQuotaCategoryCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetQuotaCategoryCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetQuotaCategories()
    {
        var mockRepo = new Mock<IQuotaCategoryRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<QuotaCategory>() {QuotaCategoryMocks.GetQuotaCategory()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetQuotaCategories.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetQuotaCategories.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<QuotaCategoryDTO>() {QuotaCategoryMocks.GetQuotaCategoryDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreateQuotaCategory()
    {
        var mockRepo = new Mock<IQuotaCategoryRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<QuotaCategory>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateQuotaCategory.Handler>>();
        
        var handler = new CreateQuotaCategory.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateQuotaCategory.Command(QuotaCategoryMocks.GetQuotaCategoryDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeleteQuotaCategory()
    {
        var mockRepo = new Mock<IQuotaCategoryRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeleteQuotaCategory.Handler>>();
        
        var handler = new DeleteQuotaCategory.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteQuotaCategory.Command("age"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdateQuotaCategory()
    {
        var mockRepo = new Mock<IQuotaCategoryRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<QuotaCategory>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdateQuotaCategory.Handler>>();
        
        var handler = new UpdateQuotaCategory.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdateQuotaCategory.Command(QuotaCategoryMocks.GetQuotaCategoryDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}