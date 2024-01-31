﻿using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("categories")]
    public class CategoryController : HomeController
    {

        public CategoryController(
            ILogger<HomeController> logger,
            ICategoryService categoryService,
            IProductService productService, 
            IService service) : base(
            logger, 
            service,
            productService,
            categoryService
        )
        {}

        [HttpGet("Index", Name = "CategoryIndex")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            System.Console.WriteLine($"Categories  = {categories}");
            return View(categories);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            return View(category);
        }

        [HttpPost("edit")]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            return View(category);
        }

        [HttpPost("delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}


    /*[Route("categories")]
public class CategoryController : Controller 
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _categoryService.GetAllCategories();
        System.Console.WriteLine($"Categories  = {categories}");
        return View(categories);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost("create")]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.AddCategory(category);
            return RedirectToAction("Index");
        }

        return View(category);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        return View(category);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _categoryService.DeleteCategory(id);
        return RedirectToAction("Index");
    }
}
*/
