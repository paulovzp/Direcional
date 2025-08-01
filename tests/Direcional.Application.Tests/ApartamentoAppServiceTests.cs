using Direcional.Application.Common;
using Direcional.Domain;
using Direcional.Infrastructure.Exceptions;
using Direcional.Persistence;
using Moq;
using System.Linq.Expressions;

namespace Direcional.Application.Tests;

public class ApartamentoAppServiceTests
{
    private readonly Mock<IApartamentoService> _apartamentoService = new();
    private readonly Mock<IApartamentoRepository> _apartamentoRepository = new();
    private readonly Mock<IDirecionalUnitOfWork> _unitOfWork = new();

    private IApartamentoAppService CreateService() =>
        new ApartamentoAppService(_apartamentoService.Object, _apartamentoRepository.Object, _unitOfWork.Object);

    #region CRUD Tests

    [Fact]
    public async Task Add_ShouldReturnApartamentoId_WhenValidRequestProvided()
    {
        // Arrange
        var service = CreateService();
        var request = new ApartamentoCreateRequest
        {
            Nome = "Apartamento Premium",
            Numero = 101,
            Andar = 1,
            ValorVenda = 250000m
        };

        var apartamento = new Apartamento
        {
            Id = 1,
            Nome = request.Nome,
            Numero = request.Numero,
            Andar = request.Andar,
            ValorVenda = request.ValorVenda
        };

        _apartamentoService.Setup(x => x.Add(It.IsAny<Apartamento>()))
            .ReturnsAsync(apartamento);
        _unitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

        // Act
        var result = await service.Add(request);

        // Assert
        Assert.Equal(1, result);
        _apartamentoService.Verify(x => x.Add(It.Is<Apartamento>(a =>
            a.Nome == request.Nome &&
            a.Numero == request.Numero &&
            a.Andar == request.Andar &&
            a.ValorVenda == request.ValorVenda)), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Read_ShouldReturnApartamento_WhenValidIdProvided()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 1;
        var apartamento = new Apartamento
        {
            Id = apartamentoId,
            Nome = "Apartamento Premium",
            Numero = 101,
            Andar = 1,
            ValorVenda = 250000m
        };

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync(apartamento);

        // Act
        var result = await service.Read(apartamentoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(apartamentoId, result.Id);
        Assert.Equal("Apartamento Premium", result.Nome);
        Assert.Equal(101, result.Numero);
        Assert.Equal(1, result.Andar);
        Assert.Equal(250000m, result.ValorVenda);
    }

    [Fact]
    public async Task Read_ShouldThrowNotFoundException_WhenApartamentoNotFound()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 999;

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync((Apartamento?)null);

        // Act & Assert
        await Assert.ThrowsAsync<DirecionalNotFoundException>(() => service.Read(apartamentoId));
    }

    [Fact]
    public async Task Update_ShouldUpdateApartamento_WhenValidRequestProvided()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 1;
        var request = new ApartamentoUpdateRequest
        {
            Nome = "Apartamento Premium Atualizado",
            ValorVenda = 280000m
        };

        var apartamento = new Apartamento
        {
            Id = apartamentoId,
            Nome = "Apartamento Premium",
            Numero = 101,
            Andar = 1,
            ValorVenda = 250000m
        };

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync(apartamento);
        _unitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

        // Act
        await service.Update(apartamentoId, request);

        // Assert
        _apartamentoService.Verify(x => x.Update(It.Is<Apartamento>(a =>
            a.Nome == request.Nome &&
            a.ValorVenda == request.ValorVenda)), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldDeleteApartamento_WhenValidIdProvided()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 1;
        var apartamento = new Apartamento
        {
            Id = apartamentoId,
            Nome = "Apartamento Premium",
            Numero = 101,
            Andar = 1,
            ValorVenda = 250000m
        };

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync(apartamento);
        _unitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

        // Act
        await service.Delete(apartamentoId);

        // Assert
        _apartamentoService.Verify(x => x.Delete(apartamento), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }

    #endregion

    #region Filter Tests

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByNome()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ApartamentoFilterRequest { Nome = "Premium" }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 1, Nome = "Apartamento Premium", Numero = 101, Andar = 1, ValorVenda = 250000m },
            new() { Id = 2, Nome = "Premium Suite", Numero = 102, Andar = 1, ValorVenda = 300000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 2);
        _apartamentoRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Apartamento, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, apt => Assert.Contains("Premium", apt.Nome));
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByNumero()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ApartamentoFilterRequest { Numero = 101 }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 1, Nome = "Apartamento Premium", Numero = 101, Andar = 1, ValorVenda = 250000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 1);
        _apartamentoRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Apartamento, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(101, result.Data.First().Numero);
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByAndar()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ApartamentoFilterRequest { Andar = 5 }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 1, Nome = "Apartamento 501", Numero = 501, Andar = 5, ValorVenda = 350000m },
            new() { Id = 2, Nome = "Apartamento 502", Numero = 502, Andar = 5, ValorVenda = 360000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 2);
        _apartamentoRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Apartamento, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, apt => Assert.Equal(5, apt.Andar));
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByPriceRange()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ApartamentoFilterRequest
            {
                ValorVendaInicio = 200000m,
                ValorVendaFim = 300000m
            }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 1, Nome = "Apartamento A", Numero = 101, Andar = 1, ValorVenda = 250000m },
            new() { Id = 2, Nome = "Apartamento B", Numero = 102, Andar = 1, ValorVenda = 280000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 2);
        _apartamentoRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Apartamento, bool>>>>(exprs => exprs.Count == 2), // Two price filters
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, apt => Assert.InRange(apt.ValorVenda, 200000m, 300000m));
    }

    [Fact]
    public async Task Read_ShouldReturnEmptyList_WhenFilterIsNull()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = null
        };

        var emptyResult = new Tuple<IEnumerable<Apartamento>, int>(new List<Apartamento>(), 0);
        _apartamentoRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Apartamento, bool>>>>(exprs => exprs.Count == 0),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(emptyResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalCount);
    }

    #endregion

    #region Disponivel Method Tests

    [Fact]
    public async Task Disponivel_ShouldReturnAvailableMessage_WhenApartamentoIsAvailable()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 1;
        var apartamento = new Apartamento
        {
            Id = apartamentoId,
            Nome = "Apartamento Premium",
            Numero = 101,
            Andar = 5,
            ValorVenda = 250000m
        };

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync(apartamento);
        _apartamentoService.Setup(x => x.Disponivel(apartamentoId))
            .ReturnsAsync(true);

        // Act
        var result = await service.Disponivel(apartamentoId);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Apartamento Premium 101 no andar 5", result.Message);
        Assert.Contains("disponível", result.Message);
        _apartamentoService.Verify(x => x.Disponivel(apartamentoId), Times.Once);
    }

    [Fact]
    public async Task Disponivel_ShouldReturnUnavailableMessage_WhenApartamentoIsNotAvailable()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 1;
        var apartamento = new Apartamento
        {
            Id = apartamentoId,
            Nome = "Apartamento Premium",
            Numero = 101,
            Andar = 5,
            ValorVenda = 250000m
        };

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync(apartamento);
        _apartamentoService.Setup(x => x.Disponivel(apartamentoId))
            .ReturnsAsync(false);

        // Act
        var result = await service.Disponivel(apartamentoId);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Apartamento Premium 101 no andar 5", result.Message);
        Assert.Contains("não disponível", result.Message);
        _apartamentoService.Verify(x => x.Disponivel(apartamentoId), Times.Once);
    }

    [Fact]
    public async Task Disponivel_ShouldThrowNotFoundException_WhenApartamentoNotFound()
    {
        // Arrange
        var service = CreateService();
        var apartamentoId = 999;

        _apartamentoRepository.Setup(x => x.Get(apartamentoId))
            .ReturnsAsync((Apartamento?)null);

        // Act & Assert
        await Assert.ThrowsAsync<DirecionalNotFoundException>(() => service.Disponivel(apartamentoId));
        _apartamentoService.Verify(x => x.Disponivel(It.IsAny<int>()), Times.Never);
    }

    #endregion

    #region Sorting Tests

    [Fact]
    public async Task Read_ShouldApplySortingByAndar_WhenOrderByAndarSpecified()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ApartamentoFilterRequest(),
            Ordenation = new FilterOrderByRequest
            {
                OrderBy = "andar",
                Direction = "asc"
            }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 1, Nome = "Apt 101", Numero = 101, Andar = 1, ValorVenda = 200000m },
            new() { Id = 2, Nome = "Apt 201", Numero = 201, Andar = 2, ValorVenda = 220000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 2);
        _apartamentoRepository.Setup(x => x.Get(
            It.IsAny<List<Expression<Func<Apartamento, bool>>>>(),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            false, // Not descending
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.Data.Count());
        _apartamentoRepository.Verify(x => x.Get(
            It.IsAny<List<Expression<Func<Apartamento, bool>>>>(),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            false,
            1,
            10), Times.Once);
    }

    [Fact]
    public async Task Read_ShouldApplySortingByNumero_WhenOrderByNumeroSpecified()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ApartamentoFilterRequest(),
            Ordenation = new FilterOrderByRequest
            {
                OrderBy = "numero",
                Direction = "desc"
            }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 2, Nome = "Apt 102", Numero = 102, Andar = 1, ValorVenda = 220000m },
            new() { Id = 1, Nome = "Apt 101", Numero = 101, Andar = 1, ValorVenda = 200000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 2);
        _apartamentoRepository.Setup(x => x.Get(
            It.IsAny<List<Expression<Func<Apartamento, bool>>>>(),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            true, // Descending
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.Data.Count());
        _apartamentoRepository.Verify(x => x.Get(
            It.IsAny<List<Expression<Func<Apartamento, bool>>>>(),
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            true,
            1,
            10), Times.Once);
    }

    #endregion

    #region Integration Scenarios

    [Fact]
    public async Task Read_ShouldHandleComplexFilteringAndSorting()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ApartamentoFilterRequest>
        {
            Page = 1,
            PageSize = 5,
            Filter = new ApartamentoFilterRequest
            {
                Nome = "Premium",
                Andar = 5,
                ValorVendaInicio = 300000m
            },
            Ordenation = new FilterOrderByRequest
            {
                OrderBy = "numero",
                Direction = "asc"
            }
        };

        var apartamentos = new List<Apartamento>
        {
            new() { Id = 1, Nome = "Premium Suite A", Numero = 501, Andar = 5, ValorVenda = 350000m },
            new() { Id = 2, Nome = "Premium Suite B", Numero = 502, Andar = 5, ValorVenda = 370000m }
        };

        var repositoryResult = new Tuple<IEnumerable<Apartamento>, int>(apartamentos, 2);
        _apartamentoRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Apartamento, bool>>>>(exprs => exprs.Count == 3), // Three filters
            It.IsAny<Expression<Func<Apartamento, object>>>(),
            false, // Ascending
            1,
            5))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, apt =>
        {
            Assert.Contains("Premium", apt.Nome);
            Assert.Equal(5, apt.Andar);
            Assert.True(apt.ValorVenda >= 300000m);
        });
    }

    #endregion
}
