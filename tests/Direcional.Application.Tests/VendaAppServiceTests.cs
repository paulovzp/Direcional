using Direcional.Domain;
using Direcional.Infrastructure.Exceptions;
using Moq;

namespace Direcional.Application.Tests;

public class VendaAppServiceTests
{
    [Fact]
    public async Task Efetuar_Should_Call_VendaService_And_Commit_When_Reserva_Exists()
    {
        var reservaId = 1;
        var valorEntrada = 1000m;
        var reserva = new Reserva();
        var request = new PagamentoReservaRequest { ValorEntrada = valorEntrada };

        var vendaServiceMock = new Mock<IVendaService>();
        var vendaRepositoryMock = new Mock<IVendaRepository>();
        var unitOfWorkMock = new Mock<IDirecionalUnitOfWork>();
        var reservaRepositoryMock = new Mock<IReservaRepository>();

        reservaRepositoryMock.Setup(r => r.Get(reservaId)).ReturnsAsync(reserva);
        vendaServiceMock.Setup(v => v.Efetuar(reserva, valorEntrada)).Returns(Task.CompletedTask);
        unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(true);

        var appService = new VendaAppService(
            vendaServiceMock.Object,
            vendaRepositoryMock.Object,
            unitOfWorkMock.Object,
            reservaRepositoryMock.Object
        );

        await appService.Efetuar(reservaId, request);

        reservaRepositoryMock.Verify(r => r.Get(reservaId), Times.Once);
        vendaServiceMock.Verify(v => v.Efetuar(reserva, valorEntrada), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Efetuar_Should_Throw_NotFound_When_Reserva_Does_Not_Exist()
    {
        var reservaId = 1;
        var request = new PagamentoReservaRequest { ValorEntrada = 1000m };

        var vendaServiceMock = new Mock<IVendaService>();
        var vendaRepositoryMock = new Mock<IVendaRepository>();
        var unitOfWorkMock = new Mock<IDirecionalUnitOfWork>();
        var reservaRepositoryMock = new Mock<IReservaRepository>();

        reservaRepositoryMock.Setup(r => r.Get(reservaId)).ReturnsAsync((Reserva)null);

        var appService = new VendaAppService(
            vendaServiceMock.Object,
            vendaRepositoryMock.Object,
            unitOfWorkMock.Object,
            reservaRepositoryMock.Object
        );

        await Assert.ThrowsAsync<DirecionalNotFoundException>(() => appService.Efetuar(reservaId, request));
    }
}
