#Query example to load resources with dependencies:

https://localhost:7023/v1/articles?include=authors,category,authors.address&fields[sportArticles]=title,typeArticle,authors,category&fields[sportCategories]=sportCategoryName&fields[regionalAuthors]=firstname,originalRegion&fields[internationalAuthors]=firstname,originalCountry,address
