using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiDotNetCoreTests.IntegrationTests.OptimisticConcurrency
{
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public sealed class DeploymentJob : Identifiable<Guid>
    {
        [Attr]
        [Required]
        public DateTimeOffset? StartedAt { get; set; }

        [HasOne]
        public DeploymentJob? ParentJob { get; set; }

        [HasMany]
        public IList<DeploymentJob> ChildJobs { get; set; } = new List<DeploymentJob>();
    }
}
