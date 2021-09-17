using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using JsonApiDotNetCore.Resources;

namespace JsonApiDotNetCore.OpenApi.JsonApiObjects.ResourceObjects
{
    // ReSharper disable once UnusedTypeParameter
    internal class ResourceIdentifierObject<TResource> : ResourceIdentifierObject
        where TResource : IIdentifiable
    {
    }

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    internal class ResourceIdentifierObject
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Id { get; set; }
    }
}
