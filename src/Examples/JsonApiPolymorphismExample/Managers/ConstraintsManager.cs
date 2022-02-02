using JsonApiDotNetCore.QueryStrings;
using JsonApiPolymorphismExample.Managers.Contracts;

namespace JsonApiPolymorphismExample.Managers
{
    public sealed class ConstraintsManager : IConstraintsManager
    {
        private readonly ISparseFieldSetQueryStringParameterReader _sparseFieldSetQueryStringParameterReader;
        private readonly IIncludeQueryStringParameterReader _includeQueryStringParameterReader;
        private readonly ISortQueryStringParameterReader _sortQueryStringParameterReader;
        private readonly IPaginationQueryStringParameterReader _paginationQueryStringParameterReader;

        public ConstraintsManager(ISparseFieldSetQueryStringParameterReader sparseFieldSetQueryStringParameterReader
            , IIncludeQueryStringParameterReader includeQueryStringParameterReader
            , ISortQueryStringParameterReader sortQueryStringParameterReader
            , IPaginationQueryStringParameterReader paginationQueryStringParameterReader)
        {
            _sparseFieldSetQueryStringParameterReader = sparseFieldSetQueryStringParameterReader ?? throw new NotImplementedException(nameof(sparseFieldSetQueryStringParameterReader));
            _includeQueryStringParameterReader = includeQueryStringParameterReader ?? throw new NotImplementedException(nameof(includeQueryStringParameterReader));
            _sortQueryStringParameterReader = sortQueryStringParameterReader ?? throw new NotImplementedException(nameof(sortQueryStringParameterReader));
            _paginationQueryStringParameterReader = paginationQueryStringParameterReader ?? throw new NotImplementedException(nameof(paginationQueryStringParameterReader));
        }

        public ConstraintFilters Build()
        {
            return new ConstraintFilters();
        }
    }
}
