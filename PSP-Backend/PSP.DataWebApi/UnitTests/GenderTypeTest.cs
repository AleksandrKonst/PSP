using Application.AutoMapperProfiles;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Queries.GenderTypeQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class GenderTypeTest
{
    [Fact]
    public async void GetGenderTypeById()
    {
        var mockRepo = new Mock<IGenderTypeRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("M"))
            .ReturnsAsync(GenderTypeMocks.GetGenderType());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetGenderTypeById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetGenderTypeById.Query("M"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = GenderTypeMocks.GetGenderTypeDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetGenderTypeCount()
    {
        var mockRepo = new Mock<IGenderTypeRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetGenderTypeCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetGenderTypeCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetGenderTypes()
    {
        var mockRepo = new Mock<IGenderTypeRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<GenderType>() {GenderTypeMocks.GetGenderType()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetGenderTypes.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetGenderTypes.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<GenderTypeDTO>() {GenderTypeMocks.GetGenderTypeDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}