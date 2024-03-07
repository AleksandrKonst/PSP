using Application.AutoMapperProfiles;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Queries.DocumentTypeQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class DocumentTypeTest
{
    
    [Fact]
    public async void GetDocumentTypeById()
    {
        var mockRepo = new Mock<IDocumentTypeRepository>();
        mockRepo.Setup(repo => repo.GetByCodeAsync("00"))
            .ReturnsAsync(DocumentTypeMocks.GetDocumentType());
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new GetDocumentTypeById.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetDocumentTypeById.Query("00"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = DocumentTypeMocks.GetDocumentTypeDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetDocumentTypeCount()
    {
        var mockRepo = new Mock<IDocumentTypeRepository>();
        mockRepo.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(1);

        var handler = new GetDocumentTypeCount.Handler(mockRepo.Object);
        
        var queryResult = await handler.Handle(new GetDocumentTypeCount.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetDocumentTypes()
    {
        var mockRepo = new Mock<IDocumentTypeRepository>();
        mockRepo.Setup(repo => repo.GetPartAsync(0, 1))
            .ReturnsAsync(new List<DocumentType>() {DocumentTypeMocks.GetDocumentType()});

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetDocumentTypes.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetDocumentTypes.Query(0, 1), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<DocumentTypeDTO>() {DocumentTypeMocks.GetDocumentTypeDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}