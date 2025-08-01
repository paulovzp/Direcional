using Direcional.Application.Common;
using Direcional.Domain;
using Moq;
using System.Linq.Expressions;

namespace Direcional.Application.Tests;

public class ClienteAppServiceTests
{
    private readonly Mock<IClienteService> _clienteService = new();
    private readonly Mock<IClienteRepository> _clienteRepository = new();
    private readonly Mock<IDirecionalUnitOfWork> _unitOfWork = new();

    private IClienteAppService CreateService() => new ClienteAppService(_clienteService.Object, _clienteRepository.Object, _unitOfWork.Object);

    [Fact]
    public async Task Read_ShouldReturnEmptyList_WhenNoDataExists()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest()
        };

        var emptyResult = new Tuple<IEnumerable<Cliente>, int>(new List<Cliente>(), 0);
        _clienteRepository.Setup(x => x.Get(
            It.IsAny<List<Expression<Func<Cliente, bool>>>>(),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<int>(),
            It.IsAny<int>()))
            .ReturnsAsync(emptyResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByNome()
    {
        // Arrange
        var service = CreateService();
        var nomeFilter = "João";
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest { Nome = nomeFilter }
        };

        var clientes = new List<Cliente>
        {
            Cliente.Create("João Silva", "joao@test.com", "123456789"),
            Cliente.Create("João Santos", "joao.santos@test.com", "987654321")
        };

        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 2);
        _clienteRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Cliente, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.NotEmpty(result.Data);
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, cliente => Assert.Contains(nomeFilter, cliente.Nome));
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByEmail()
    {
        // Arrange
        var service = CreateService();
        var emailFilter = "test@email.com";
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest { Email = emailFilter }
        };

        var clientes = new List<Cliente>
        {
            Cliente.Create("Cliente Teste", emailFilter, "123456789")
        };

        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 1);
        _clienteRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Cliente, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(emailFilter, result.Data.First().Email);
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterByTelefone()
    {
        // Arrange
        var service = CreateService();
        var telefoneFilter = "123456789";
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest { Telefone = telefoneFilter }
        };

        var clientes = new List<Cliente>
        {
            Cliente.Create("Cliente Teste", "test@email.com", telefoneFilter)
        };

        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 1);
        _clienteRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Cliente, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(telefoneFilter, result.Data.First().Telefone);
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenFilterById()
    {
        // Arrange
        var service = CreateService();
        var idFilter = 1;
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest { Id = idFilter }
        };

        var cliente = Cliente.Create("Cliente Teste", "test@email.com", "123456789");
        // Setting Id using reflection since it might be private setter
        typeof(Cliente).GetProperty("Id")?.SetValue(cliente, idFilter);

        var clientes = new List<Cliente> { cliente };
        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 1);

        _clienteRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Cliente, bool>>>>(exprs => exprs.Count == 1),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(idFilter, result.Data.First().Id);
    }

    [Fact]
    public async Task Read_ShouldReturnFilteredData_WhenMultipleFiltersApplied()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest
            {
                Nome = "João",
                Email = "joao@test.com"
            }
        };

        var clientes = new List<Cliente>
        {
            Cliente.Create("João Silva", "joao@test.com", "123456789")
        };

        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 1);
        _clienteRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Cliente, bool>>>>(exprs => exprs.Count == 2), // Two filters applied
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.TotalCount);
        Assert.Contains("João", result.Data.First().Nome);
        Assert.Contains("joao@test.com", result.Data.First().Email);
    }

    [Fact]
    public async Task Read_ShouldReturnEmptyList_WhenFilterIsNull()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = null
        };

        var emptyResult = new Tuple<IEnumerable<Cliente>, int>(new List<Cliente>(), 0);
        _clienteRepository.Setup(x => x.Get(
            It.Is<List<Expression<Func<Cliente, bool>>>>(exprs => exprs.Count == 0), // No filters when null
            It.IsAny<Expression<Func<Cliente, object>>>(),
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

    [Fact]
    public async Task Read_ShouldRespectPagination_WhenRequestingSpecificPage()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 2,
            PageSize = 5,
            Filter = new ClienteFilterRequest()
        };

        var clientes = new List<Cliente>
        {
            Cliente.Create("Cliente 6", "cliente6@test.com", "123456786"),
            Cliente.Create("Cliente 7", "cliente7@test.com", "123456787")
        };

        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 10); // Total of 10 records
        _clienteRepository.Setup(x => x.Get(
            It.IsAny<List<Expression<Func<Cliente, bool>>>>(),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            2, // Page 2
            5)) // PageSize 5
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.Data.Count());
        Assert.Equal(10, result.TotalCount);
        _clienteRepository.Verify(x => x.Get(
            It.IsAny<List<Expression<Func<Cliente, bool>>>>(),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            It.IsAny<bool>(),
            2,
            5), Times.Once);
    }

    [Fact]
    public async Task Read_ShouldApplyCorrectSorting_WhenOrderByIsSpecified()
    {
        // Arrange
        var service = CreateService();
        var request = new FilterRequest<ClienteFilterRequest>
        {
            Page = 1,
            PageSize = 10,
            Filter = new ClienteFilterRequest(),
            Ordenation = new FilterOrderByRequest
            {
                OrderBy = "nome",
                Direction = "asc"
            }
        };

        var clientes = new List<Cliente>
        {
            Cliente.Create("Ana Silva", "ana@test.com", "123456789"),
            Cliente.Create("Bruno Santos", "bruno@test.com", "987654321")
        };

        var repositoryResult = new Tuple<IEnumerable<Cliente>, int>(clientes, 2);
        _clienteRepository.Setup(x => x.Get(
            It.IsAny<List<Expression<Func<Cliente, bool>>>>(),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            false, // Not descending (asc)
            1,
            10))
            .ReturnsAsync(repositoryResult);

        // Act
        var result = await service.Read(request);

        // Assert
        Assert.Equal(2, result.Data.Count());
        _clienteRepository.Verify(x => x.Get(
            It.IsAny<List<Expression<Func<Cliente, bool>>>>(),
            It.IsAny<Expression<Func<Cliente, object>>>(),
            false,
            1,
            10), Times.Once);
    }

}
