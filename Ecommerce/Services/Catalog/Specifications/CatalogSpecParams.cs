namespace Catalog.Specifications
{
    //filtering container for the catalog items, it will be used in the specification pattern to filter the items based on the parameters provided by the user
    public class CatalogSpecParams
    {
        private const int MAX_PAGE_SIZE = 70;
        private int _pageSize = 10;

        public int Pageindex { get; set; } = 1;
        public int PageSize { 
            get => _pageSize; 
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
    }
}
