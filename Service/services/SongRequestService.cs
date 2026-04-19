using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;

namespace Service.services
{
    public class SongRequestService : ISongRequest
    {
        private readonly IRepository<SongRequest> _repository;
        private readonly IRepository<SongRequestVote> _SRVrepository;
        private readonly IMapper _mapper;

        public SongRequestService(IRepository<SongRequest> repository, IMapper mapper, IRepository<SongRequestVote> sRVrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _SRVrepository = sRVrepository;
        }
        public SongRequestDto AddSongRequest(SongRequestDto dto)
        {
            // 1. יצירת אובייקט חדש ונקי מה-DTO
            var newRequest = new SongRequest
            {
                SongDes = dto.SongDes,
                // ודאי שה-UserId מגיע מה-React/Controller כמו שצריך
                CreatorId = dto.CreatorId,
                PriorityScore = 0,
                IsFulfilled = false
            };

            // 2. הוספה ל-Repository
            _repository.AddItem(newRequest);

            // 3. החזרת ה-DTO המעודכן (כולל ה-ID שה-SQL יצר)
            return _mapper.Map<SongRequestDto>(newRequest);
        }

        public List<SongRequestDto> GetAllSongRequests(int userId)
        {
            // 1. שליפת כל הבקשות
            var songRequests = _repository.GetAll().Where(s => !s.IsFulfilled).ToList();
            var songRequestsDto = _mapper.Map<List<SongRequestDto>>(songRequests);

            // 2. שליפת כל ההצבעות כולל מידע על סוג המשתמש המצביע (חשוב לחישוב הניקוד)
            var allVotes = _SRVrepository.GetAll()
                .Include(v => v.User)
                .ToList();

            // 3. יצירת סט הצבעות של המשתמש הנוכחי (בשביל ה-UI)
            var userVotedSongIds = userId > 0
                ? allVotes.Where(v => v.UserId == userId).Select(v => v.SongRequestId).ToHashSet()
                : new HashSet<int>();

            var today = DateOnly.FromDateTime(DateTime.Today);

            foreach (var dto in songRequestsDto)
            {
                // א. סימון האם המשתמש הנוכחי הצביע (בשביל ה-React)
                if (userVotedSongIds.Contains(dto.Id))
                {
                    dto.isVotedByMe = true;
                }

                // ב. חישוב ניקוד מהצבע ות: משתמש רגיל +1, משודרג +2
                double votesScore = allVotes
                    .Where(v => v.SongRequestId == dto.Id)
                    .Sum(v => v.User.Role == UserRole.Manager ? 3.0 : v.User.Role == UserRole.Admin ? 2.0 : 1.0);

                // ג. חישוב ניקוד מזמן: כל יום שחלף +1
                double dateScore = 0;
                if (dto.Date.HasValue)
                {
                    dateScore = today.DayNumber - dto.Date.Value.DayNumber;
                }
                dto.VotesCount = allVotes.Where(v => v.SongRequestId == dto.Id).Count();
                dto.PriorityScore = votesScore + dateScore;
            }
            return songRequestsDto.OrderByDescending(x => x.PriorityScore).ToList();
        }
        public void FullReq(SongRequestDto srd)
        {
            _repository.UpdateItem(srd.Id, _mapper.Map<SongRequest>(srd));

        }
    }
}
