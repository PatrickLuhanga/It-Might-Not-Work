using Microsoft.AspNetCore.Mvc;
using RePurpose.Data;
using RePurpose.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace RePurposeDurban.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: /Products
        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.Select(p => new ProductCardModel
            {
                Id = p.ProductID,
                Name = p.ProductType,
                Brand = "Generic", // Adjust as needed
                CategoryName = "General", // Adjust as needed
                ShortDescription = p.Description,
                Price = 0, // Replace if you have price
                ImageUrl = p.ImagePath,
                StockCount = p.IsWorking ? 10 : 0, // Example logic
                CategoryId = 0,
                PartNumber = "",
                Manufacturer = "",
                IsFeatured = false,
                IsNewArrival = false
            }).ToList();

            var dummyProducts = new List<ProductCardModel>
                {
                    new ProductCardModel { Id = 1, Name = "RAM Stick", Brand = "Lenovo", CategoryName = "Electronics", ShortDescription = "DDR4-3200-NB8G Memory Module 8GB 1 x 8GB DDR4 3200Mhz", Price = 410, ImageUrl = "/images/lenovo ram stick.webp", StockCount = 10, PartNumber = "DDR4-3200-NB8G", Manufacturer = "Lenovo", IsFeatured = true, TechnicalSpecs = new Dictionary<string, string> { { "Memory", "8GB" }, { "Speed", "3200Mhz" } } },
            new ProductCardModel {
                Id = 1,
                Name = "Resistor Pack (1kΩ, 100 pcs)",
                Brand = "Ohmite",
                CategoryName = "Electronics",
                ShortDescription = "A pack of 100 1kΩ resistors for circuit prototyping.",
                Price = 25,
                ImageUrl = "/images/resistor_pack.webp",
                StockCount = 300,
                PartNumber = "RES-1K-100",
                Manufacturer = "Ohmite",
                IsFeatured = true,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Resistance", "1kΩ" },
                    { "Quantity", "100 pcs" }
                }
},
            new ProductCardModel {
                Id = 2,
                Name = "Electrolytic Capacitor (1000µF 25V)",
                Brand = "Nichicon",
                CategoryName = "Electronics",
                ShortDescription = "High-performance capacitor ideal for power filtering and audio circuits.",
                Price = 12.50m,
                ImageUrl = "/images/capacitor.webp",
                StockCount = 150,
                PartNumber = "CAP-1000uF-25V",
                Manufacturer = "Nichicon",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Capacitance", "1000µF" },
                    { "Voltage", "25V" }
                }
},
            new ProductCardModel {
                Id = 3,
                Name = "NE555 Timer IC",
                Brand = "Texas Instruments",
                CategoryName = "Electronics",
                ShortDescription = "Versatile timer chip used for pulse generation and oscillators.",
                Price = 9.99m,
                ImageUrl = "/images/ne555.webp",
                StockCount = 220,
                PartNumber = "NE555P",
                Manufacturer = "Texas Instruments",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Package", "DIP-8" },
                    { "Function", "Timer/IC" }
                }
},
            new ProductCardModel {
                Id = 4,
                Name = "Arduino Uno R3 Microcontroller",
                Brand = "Arduino",
                CategoryName = "Electronics",
                ShortDescription = "Beginner-friendly microcontroller board with USB interface.",
                Price = 250,
                ImageUrl = "/images/arduino_uno.webp",
                StockCount = 60,
                PartNumber = "A000066",
                Manufacturer = "Arduino",
                IsFeatured = true,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Microcontroller", "ATmega328P" },
                    { "Interface", "USB" }
                }
},
            new ProductCardModel {
                Id = 5,
                Name = "OLED Display Module (0.96 inch, I2C)",
                Brand = "Waveshare",
                CategoryName = "Electronics",
                ShortDescription = "Compact monochrome OLED display module for microcontroller projects.",
                Price = 79.99m,
                ImageUrl = "/images/oled_display_module.webp",
                StockCount = 80,
                PartNumber = "OLED096-I2C",
                Manufacturer = "Waveshare",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Resolution", "128x64" },
                    { "Interface", "I2C" }
                }
},
            new ProductCardModel {
                Id = 6,
                Name = "Piezo Buzzer Module",
                Brand = "Keyes",
                CategoryName = "Electronics",
                ShortDescription = "A simple buzzer module for sound notifications in Arduino projects.",
                Price = 15.00m,
                ImageUrl = "/images/piezo_buzzer.webp",
                StockCount = 90,
                PartNumber = "BUZZ-PZ-KY012",
                Manufacturer = "Keyes",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Voltage", "5V" },
                    { "Type", "Piezoelectric" }
                }
},
            new ProductCardModel {
                Id = 7,
                Name = "IR Receiver Module",
                Brand = "Generic",
                CategoryName = "Electronics",
                ShortDescription = "Infrared receiver module compatible with Arduino and remote controls.",
                Price = 22.00m,
                ImageUrl = "/images/ir_receiver.webp",
                StockCount = 130,
                PartNumber = "IR-1838B",
                Manufacturer = "Generic",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Wavelength", "940nm" },
                    { "Voltage", "2.7V–5.5V" }
                }
}                };

            var viewModel = new ProductBrowseViewModel
            {
                Products = dummyProducts,
                TotalProducts = products.Count
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            // Dummy data for the example
            var dummyProducts = new List<ProductCardModel>
            {
            new ProductCardModel { Id = 1, Name = "RAM Stick", Brand = "Lenovo", CategoryName = "Electronics", ShortDescription = "DDR4-3200-NB8G Memory Module 8GB 1 x 8GB DDR4 3200Mhz", Price = 410, ImageUrl = "/images/lenovo ram stick.webp", StockCount = 10, PartNumber = "DDR4-3200-NB8G", Manufacturer = "Lenovo", IsFeatured = true, TechnicalSpecs = new Dictionary<string, string> { { "Memory", "8GB" }, { "Speed", "3200Mhz" } } },
            new ProductCardModel {
                Id = 1,
                Name = "Resistor Pack (1kΩ, 100 pcs)",
                Brand = "Ohmite",
                CategoryName = "Electronics",
                ShortDescription = "A pack of 100 1kΩ resistors for circuit prototyping.",
                Price = 25,
                ImageUrl = "/images/resistor_pack.webp",
                StockCount = 300,
                PartNumber = "RES-1K-100",
                Manufacturer = "Ohmite",
                IsFeatured = true,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Resistance", "1kΩ" },
                    { "Quantity", "100 pcs" }
                }
},
            new ProductCardModel {
                Id = 2,
                Name = "Electrolytic Capacitor (1000µF 25V)",
                Brand = "Nichicon",
                CategoryName = "Electronics",
                ShortDescription = "High-performance capacitor ideal for power filtering and audio circuits.",
                Price = 12.50m,
                ImageUrl = "/images/capacitor.webp",
                StockCount = 150,
                PartNumber = "CAP-1000uF-25V",
                Manufacturer = "Nichicon",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Capacitance", "1000µF" },
                    { "Voltage", "25V" }
                }
},
            new ProductCardModel {
                Id = 3,
                Name = "NE555 Timer IC",
                Brand = "Texas Instruments",
                CategoryName = "Electronics",
                ShortDescription = "Versatile timer chip used for pulse generation and oscillators.",
                Price = 9.99m,
                ImageUrl = "/images/ne555.webp",
                StockCount = 220,
                PartNumber = "NE555P",
                Manufacturer = "Texas Instruments",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Package", "DIP-8" },
                    { "Function", "Timer/IC" }
                }
},
            new ProductCardModel {
                Id = 4,
                Name = "Arduino Uno R3 Microcontroller",
                Brand = "Arduino",
                CategoryName = "Electronics",
                ShortDescription = "Beginner-friendly microcontroller board with USB interface.",
                Price = 250,
                ImageUrl = "/images/arduino_uno.webp",
                StockCount = 60,
                PartNumber = "A000066",
                Manufacturer = "Arduino",
                IsFeatured = true,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Microcontroller", "ATmega328P" },
                    { "Interface", "USB" }
                }
},
            new ProductCardModel {
                Id = 5,
                Name = "OLED Display Module (0.96 inch, I2C)",
                Brand = "Waveshare",
                CategoryName = "Electronics",
                ShortDescription = "Compact monochrome OLED display module for microcontroller projects.",
                Price = 79.99m,
                ImageUrl = "/images/oled_display_module.webp",
                StockCount = 80,
                PartNumber = "OLED096-I2C",
                Manufacturer = "Waveshare",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Resolution", "128x64" },
                    { "Interface", "I2C" }
                }
},
            new ProductCardModel {
                Id = 6,
                Name = "Piezo Buzzer Module",
                Brand = "Keyes",
                CategoryName = "Electronics",
                ShortDescription = "A simple buzzer module for sound notifications in Arduino projects.",
                Price = 15.00m,
                ImageUrl = "/images/piezo_buzzer.webp",
                StockCount = 90,
                PartNumber = "BUZZ-PZ-KY012",
                Manufacturer = "Keyes",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Voltage", "5V" },
                    { "Type", "Piezoelectric" }
                }
},
            new ProductCardModel {
                Id = 7,
                Name = "IR Receiver Module",
                Brand = "Generic",
                CategoryName = "Electronics",
                ShortDescription = "Infrared receiver module compatible with Arduino and remote controls.",
                Price = 22.00m,
                ImageUrl = "/images/ir_receiver.webp",
                StockCount = 130,
                PartNumber = "IR-1838B",
                Manufacturer = "Generic",
                IsFeatured = false,
                TechnicalSpecs = new Dictionary<string, string> {
                    { "Wavelength", "940nm" },
                    { "Voltage", "2.7V–5.5V" }
                }
}

            // ... include the other dummy products from before with some extra specs
            };

            var product = dummyProducts.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductType,Description,IsWorking")] Product product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "images", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    product.ImagePath = "/images/" + fileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
