using TemoraColetaETT.Domain.Entities;

namespace TemoraColetaETT.Domain.Entities;

public class Empresa
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Cnpj { get; private set; }

    private readonly List<Pessoa> _pessoas = new();
    public IReadOnlyCollection<Pessoa> Pessoas => _pessoas.AsReadOnly();

    public Empresa(string nome, string cnpj)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da empresa é obrigatório.");
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("Cnpg da empresa é obrigatório.");

        Id = Guid.NewGuid();
        Cnpj = cnpj;
        Nome = nome;
    }

    public void AdicionarPessoa(Pessoa pessoa)
    {
        if (pessoa == null) throw new ArgumentNullException(nameof(pessoa));

        _pessoas.Add(pessoa);
    }
}