namespace Direcional.Domain.Tests;

public class UsuarioTests
{
    [Fact]
    public void Usuario_Password_Valido()
    {
        var senha = "SenhaForte123";
        var usuario = new Usuario();
        usuario.DefinirSenha(senha);
        Assert.True(usuario.ValidarSenha(senha));
    }

    [Fact]
    public void Usuario_Password_Invalido()
    {
        var senha = "SenhaForte123";
        var usuario = new Usuario();
        usuario.DefinirSenha(senha);
        Assert.False(usuario.ValidarSenha($"{senha}456"));
    }
}
