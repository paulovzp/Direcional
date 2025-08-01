namespace Direcional.Infrastructure.Constants;

public class MensagemValidacao
{
    public const string MaxLengthMessage = "O campo {0} deve ter no máximo {1} caracteres.";
    public const string EmailUniqueMessage = "O email informado já está cadastrado.";
    public const string EmailInvalid = "não é um email valido";
    public const string IdZerado = "Id não informado";

    public class Venda
    {
        public const string ValorEntradaMenorZero = "Valor de entrada deve ser maior que zero.";
    }

    public class Cliente
    {
        public const string NotFound = "Cliente não foi encontrado";
        public const string EmailRequired = "Email é obrigatório";
        public const string NomeRequired = "Nome é obrigatório";
        public const string TelefoneRequired = "Telefone é obrigatório";
        public const string NaoPodeSerExcluido = "Cliente com movimentação no sistema, não pode ser excluido";
    }

    public class Corretor
    {
        public const string NotFound = "Corretor não foi encontrado";
        public const string EmailRequired = "Email é obrigatório";
        public const string NomeRequired = "Nome é obrigatório";
        public const string TelefoneRequired = "Telefone é obrigatório";
        public const string NaoPodeSerExcluido = "Corretor com movimentação no sistema, não pode ser excluido";
    }

    public class Apartamento
    {
        public const string NotFound = "Apartamento não foi encontrado";
        public const string AndarNumeroUnique = "Apartamento para Número e Andar informado já cadastro no sistema";
        public const string NomeRequired = "Nome é obrigatório";
        public const string NumeroRequired = "Número deve ser maior do que zero";
        public const string AndarRequired = "Andar deve ser maior ou igual a zero";
        public const string ValorVendaRequired = "Valor de venda é obrigatório";
        public const string ValorVendaInvalid = "Valor de venda deve ser maior que zero";
        public const string NaoPodeSerExcluido = "Apartamento com movimentação no sistema, não pode ser excluído";
    }

    public class Reserva
    {
        public const string Reservado = "Apartamento já reservado";
        public const string Vendido = "Apartamento já vendido";
        public const string ApartamentoRequired = "Apartamento é obrigatório";
        public const string CorretorRequired = "Corretor é obrigatório";
        public const string ClienteRequired = "Cliente é obrigatório";
    }
}
