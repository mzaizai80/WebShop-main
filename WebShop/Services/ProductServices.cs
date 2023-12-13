﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;

public class ProductService : IProductService, IProductCategoryService
{
    private readonly IFileReader _fileReader;
    private readonly string _productsFilePath;
    private readonly string _categoriesFilePath;
    private readonly string _productCategoryFilePath;
    private readonly IFileWriter _fileWriter;

    public ProductService()
    {
        // Default constructor implementation for Moq Tests only
    }

    public ProductService(IFileReader fileReader, IFileWriter fileWriter, IOptions<ProductServiceOptions> options)
    {
        _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        _productsFilePath = options.Value.ProductsFilePath ??
                            throw new ArgumentNullException(nameof(options.Value.ProductsFilePath));
        _categoriesFilePath = options.Value.CategoriesFilePath ??
                              throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
        _productCategoryFilePath = options.Value.ProductCategoryFilePath ??
                                   throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
    }


    public void AddProduct(Product product)
    {
        var products = GetAllProducts();
        products.Add(product);
        SaveProducts(products);
    }

    public void UpdateProduct(Product updatedProduct)
    {
        var products = GetAllProducts();
        var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

        if (existingProduct != null)
        {
            existingProduct.Name = updatedProduct.Name;
            existingProduct.PictureUrl = updatedProduct.PictureUrl;

            SaveProducts(products);
        }
    }

    public void DeleteProduct(int productId)
    {
        var products = GetAllProducts();
        var productToRemove = products.FirstOrDefault(p => p.Id == productId);

        if (productToRemove != null)
        {
            products.Remove(productToRemove);
            SaveProducts(products);
        }
    }


    public List<Product> GetAllProducts()
    {
        var productsJson = _fileReader.ReadAllText(_productsFilePath);
        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson) ?? new List<Product>();
        return products;
    }

    private void SaveProducts(List<Product> products)
    {
        var productsJson = JsonConvert.SerializeObject(products);
        _fileWriter.WriteAllText(_productsFilePath, productsJson);
    }



    public Dictionary<Product, List<Category>> GetProductCategoryAssociations()
    {
        var products = GetAllProducts();
        var categories = ICategoryService.GetAllCategories();
        var productCategoryRelation = IProductCategoryService.GetAllProductCategoryRelation();

        var productCategoryMap = new Dictionary<Product, List<Category>>();

        foreach (var relation in productCategoryRelation)
        {
            var product = products.FirstOrDefault(p => p.Id == relation.ProductId);
            var category = FindCategory(categories, relation.CategoryId);

            if (product != null && category != null)
            {
                if (!productCategoryMap.ContainsKey(product))
                {
                    productCategoryMap[product] = new List<Category>();
                }

                productCategoryMap[product].Add(category);
            }
        }

        return productCategoryMap;
    }

    public Category FindCategory(List<Category> categories, int categoryId)
    {
        foreach (var category in categories)
        {
            if (category.Id == categoryId)
            {
                return category;
            }

            if (category.Subcategories != null)
            {
                var foundSubcategory = category.Subcategories.FirstOrDefault(sub => sub.Id == categoryId);
                if (foundSubcategory != null)
                {
                    return foundSubcategory;
                }
            }
        }

        return null;
    }
}