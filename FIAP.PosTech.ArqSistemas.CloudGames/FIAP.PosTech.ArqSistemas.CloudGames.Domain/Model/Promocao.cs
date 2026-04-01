namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model
{
    public class Promocao
    {
        public int Id { get; set; }
        public required string Descricao { get; set; }
        public bool Ativa { get; set; }
    }
}
