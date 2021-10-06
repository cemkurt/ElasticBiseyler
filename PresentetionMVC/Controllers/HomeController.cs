using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using PresentetionMVC.Models;
using Proj.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PresentetionMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        private readonly ICategoryService _categoryService;
        private readonly IElasticSearchManager<Category> _elasticSearchManager;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ICategoryService categoryService, IElasticSearchManager<Category> elasticSearchManager)
        {
            _elasticSearchManager = elasticSearchManager;
            _categoryService = categoryService;
            this.configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _categoryService.CategoriesAll();

            //    await _elasticSearchManager.LoadAsync(models);

            SearchDescriptor<Category> searchQuery =
                new SearchDescriptor<Category>().Query(x => x.MatchPhrasePrefix(v => v.Field(y => y.Name).Query("Bilgisayar")));



            //  var modelsE = await _elasticSearchManager.SearchAsync(searchQuery);


            //   var modelsE = await _elasticSearchManager.GetAsync(new Category { Id = 3 });

              _elasticSearchManager.DeleteIndexAsync();

            // var data = configuration.GetSection("siri").Value;
              var s = await _elasticSearchManager.AddOrUpdateAsync(new Category { Id = 9, Name = "edammmmm" });

            //   await _elasticSearchManager.DeleteAsync(modelsE.FirstOrDefault());


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
