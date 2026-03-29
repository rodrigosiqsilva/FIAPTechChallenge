namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface ICorrelationIdGenerator
    {
        string Get();
        void Set(string correlationId);
    }
}