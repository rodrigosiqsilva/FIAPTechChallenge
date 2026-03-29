using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra
{
    public class BaseLogger<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly ICorrelationIdGenerator _correlationIdGenerator;

        public BaseLogger(ILogger<T> logger, ICorrelationIdGenerator correlationIdGenerator)
        {
            _logger = logger;
            _correlationIdGenerator = correlationIdGenerator;
        }

        public virtual void LogInformation(string message)
        {
            _logger.LogInformation($"LogInformation => [CorrelationId: {_correlationIdGenerator.Get()}] {message}");
        }

        public virtual void LogError(string message)
        {
            _logger.LogInformation($"LogError => [CorrelationId: {_correlationIdGenerator.Get()}] {message}");
        }

        public virtual void LogWarning(string message)
        {
            _logger.LogInformation($"LogWarning => [CorrelationId: {_correlationIdGenerator.Get()}] {message}");
        }

        public virtual void LogDebug(string message)
        {
            _logger.LogInformation($"LogDebug => [CorrelationId: {_correlationIdGenerator.Get()}] {message}");
        }

        public virtual void LogTrace(string message)
        {
            _logger.LogInformation($"LogTrace => [CorrelationId: {_correlationIdGenerator.Get()}] {message}");
        }
    }
}
