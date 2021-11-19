using System.Collections.Generic;
using JetBrains.Annotations;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiDotNetCoreTests.IntegrationTests.OptimisticConcurrency
{
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public sealed class WebImage : PostgresVersionedIdentifiable<long>
    {
        [Attr]
        public string? Description { get; set; }

        [Attr]
        public string Path { get; set; } = null!;

        [HasMany]
        public IList<Paragraph> UsedIn { get; set; } = new List<Paragraph>();
    }
}
