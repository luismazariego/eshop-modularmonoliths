namespace Catalog.Data.Seed;

public static class InitialData
{
    public static readonly Product[] Products = new[]
    {
        Product.Create(
            new Guid("abea18ef-2454-4d2b-9d38-04ebaa2fb3a3"), 
            "IPhone", 
            ["Smart Phones"], 
            "Super cool iPhone 17 Pro Max", 
            "fake/location/image.png", 
            1250.99m),

        Product.Create(
            new Guid("d4f9b2c1-6b4a-4f3e-9a1b-2c3d4e5f6a70"), 
            "Samsung Galaxy S24", 
            ["Smart Phones"], 
            "Flagship Samsung phone with stunning display", 
            "fake/location/galaxy_s24.png", 
            999.99m),
        
        Product.Create(
            new Guid("e2a3f4b5-1c6d-4e7f-8a9b-0c1d2e3f4a56"), 
            "Google Pixel 8", 
            ["Smart Phones"], 
            "Pure Android experience with excellent camera", 
            "fake/location/pixel_8.png", 
            799.00m),
       
        Product.Create(
            new Guid("f1b2c3d4-5e6f-47a8-9b0c-1d2e3f4a5b67"), 
            "OnePlus 12", 
            ["Smart Phones", "Android"], 
            "Fast performance and smooth software", 
            "fake/location/oneplus_12.png", 
            699.50m),
        
        Product.Create(
            new Guid("a7b8c9d0-1234-4e5f-9a8b-7c6d5e4f3a21"), 
            "Sony Xperia 1 V", 
            ["Smart Phones", "Photography"], 
            "High-end phone focused on photography and media", 
            "fake/location/xperia_1v.png", 
            1199.00m)
    };
}
