using JsonApiPolymorphismExample.Models;
using JsonApiPolymorphismExample.Services.Contracts;

namespace JsonApiPolymorphismExample.Services;

public sealed class ArticleService : IService<ArticleBase>
{
    public Task<IReadOnlyCollection<ArticleBase>> GetAsync(CancellationToken cancellationToken)
    {
        Guid id1Article = Guid.Parse("2C0D5741-459D-4039-AEF7-21585DA8D459");
        Guid id1Author = Guid.Parse("2C0D5741-459D-4039-AEF7-21585DA8D460");
        Guid id1Category = Guid.Parse("2C0D5741-459D-4039-AEF7-21585DA8D461");

        Guid id2Article = Guid.Parse("E8B3F3F4-1A5E-4CE7-87D8-C1097F50866C");
        Guid id2Author = Guid.Parse("E8B3F3F4-1A5E-4CE7-87D8-C1097F50867C");
        Guid id2Category = Guid.Parse("E8B3F3F4-1A5E-4CE7-87D8-C1097F50868C");
        Guid id2AuthorAddress = Guid.Parse("E8B3F3F4-1A5E-4CE7-87D8-C1097F50869C");

        // Build first article
        var author1 = new RegionalAuthor
        {
            Id = id1Author,
            LocalId = id1Author.ToString(),
            StringId = id1Author.ToString(),
            Firstname = "FirstnameProperty1",
            Name = "NameProperty1",
            OriginalRegion = "TestOriginalRegionProperty"
        };

        var category1 = new SportCategory
        {
            Id = id1Category,
            LocalId = id1Category.ToString(),
            StringId = id1Category.ToString(),
            Name = "TestCategory1",
            SportCategoryName = "TestSportCategory1"
        };

        var article1 = new SportArticle()
        {
            Id = id1Article,
            LocalId = id1Article.ToString(),
            StringId = id1Article.ToString(),
            Test = "TestProperty1",
            Title = "TitleProperty1",
            TypeArticle = "TestType1",
            Authors = new List<AuthorBase> { author1 },
            Category = category1
        };

        // Build second article
        var author2 = new InternationalAuthor
        {
            Id = id2Author,
            LocalId = id2Author.ToString(),
            StringId = id2Author.ToString(),
            Firstname = "FirstnameProperty2",
            Name = "NameProperty2",
            OriginalCountry = "OriginalCountryAuthor2",
            Address = new Address
            {
                Id = id2AuthorAddress,
                LocalId = id2AuthorAddress.ToString(),
                StringId = id2AuthorAddress.ToString(),
                Street = "TestStreetPropertyForInternationalAuthor2",
                ZipCode = 1000
            }
        };

        var category2 = new SportCategory
        {
            Id = id2Category,
            LocalId = id2Category.ToString(),
            StringId = id2Category.ToString(),
            Name = "TestCategory2",
            SportCategoryName = "TestSportCategory2"
        };

        var article2 = new SportArticle()
        {
            Id = id2Article,
            LocalId = id2Article.ToString(),
            StringId = id2Article.ToString(),
            Test = "TestProperty2",
            Title = "TitleProperty2",
            TypeArticle = "TestType2",
            Authors = new List<AuthorBase> { author1, author2 },
            Category = category2
        };

        IReadOnlyCollection<ArticleBase> result = new List<SportArticle> { article1, article2 };
        return Task.FromResult(result);
    }
}
