using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TestBuildingBlocks;
using Xunit;

namespace OpenApiTests.NamingConvention.KebabCase
{
    public sealed class KebabCaseTests
        : IClassFixture<IntegrationTestContext<KebabCaseNamingConventionStartup<NamingConventionDbContext>, NamingConventionDbContext>>
    {
        private static Lazy<Task<JsonDocument>> _lazyOpenApiDocument;
        private readonly IntegrationTestContext<KebabCaseNamingConventionStartup<NamingConventionDbContext>, NamingConventionDbContext> _testContext;

        public KebabCaseTests(IntegrationTestContext<KebabCaseNamingConventionStartup<NamingConventionDbContext>, NamingConventionDbContext> testContext)
        {
            _testContext = testContext;

            _lazyOpenApiDocument ??= new Lazy<Task<JsonDocument>>(async () =>
            {
                testContext.UseController<SupermarketsController>();

                string content = await GetAsync("swagger/v1/swagger.json");

                await WriteToSwaggerDocumentsFolderAsync(content);

                return JsonDocument.Parse(content);
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        [Fact]
        public async Task Kebab_naming_policy_is_applied_to_get_collection_endpoint()
        {
            // Act
            JsonDocument document = await _lazyOpenApiDocument.Value;

            // Assert
            document.ShouldContainPath("paths./supermarkets.get").With(getElement =>
            {
                getElement.ShouldContainPath("operationId").With(operationElement =>
                {
                    operationElement.ShouldBeString("get-supermarket-collection");
                });

                getElement.ShouldContainPath("responses.200.content['application/vnd.api+json'].schema.$ref")
                    .ShouldBeReferenceSchemaId("supermarket-collection-response-document").With(responseRefId =>
                    {
                        document.ShouldContainPath("components.schemas").With(schemasElement =>
                        {
                            schemasElement.ShouldContainPath($"{responseRefId}.properties").With(propertiesElement =>
                            {
                                propertiesElement.ShouldContainPath("links.$ref").ShouldBeReferenceSchemaId("links-in-resource-collection-document");
                                propertiesElement.ShouldContainPath("jsonapi.$ref").ShouldBeReferenceSchemaId("jsonapi-object");

                                propertiesElement.ShouldContainPath("data.items.$ref").ShouldBeReferenceSchemaId("supermarket-data-in-response").With(
                                    dataRefId =>
                                    {
                                        schemasElement.ShouldContainPath($"{dataRefId}.properties").With(dataPropertiesElement =>
                                        {
                                            dataPropertiesElement.ShouldContainPath("links.$ref").ShouldBeReferenceSchemaId("links-in-resource-object");

                                            dataPropertiesElement.ShouldContainPath("type.$ref").ShouldBeReferenceSchemaId("supermarkets-resource-type").With(
                                                typeRefId =>
                                                {
                                                    JsonElement primaryResourceTypeElement = schemasElement.ShouldContainPath($"{typeRefId}.enum[0]");
                                                    primaryResourceTypeElement.ShouldBeString("supermarkets");
                                                });

                                            dataPropertiesElement.ShouldContainPath("attributes.$ref")
                                                .ShouldBeReferenceSchemaId("supermarket-attributes-in-response").With(attributesRefId =>
                                                {
                                                    schemasElement.ShouldContainPath($"{attributesRefId}.properties").With(attributePropertiesElement =>
                                                    {
                                                        attributePropertiesElement.Should().ContainProperty("name-of-city");
                                                        attributePropertiesElement.Should().ContainProperty("kind");
                                                        attributePropertiesElement.ShouldContainPath("kind.$ref").ShouldBeReferenceSchemaId("supermarket-type");
                                                    });
                                                });

                                            dataPropertiesElement.ShouldContainPath("relationships.$ref")
                                                .ShouldBeReferenceSchemaId("supermarket-relationships-in-response").With(relationshipsRefId =>
                                                {
                                                    schemasElement.ShouldContainPath($"{relationshipsRefId}.properties").With(relationshipPropertiesElement =>
                                                    {
                                                        relationshipPropertiesElement.Should().ContainProperty("store-manager");

                                                        // ...
                                                    });
                                                });
                                        });
                                    });
                            });
                        });
                    });
            });
        }

        [Fact]
        public async Task Kebab_naming_policy_is_applied_to_get_single_endpoint()
        {
            // Arrange
            const string expectedReferenceIdForResponseDocument = "supermarket-primary-response-document";
            const string expectedReferenceIdForTopLevelLinks = "links-in-resource-document";

            const string expectedOperationId = "get-supermarket";
            const string expectedPrimaryResourcePublicName = "supermarkets";
            string expectedPath = $"/{expectedPrimaryResourcePublicName}/{{id}}";

            // Act
            JsonDocument document = await _lazyOpenApiDocument.Value;

            // Assert
            document.SelectTokenOrError("paths").Should().ContainProperty(expectedPath);
            document.SelectTokenOrError($"paths.{expectedPath}.get.tags[0]").GetString().Should().Be(expectedPrimaryResourcePublicName);
            document.SelectTokenOrError($"paths.{expectedPath}.get.operationId").GetString().Should().Be(expectedOperationId);

            string responseRefId = document.SelectTokenOrError($"paths.{expectedPath}.get.responses.200.content['application/vnd.api+json'].schema.$ref")
                .GetReferenceSchemaId();

            responseRefId.Should().Be(expectedReferenceIdForResponseDocument);

            string topLevelLinksRefId = document.SelectTokenOrError($"components.schemas.{responseRefId}.properties.links.$ref").GetReferenceSchemaId();
            topLevelLinksRefId.Should().Be(expectedReferenceIdForTopLevelLinks);
        }

        private async Task<string> GetAsync(string requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            using HttpClient client = _testContext.Factory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        private async Task WriteToSwaggerDocumentsFolderAsync(string content)
        {
            string path = GetSwaggerDocumentPath(nameof(KebabCase));
            await File.WriteAllTextAsync(path, content);
        }

        private string GetSwaggerDocumentPath(string fileName)
        {
            string testDirectoryPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName;
            string namespacePathRelativeToTestDirectory = Path.Join(GetType().Namespace!.Split('.'));

            string[] swaggerDocumentFilePathElements =
            {
                testDirectoryPath,
                namespacePathRelativeToTestDirectory,
                $"{fileName}.json"
            };

            return Path.Join(swaggerDocumentFilePathElements);
        }
    }
}
