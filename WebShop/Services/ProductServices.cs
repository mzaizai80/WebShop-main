using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;

public class ProductService
{
    private readonly IFileReader _fileReader;
    //private readonly string _productsFilePath;
    //private readonly string _categoriesFilePath;
    //private readonly string _productCategoryFilePath;

    //private readonly string _productsFilePath = "data/products.json";
    //private readonly string _categoriesFilePath = "data/categories.json";
    //private readonly string _productCategoryFilePath = "data/productCategoryRelation.json";

    private readonly string _productsFilePath;
    private readonly string _categoriesFilePath;
    private readonly string _productCategoryFilePath;

    public ProductService(IFileReader fileReader, IOptions<ProductServiceOptions> options)
    {
        _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        _productsFilePath = options.Value.ProductsFilePath ?? throw new ArgumentNullException(nameof(options.Value.ProductsFilePath));
        _categoriesFilePath = options.Value.CategoriesFilePath ?? throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
        _productCategoryFilePath = options.Value.ProductCategoryFilePath ?? throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
    }


    public List<Product> GetAllProducts()
    {
        var productsJson = _fileReader.ReadAllText(_productsFilePath);
        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson) ?? new List<Product>();

        var categoriesJson = _fileReader.ReadAllText(_categoriesFilePath);
        var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();

        var productCategoryRelationJson = _fileReader.ReadAllText(_productCategoryFilePath);
        var productCategoryRelation = JsonConvert.DeserializeObject<List<ProductCategoryRelation>>(productCategoryRelationJson) ?? new List<ProductCategoryRelation>();

        ProcessProductCategories(products, categories, productCategoryRelation);
        Console.WriteLine( "00000000000000000000000000000000000000000");
        Console.WriteLine($"Result in public List<Product> GetAllProducts()");
        Console.WriteLine($"Number of products: {products.Count}");
        Console.WriteLine( "00000000000000000000000000000000000000000");
        return products;
    }

    private static void ProcessProductCategories(List<Product> products, List<Category> categories, List<ProductCategoryRelation> productCategoryRelation)
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Result in  private static void ProcessProductCategories(List<Product> products, List<Category> categories, List<ProductCategoryRelation> productCategoryRelation)");
        Console.WriteLine($"Number of products: {products.Count}");
        Console.WriteLine($"Number of categories: {categories.Count}");
        Console.WriteLine($"Number of product-category relations: {productCategoryRelation.Count}");
        Console.WriteLine("-------------------------------------------");

        foreach (var product in products)
        {
            var productCategoryRelationIds = productCategoryRelation
                .Where(pc => pc.ProductId == product.Id)
                .Select(pc => pc.CategoryId)
                .ToList();

            var productCategoriesList = categories.Where(c => productCategoryRelationIds.Contains(c.Id)).ToList();
            product.Categories = productCategoriesList;

            // Add debugging statements
            Console.WriteLine($"Product ID: {product.Id}");
            Console.WriteLine($"Associated Categories: {string.Join(", ", product.Categories.Select(cat => cat.Name))}");
        }
    }

/*
    private static void ProcessProductCategories(List<Product> products, List<Category> categories, List<ProductCategoryRelation> productCategoryRelation)
    {
        Console.WriteLine( "-------------------------------------------");
        Console.WriteLine($"Result in  private static void ProcessProductCategories(List<Product> products, List<Category> categories, List<ProductCategoryRelation> productCategoryRelation)");
        Console.WriteLine($"Number of products: {products.Count}");
        Console.WriteLine($"Number of categories: {categories.Count}");
        Console.WriteLine($"Number of categories: {productCategoryRelation.Count}");
        Console.WriteLine( "-------------------------------------------");
        foreach (var product in products)
        {
            var productCategoryRelationIds = productCategoryRelation
                .Where(pc => pc.ProductId == product.Id)
                .Select(pc => pc.CategoryId)
                .ToList();

            var productCategoriesList = categories.Where(c => productCategoryRelationIds.Contains(c.Id)).ToList();
            product.Categories = productCategoriesList;
        }
    }
*/

    public List<Category> GetAllCategories()
    {
        var categoriesJson = _fileReader.ReadAllText(_categoriesFilePath);
        var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();
        Console.WriteLine( "******************************************");
        Console.WriteLine($"Result in public List<Category> GetAllCategories()");
        Console.WriteLine($"Number of categories: {categories.Count}");
        Console.WriteLine( "******************************************");
        return categories;
    }

}


