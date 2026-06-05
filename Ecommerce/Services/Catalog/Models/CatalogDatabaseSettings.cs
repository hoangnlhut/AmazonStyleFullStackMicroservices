namespace Catalog.Models
{
    public class CatalogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProductCollectionName { get; set; } = null!;
        public string BrandCollectionName { get; set; } = null!;
        public string TypeCollectionName { get; set; } = null!;
    }
}
