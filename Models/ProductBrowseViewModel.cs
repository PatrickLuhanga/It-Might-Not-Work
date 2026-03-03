namespace RePurpose.Models
{
    public class ProductBrowseViewModel
    {
        // Search and Filter Properties
        public string CurrentSearchQuery { get; set; } = string.Empty;
        public List<int> SelectedCategories { get; set; } = new List<int>();
        public List<int> SelectedBrands { get; set; } = new List<int>();
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SelectedSpecification { get; set; } = string.Empty;
        public string SpecificationValue { get; set; } = string.Empty;
        public bool InStockOnly { get; set; } = false;
        public string SortBy { get; set; } = "relevance";

        // Filter Options
        public IEnumerable<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public IEnumerable<BrandModel> Brands { get; set; } = new List<BrandModel>();
        public Dictionary<string, string> AvailableSpecifications { get; set; } = new Dictionary<string, string>();

        // Products and Pagination
        public IEnumerable<ProductCardModel> Products { get; set; } = new List<ProductCardModel>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalProducts { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    }

    // Supporting Models
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int StockCount { get; set; }

        // Additional properties for technical products
        public int CategoryId { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public bool IsFeatured { get; set; } = false;
        public bool IsNewArrival { get; set; } = false;
        public Dictionary<string, string> TechnicalSpecs { get; set; } = new Dictionary<string, string>();
    }

    // Technical Specification Model (for detailed product views)
    public class TechnicalSpecificationModel
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
}
