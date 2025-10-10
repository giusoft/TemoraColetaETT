using TemoraColetaETT.Domain.Entities;

namespace TemoraColetaETT.Domain.Entities
{
    public class Usuario : Pessoa
    {
        public string Senha { get; private set; }

        public Usuario(string? nomeCompleto, DateTime? dataNascimento, string? cpf, string? rg, string? orgaoEmissor, string? ufEmissor, string senha, Empresa empresa)
            : base(nomeCompleto, dataNascimento, cpf, rg, orgaoEmissor, ufEmissor, empresa)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha obrigatória.");

            Senha = senha;
        }
    }
}