using System.Reflection;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.QueryStrings;
using JsonApiDotNetCore.Resources;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace JsonApiDotNetCore.Middleware;

public sealed class JsonApiOutputFormatterDecorator : IJsonApiOutputFormatter
{
    private readonly IJsonApiOutputFormatter _jsonApiOutputFormatter;

    public JsonApiOutputFormatterDecorator(IJsonApiOutputFormatter jsonApiOutputFormatter)
    {
        _jsonApiOutputFormatter = jsonApiOutputFormatter ?? throw new NotImplementedException(nameof(jsonApiOutputFormatter));
    }

    /// <inheritdoc />
    public bool CanWriteResult(OutputFormatterCanWriteContext context)
    {
        return context == null ? throw new ArgumentException(nameof(context)) : _jsonApiOutputFormatter.CanWriteResult(context);
    }

    /// <inheritdoc />
    public async Task WriteAsync(OutputFormatterWriteContext context)
    {
        if (context == null)
        {
            throw new ArgumentException(nameof(context));
        }

        if (context.Object != null)
        {
            var jsonApiRequest = context.HttpContext.RequestServices.GetRequiredService<IJsonApiRequest>();
            var resourceGraph = context.HttpContext.RequestServices.GetRequiredService<IResourceGraph>();
            var queryStringReader = context.HttpContext.RequestServices.GetRequiredService<IQueryStringReader>();
            var queryLayerComposer = context.HttpContext.RequestServices.GetRequiredService<IQueryLayerComposer>();

            var type = context.Object is IEnumerable<IIdentifiable> resources ? resources.GetType().GetGenericArguments().Single() : context.Object.GetType();

            if (jsonApiRequest.PrimaryResourceType!.ClrType != type)
            {
                ((JsonApiRequest)jsonApiRequest).PrimaryResourceType = resourceGraph.GetResourceType(type);

                var disableQueryStringAttribute = GetType().GetCustomAttribute<DisableQueryStringAttribute>(true);
                queryStringReader.ReadAll(disableQueryStringAttribute);
                queryLayerComposer.ComposeFromConstraints(jsonApiRequest.PrimaryResourceType!);
            }
        }

        await _jsonApiOutputFormatter.WriteAsync(context);
    }
}
