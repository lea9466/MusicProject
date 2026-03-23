using AutoMapper;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;

namespace Service.services
{
    public class ChordService : IChord
    {
        private readonly IRepository<Chord> _repository;
        private readonly IMapper _mapper;

        public ChordService(IRepository<Chord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ChordDto CreateChord(ChordDto chordDto)
        {
            if (chordDto == null) return null;
            var entity = _mapper.Map<Chord>(chordDto);
            _repository.AddItem(entity);
            return _mapper.Map<ChordDto>(entity);
        }

        public ChordDto GetChordById(int id)
        {
            var chord = _repository.GetById(id);
            return _mapper.Map<ChordDto>(chord);
        }

        public void DeleteChordById(int id)
        {
            _repository.DeleteItem(id);
        }

        public Dictionary<int, List<ChordDto>> GetChordsBySongId(int songId)

        {

            var allChords = _repository.GetAll().Where(c => c.SongId == songId);
            var chordsList = _mapper.Map<List<ChordDto>>(allChords); 
            return chordsList
                .GroupBy(c => c.LineNumber)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key ?? 0,
                    g => g.OrderBy(c => c.IndexInLine).ToList()
                );
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