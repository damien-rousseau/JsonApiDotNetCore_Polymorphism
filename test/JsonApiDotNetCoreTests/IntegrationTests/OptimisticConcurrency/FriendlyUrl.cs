using JetBrains.Annotations;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiDotNetCoreTests.IntegrationTests.OptimisticConcurrency
{
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public sealed class FriendlyUrl : PostgresVersionedIdentifiable<long>
    {
        [Attr]
        public string Uri { get; set; } = null!;

        [HasOne]
        public WebPage? Page { get; set; }
    }
}
