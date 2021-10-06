using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Services
{
    public interface ICategoryService  
    {
        public Task<List<Category>> CategoriesAll();
    }

    public class CategoryService :  ICategoryService
    {
         
        private readonly DatabaseContext _databaseContext;
        public CategoryService(DatabaseContext databaseContext )
        {
             
            _databaseContext = databaseContext;
        }

        public async Task<List<Category>> CategoriesAll()
        {
            return await _databaseContext.Categories.ToListAsync();
        }
    }
}
