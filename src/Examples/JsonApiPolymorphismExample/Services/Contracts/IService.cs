namespace JsonApiPolymorphismExample.Services.Contracts;

public interface IService<T>
{
    Task<IReadOnlyCollection<T>> GetAsync(CancellationToken cancellationToken);
}
