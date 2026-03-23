using AutoMapper;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;

namespace Service.services
{
    public class SongService : ISong
    {
        private readonly IRepository<Song> _repository;
        private readonly IWordLine _wordLineService;
        private readonly IChord _chordService;
        private readonly IUserFavoriteSong _userFavoriteSongService;
        private readonly IMapper _mapper;

        public SongService(
            IRepository<Song> repository,
            IWordLine wordLineService,
            IChord chordService,
            IMapper mapper,
            IUserFavoriteSong userFavoriteSongService)
        {
            _repository = repository;
            _wordLineService = wordLineService;
            _chordService = chordService;
            _mapper = mapper;
            _userFavoriteSongService = userFavoriteSongService;
        }

        public List<SongDto> GetAllSongs()
        {
            var songs = _repository.GetAll();
            return _mapper.Map<List<SongDto>>(songs);
        }
        public List<SongDto> GetNewSongs()
        {
            var songs = _repository.GetAll()
                .OrderByDescending(s => s.Date) 
                .Take(6)                       
                .ToList();                    
            return _mapper.Map<List<SongDto>>(songs);
        }

        public List<SongDto> GetSongsByIds(List<int> ids)
        {
            var songs = _repository.GetAll().Where(s => ids.Contains(s.Id));
            return _mapper.Map<List<SongDto>>(songs);
        }
        public List<SongDto> GetSongsByCatId(int id)
        {
            var songs = _repository.GetAll().Where(s => s.CategoryId==id);
            return _mapper.Map<List<SongDto>>(songs);
        }

        public SongDto AddSong(SongDto songDto)
        {
            var newSong = _mapper.Map<Song>(songDto);
            _repository.AddItem(newSong);
            return _mapper.Map<SongDto>(newSong);
        }

        public bool AddFullSong(FullSongDto fullSongDto)
        {
            try
            {
                var savedSong = AddSong(fullSongDto.Song);
                if (savedSong == null || savedSong.Id <= 0) return false;

                // טיפול בשורות טקסט
                fullSongDto.WordLines?.ForEach(line =>
                {
                    line.SongId = savedSong.Id;
                    _wordLineService.AddWordLine(line);
                });

                // טיפול באקורדים
                fullSongDto.Chords?.ForEach(chord =>
                {
                    chord.SongId = savedSong.Id;
                    _chordService.CreateChord(chord);
                });

                return true;
            }
            catch { return false; }
        }

        public FullSongDto GetFullSongById(int songId)
        {
            var songEntity = _repository.GetById(songId);
            if (songEntity == null) return null;
            return new FullSongDto
            {
                Song = _mapper.Map<SongDto>(songEntity),
                WordLines = _wordLineService.GetLinesBySongId(songId),
                ChordsByLine = _chordService.GetChordsBySongId(songId)
            };
        }

        public List<SongDto> GetSongsByUserId(int userId)
        {
            var userSongs = _repository.GetAll().Where(s => s.UserId == userId);
            return _mapper.Map<List<SongDto>>(userSongs);
        }

        public SongDto UpdateSong(FullSongDto fullSongDto)
        {
            try
            {
                var existingSong = _repository.GetById(fullSongDto.Song.Id);
                if (existingSong == null) throw new Exception("Song not found");
                if (existingSong.SourceText != fullSongDto.Song.SourceText)
                {
                    // ניקוי נתונים ישנים
                    _chordService.DeleteBySongId(fullSongDto.Song.Id);
                    _wordLineService.DeleteBySongId(fullSongDto.Song.Id);

                    // הוספת שורות מילים
                    fullSongDto.WordLines?.ForEach(line =>
                    {
                        line.SongId = existingSong.Id;
                        _wordLineService.AddWordLine(line);
                    });

                    // הוספת אקורדים
                    fullSongDto.Chords?.ForEach(chord =>
                    {
                        chord.SongId = existingSong.Id;
                        _chordService.CreateChord(chord);
                    });
                }

                // עדכון פרטי השיר עצמו
                var updatedSongEntity = _mapper.Map<Song>(fullSongDto.Song);
                _repository.UpdateItem(fullSongDto.Song.Id, updatedSongEntity);
                return _mapper.Map<SongDto>(updatedSongEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update song: {ex.Message}");
            }

        }
        public bool DeleteSong(int id)
        {
            try
            {
                _chordService.DeleteBySongId(id);
                _userFavoriteSongService.DeleteBySongId(id);
                _wordLineService.DeleteBySongId(id);
                _repository.DeleteItem(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}