namespace TemoraColetaETT.Domain.Entities;
using TemoraColetaETT.Domain.Entities;

public abstract class Pessoa
{
    public Guid Id { get; init; }
    public string? NomeCompleto { get; private set; }
    public DateTime? DataNascimento { get; private set; }
    public string? Cpf { get; private set; }
    public string? Rg { get; private set; }
    public string? OrgaoEmissor { get; private set; }
    public string? UfEmissor { get; private set; }
    public Empresa Empresa { get; private set; }

    protected Pessoa(string? nomeCompleto, DateTime? dataNascimento, string? cpf, string? rg, string? orgaoEmissor, string? ufEmissor, Empresa empresa)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new ArgumentException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF é obrigatório.");

        if (dataNascimento.HasValue)
            ValidateDate(dataNascimento.Value);

        Id = Guid.NewGuid();
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        Cpf = cpf;
        Rg = rg;
        OrgaoEmissor = orgaoEmissor;
        UfEmissor = ufEmissor;

        Empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));

        empresa.AdicionarPessoa(this);
    }

    private void ValidateDate(DateTime dataNascimento)
    {
        if (dataNascimento > DateTime.Now)
        {
            throw new ArgumentException("Data de nascimento não pode ser no futuro.");
        }
    }
}