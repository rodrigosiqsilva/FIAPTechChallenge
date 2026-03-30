namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class PessoaFisica(int id, string nome, string email, string senha, byte adminitrador)
    {
        public int Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public string Email { get; set; } = email;
        public string Senha { get; set; } = senha;
        public byte Administrador { get; set; } = adminitrador;
    }
}
