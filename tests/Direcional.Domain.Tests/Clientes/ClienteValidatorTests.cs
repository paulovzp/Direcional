using Direcional.Infrastructure.Constants;
using FluentValidation;
using Moq;
using System.Linq.Expressions;

namespace Direcional.Domain.Tests;

public class ClienteValidatorTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock = new();

    private IClienteValidator CreateValidator(bool emailExists)
    {

        if (emailExists)
            _clienteRepositoryMock
                .Setup(x => x.Exists(It.IsAny<Expression<Func<Cliente, bool>>>()))
                .ReturnsAsync(true);
        else
            _clienteRepositoryMock
                .Setup(x => x.Exists(It.IsAny<Expression<Func<Cliente, bool>>>()))
                .ReturnsAsync(false);

        return new ClienteValidator(_clienteRepositoryMock.Object);
    }

    [Fact]
    public async Task Cliente_Add_Valido()
    {
        var clienteValidator = CreateValidator(false);
        var cliente = Cliente.Create("Cliente Teste", "cliente@teste.com", "1234567890");
        var result = await clienteValidator.ValidateAsync(cliente, op => op.IncludeRuleSets(ValidationRules.CreateRule));
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Cliente_Add_Email_Ja_Existe()
    {
        var clienteValidator = CreateValidator(true);
        var cliente = Cliente.Create("Cliente Teste", "cliente@teste.com", "1234567890");
        var result = await clienteValidator.ValidateAsync(cliente, op => op.IncludeRuleSets(ValidationRules.CreateRule));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == MensagemValidacao.EmailUniqueMessage);
    }

    [Fact]
    public async Task Cliente_Add_Email_Vazio()
    {
        var clienteValidator = CreateValidator(true);
        var cliente = Cliente.Create("Cliente Teste", "", "1234567890");
        var result = await clienteValidator.ValidateAsync(cliente, op => op.IncludeRuleSets(ValidationRules.CreateRule));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == MensagemValidacao.Cliente.EmailRequired);
    }

    [Fact]
    public async Task Cliente_Add_Email_Null()
    {
        var clienteValidator = CreateValidator(true);
        var cliente = Cliente.Create("Cliente Teste", null, "1234567890");
        var result = await clienteValidator.ValidateAsync(cliente, op => op.IncludeRuleSets(ValidationRules.CreateRule));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == MensagemValidacao.Cliente.EmailRequired);
    }

    [Fact]
    public async Task Cliente_Add_Email_Invalido()
    {
        var clienteValidator = CreateValidator(true);
        var cliente = Cliente.Create("Cliente Teste", "emailinvalido", "1234567890");
        var result = await clienteValidator.ValidateAsync(cliente, op => op.IncludeRuleSets(ValidationRules.CreateRule));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == MensagemValidacao.EmailInvalid);
    }
}
