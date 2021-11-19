using System.Collections.Generic;
using JetBrains.Annotations;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiDotNetCoreTests.IntegrationTests.OptimisticConcurrency
{
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public sealed class TextBlock : PostgresVersionedIdentifiable<long>
    {
        [Attr]
        public int ColumnCount { get; set; }

        [HasMany]
        public IList<Paragraph> Paragraphs { get; set; } = new List<Paragraph>();

        [HasMany]
        public IList<WebPage> UsedAt { get; set; } = new List<WebPage>();
    }
}
