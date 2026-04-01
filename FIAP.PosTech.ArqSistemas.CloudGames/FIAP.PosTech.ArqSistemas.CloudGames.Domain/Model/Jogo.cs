namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class Jogo
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
