namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public byte Administrador { get; set; }
    }
}
