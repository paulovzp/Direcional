namespace Direcional.Infrastructure.Constants;

public class MensagemValidacao
{
    public const string MaxLengthMessage = "O campo {0} deve ter no máximo {1} caracteres.";
    public const string EmailUniqueMessage = "O email informado já está cadastrado.";
    public const string IdZerado = "Id não informado";

    public class Cliente
    {
        public const string EmailRequired = "Email é obrigatório";
        public const string NomeRequired = "Nome é obrigatório";
        public const string TelefoneRequired = "Telefone é obrigatório";
    }
}
