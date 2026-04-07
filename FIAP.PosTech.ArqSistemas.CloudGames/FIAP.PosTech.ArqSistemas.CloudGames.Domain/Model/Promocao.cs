namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class Promocao : ClasseBase
    {
        public required string Descricao { get; set; }
        public bool Ativa { get; set; }
    }
}
