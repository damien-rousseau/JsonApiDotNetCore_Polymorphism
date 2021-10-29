using System.Text.Json;
using FluentAssertions.Execution;

namespace TestBuildingBlocks
{
    // TODO: Make this a nested type (see HttpResponseMessageExtensions for example)
    public sealed class JsonElementAssertions : JsonElementAssertions<JsonElementAssertions>
    {
        internal JsonElementAssertions(JsonElement subject)
            : base(subject)
        {
        }
    }

    public class JsonElementAssertions<TAssertions>
        where TAssertions : JsonElementAssertions<TAssertions>
    {
        private readonly JsonElement _subject;

        protected JsonElementAssertions(JsonElement subject)
        {
            _subject = subject;
        }

        public void ContainProperty(string propertyName)
        {
            string escapedJson = _subject.ToString()?.Replace("{", "{{").Replace("}", "}}");

            Execute.Assertion.ForCondition(_subject.TryGetProperty(propertyName, out _))
                .FailWith($"Expected JSON element '{escapedJson}' to contain a property named '{propertyName}'.");
        }
    }
}
