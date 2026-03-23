using AutoMapper;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;

namespace Service.services
{
    public class WordLineService : IWordLine
    {
        private readonly IRepository<WordLine> _repository;
        private readonly IMapper _mapper;

        public WordLineService(IRepository<WordLine> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public WordLineDto AddWordLine(WordLineDto wordLineDto)
        {
            var entity = _mapper.Map<WordLine>(wordLineDto);
            _repository.AddItem(entity);
            return _mapper.Map<WordLineDto>(entity);
        }

        public List<WordLineDto> GetLinesBySongId(int songId)
        {
            var lines = _repository.GetAll()
                                   .Where(l => l.SongId == songId)
                                   .OrderBy(l => l.LineNumber);

            return _mapper.Map<List<WordLineDto>>(lines);
        }
        public void DeleteBySongId(int id)
        {
            var itemsToDelete = _repository.GetAll()
                                           .Where(item => item.SongId == id)
                                           .ToList();
            foreach (var item in itemsToDelete)
            {
                _repository.DeleteItem(item.Id);
            }
        }
    }
}