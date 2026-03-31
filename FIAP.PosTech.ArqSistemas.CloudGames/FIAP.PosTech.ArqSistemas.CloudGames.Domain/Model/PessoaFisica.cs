namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public byte Administrador { get; set; }
    }
}
