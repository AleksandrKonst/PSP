using Application.AutoMapperProfiles;
using Application.DTO.ArmContextDTO.Delete;
using Application.DTO.ArmContextDTO.Insert;
using Application.DTO.ArmContextDTO.Search;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.FlightContextDTO;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.ARMCommands;
using Application.MediatR.Commands.FlightCommands;
using Application.MediatR.Commands.PassengerCommands;
using Application.MediatR.Queries.ARMQueries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mocks;

namespace UnitTests;

public class ARMTest
{
    [Fact]
    public async void SearchByPassenger()
    {
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        mockPassengerRepo.Setup(repo => repo
                .GetByFullNameWithCouponEventAsync("Виталий", "Райко", "Дмитриевич", "M", new DateOnly(2002,7,18), new List<int>() {2023}))
            .ReturnsAsync(ARMMocks.GetPassenger());
        
        var mockQuotaCategoryRepo = new Mock<IQuotaCategoryRepository>();
        mockQuotaCategoryRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(ARMMocks.GetQuotaCategory());
        
        var mockLogger = new Mock<ILogger<SearchByPassenger.Handler>>();
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new SearchByPassenger.Handler(mockPassengerRepo.Object, mockQuotaCategoryRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new SearchByPassenger.Query(new SearchByPassengerDTO()
        {
            QuotaBalancesYear = 2023,
            Name = "Виталий",
            Surname = "Райко",
            Patronymic = "Дмитриевич",
            Birthdate = new DateOnly(2002,7,18),
            Gender = "M",
            DocumentType = "01",
            DocumentNumber = "46464565656",
            Snils = "11111111111"
        }), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = ARMMocks.GetSearchPassengerResponseDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void SearchByTicket()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.GetAllAsync(1 , "2344555790")).ReturnsAsync(new List<CouponEvent>() {ARMMocks.GetTicket()});
        
        var mockLogger = new Mock<ILogger<SearchByTicket.Handler>>();
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var handler = new SearchByTicket.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new SearchByTicket.Query(new SearchByTicketDTO()
        {
            TicketType = 1,
            TicketNumber = "2344555790"
        }), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = ARMMocks.SearchTicketResponseDTO();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void SelectPassengerQuotaCount()
    {
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        mockPassengerRepo.Setup(repo => repo
                .GetByFullNameWithCouponEventAsync("Виталий", "Райко", "Дмитриевич", "M", new DateOnly(2002,7,18), new List<int>() {2023}))
            .ReturnsAsync(ARMMocks.GetPassenger());
        
        var mockQuotaCategoryRepo = new Mock<IQuotaCategoryRepository>();
        mockQuotaCategoryRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(ARMMocks.GetQuotaCategory());
        
        var mockLogger = new Mock<ILogger<SelectPassengerQuotaCount.Handler>>();
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });

        var mediator = new Mock<IMediator>();
        mediator.Setup(mock => mock.Send(new CheckPassenger.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18)), 
            new CancellationToken())).ReturnsAsync(new CheckPassenger.QueryResult(true));
        mediator.Setup(mock => mock.Send(new CheckPassengerType.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18)), 
            new CancellationToken())).ReturnsAsync(new CheckPassengerType.QueryResult(true));
        mediator.Setup(mock => mock.Send(new CreatePassenger.Command(new PassengerDTO()
            {
                Name = "Виталий",
                Surname = "Райко",
                Patronymic = "Дмитриевич",
                Birthdate = new DateOnly(2002,7,18),
                Gender = "M",
                PassengerTypes = new List<string> {"invalid_23"}
            }), 
            new CancellationToken())).ReturnsAsync(new CreatePassenger.CommandResult(true));

        var handler = new SelectPassengerQuotaCount.Handler(mockPassengerRepo.Object, mockQuotaCategoryRepo.Object, mediator.Object ,mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new SelectPassengerQuotaCount.Query(new List<SelectPassengerRequestDTO>()
        {
            new()
            {
                Id = 1,
                Name = "Виталий",
                Surname = "Райко",
                Patronymic = "Дмитриевич",
                Birthdate = new DateOnly(2002,7,18),
                Gender = "M",
                DocumentType = "01",
                DocumentNumber = "46464565656",
                Snils = "11111111111",
                QuotaBalancesYears = new List<int> {2023},
                Types = new List<string> {"invalid_23"}
            }
        }), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<dynamic>() {ARMMocks.GetPassengerQuotaCountDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CheckPassenger()
    {
        var handler = new CheckPassenger.Handler();
        
        var queryResult = await handler.Handle(new CheckPassenger.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18))
            , new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult,result);
    }
    
    [Fact]
    public async void CheckPassengerType()
    {
        var handler = new CheckPassengerType.Handler();
        
        var queryResult = await handler.Handle(new CheckPassengerType.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18))
            , new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(trueResult,result);
    }
    
    [Fact]
    public async void DeleteCouponEvent()
    {
        var mockRepo = new Mock<ICouponEventRepository>();
        mockRepo.Setup(repo => repo.DeleteByTicketAsync("issued", 1, "2344555790", new DateTime(2023,3,1, 16, 10, 0).ToUniversalTime()))
            .ReturnsAsync(1);
        
        var mockLogger = new Mock<ILogger<DeleteCouponEvent.Handler>>();
        
        var handler = new DeleteCouponEvent.Handler(mockRepo.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteCouponEvent.Command(new DeleteCouponEventRequestDTO()
        {
            OperationType = "issued",
            TicketType = 1,
            TicketNumber = "2344555790",
            OperationDatetime = "2023-3-1 16:10:00.000"
        }), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = 1;
        
        Assert.Equal(trueResult, result);
    }
    
    [Fact]
    public async void InsertCouponEvent()
    {
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        mockPassengerRepo.Setup(repo => repo.CheckByFullNameAsync("Виталий", "Райко", "Дмитриевич", "M",
                new DateOnly(2002,7,18)))
            .ReturnsAsync(true);
        mockPassengerRepo.Setup(repo => repo.GetByFullNameWithCouponEventAsync("Виталий", "Райко", "Дмитриевич", "M",
                new DateOnly(2002,7,18), new List<int>() {2023}))
            .ReturnsAsync(ARMMocks.GetPassenger);
        mockPassengerRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Passenger>())).ReturnsAsync(true);
        
        var mockQuotaCategoryRepo = new Mock<IQuotaCategoryRepository>();
        mockQuotaCategoryRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ARMMocks.GetQuotaCategory);
        
        var mockFlightRepo = new Mock<IFlightRepository>();
        mockFlightRepo.Setup(repo => repo.CheckByCodeAsync(180)).ReturnsAsync(true);
        
        var mockFareRepo = new Mock<IFareRepository>();
        mockFareRepo.Setup(repo => repo.CheckByCodeAsync("5NI")).ReturnsAsync(true);
        
        var mockPassengerTypeRepo = new Mock<IPassengerTypeRepository>();
        mockPassengerTypeRepo.Setup(repo => repo.GetByCodeAsync("invalid_23")).ReturnsAsync(new PassengerType()
        {
            Code = "invalid_23",
            Type = "инвалид с детства II или III группы",
            Description = "Инвалид с детства II или III группы от 18 лет",
            Appendices = new List<short>() {1,3,4},
            QuotaCategories = new List<string>() {"invalid"}
        });
        
        var mediator = new Mock<IMediator>();
        mediator.Setup(mock => mock.Send(new CheckPassenger.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18)), 
            new CancellationToken())).ReturnsAsync(new CheckPassenger.QueryResult(true));
        mediator.Setup(mock => mock.Send(new CheckPassengerType.Query("Виталий", "Райко", "Дмитриевич", new DateOnly(2002,7,18)), 
            new CancellationToken())).ReturnsAsync(new CheckPassengerType.QueryResult(true));
        mediator.Setup(mock => mock.Send(new CreatePassenger.Command(new PassengerDTO()
            {
                Name = "Виталий",
                Surname = "Райко",
                Patronymic = "Дмитриевич",
                Birthdate = new DateOnly(2002,7,18),
                Gender = "M",
                PassengerTypes = new List<string> {"invalid_23"}
            }), 
            new CancellationToken())).ReturnsAsync(new CreatePassenger.CommandResult(true));
        mediator.Setup(mock => mock.Send(new CreateFlight.Command(new FlightDTO()
            {
                Code = 180,
                AirlineCode = "SU",
                DepartPlace = "VVO",
                DepartDateTimePlan = new DateTime(2023, 3, 1),
                ArrivePlace = "SVO",
                ArriveDateTimePlan = new DateTime(2023, 3, 1),
                PnrCode = "5NR",
                FareCode = "5NR"
            }), 
            new CancellationToken())).ReturnsAsync(new CreateFlight.CommandResult(true));
        
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<InsertCouponEvent.Handler>>();
        
        var handler = new InsertCouponEvent.Handler(mockPassengerRepo.Object, mockQuotaCategoryRepo.Object, mockFlightRepo.Object, mockFareRepo.Object, mockPassengerTypeRepo.Object, mockMapper.CreateMapper(), mediator.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new InsertCouponEvent.Command(ARMMocks.GetInsertPassengerRequestDTO()), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new List<InsertPassengerResponseDTO>() {ARMMocks.InsertPassengerResponseDTO()};
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}