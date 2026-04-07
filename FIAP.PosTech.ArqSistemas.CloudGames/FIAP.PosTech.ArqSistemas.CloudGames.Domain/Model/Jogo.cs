namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class Jogo : ClasseBase
    {
        public required string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
