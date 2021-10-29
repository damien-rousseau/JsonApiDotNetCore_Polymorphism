using System;
using System.Text.Json;
using BlushingPenguin.JsonPath;
using FluentAssertions;

namespace TestBuildingBlocks
{
    // TODO: Rename after elimination of JsonDocument usage.
    public static class JsonDocumentExtensions
    {
        public static JsonElement SelectTokenOrError(this JsonDocument source, string path)
        {
            return source.SelectToken(path, true)!.Value;
        }

        public static JsonElement ShouldContainPath(this JsonDocument source, string path)
        {
            return source.SelectToken(path, true)!.Value;
        }

        public static JsonElement ShouldContainPath(this JsonElement source, string path)
        {
            return source.SelectToken(path, true)!.Value;
        }

        public static string ShouldBeReferenceSchemaId(this JsonElement source, string value)
        {
            string referenceSchemaId = source.GetReferenceSchemaId();
            referenceSchemaId.Should().Be(value);

            return referenceSchemaId;
        }

        public static void ShouldBeString(this JsonElement source, string value)
        {
            source.ValueKind.Should().Be(JsonValueKind.String);
            source.GetString().Should().Be(value);
        }

        public static void With<T>(this T source, Action<T> action)
        {
            action(source);
        }
    }
}
