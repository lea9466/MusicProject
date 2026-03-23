using Microsoft.EntityFrameworkCore;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;
using AutoMapper;

namespace Service.services
{
    public class CategoryService : ICategory
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper; 

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper; 
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _repository.GetById(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _repository.GetAll();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public CategoryDto CreateCategory(CategoryDto categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);
            _repository.AddItem(newCategory);
            return _mapper.Map<CategoryDto>(newCategory);
        }

        public void UpdateCategory(int id, CategoryDto categoryDto)
        {
            var categoryToUpdate = _mapper.Map<Category>(categoryDto);
            _repository.UpdateItem(id, categoryToUpdate);
        }

        public bool DeleteCategory(int id)
        {
            var category = _repository.GetAll().Include(c => c.Songs).FirstOrDefault(c => c.Id == id);
            if (category == null) return false;

            foreach (var item in category.Songs)
            {
                item.CategoryId = 1;
            }

            _repository.DeleteItem(id);
            return true;
        }
    }
}
