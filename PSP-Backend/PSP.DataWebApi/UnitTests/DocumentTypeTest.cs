using Application.AutoMapperProfiles;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.DocumentTypeCommands;
using Application.MediatR.Queries.DocumentTypeQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.Extensions.Logging;
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
    
    [Fact]
    public async void CreateDocumentType()
    {
        var mockRepo = new Mock<IDocumentTypeRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<DocumentType>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateDocumentType.Handler>>();
        
        var handler = new CreateDocumentType.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateDocumentType.Command(DocumentTypeMocks.GetDocumentTypeDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void DeleteDocumentType()
    {
        var mockRepo = new Mock<IDocumentTypeRepository>();
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<DeleteDocumentType.Handler>>();
        
        var handler = new DeleteDocumentType.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteDocumentType.Command("00"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void UpdateDocumentType()
    {
        var mockRepo = new Mock<IDocumentTypeRepository>();
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<DocumentType>())).ReturnsAsync(true);
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<UpdateDocumentType.Handler>>();
        
        var handler = new UpdateDocumentType.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new UpdateDocumentType.Command(DocumentTypeMocks.GetDocumentTypeDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult, result);
    }
}