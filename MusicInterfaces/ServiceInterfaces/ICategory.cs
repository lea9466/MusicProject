using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicDTO;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface ICategory
    {
        public CategoryDto GetCategoryById(int id);
        public List<CategoryDto> GetAllCategories();
        public CategoryDto CreateCategory(CategoryDto categoryDto);
        public bool DeleteCategory(int id);
        public void UpdateCategory(int id, CategoryDto categoryDto);

    }
}
