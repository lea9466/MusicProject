using AutoMapper;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;
namespace Service.services
{
    public class SongRequestVoteService : ISongRequestVot
    {
        private readonly IRepository<SongRequestVote> _repository;
        private readonly IMapper _mapper;
        private readonly ICompositeDelete<UserFavoriteSong> _repoDelete;

        public SongRequestVoteService(IRepository<SongRequestVote> repository, IMapper mapper, ICompositeDelete<UserFavoriteSong> repoDelete)

        {
            _repository = repository;
            _mapper = mapper;
            _repoDelete = repoDelete;

        }
        public SongRequestVoteDto ToggleVote(int userId, int requestId)
        {
            // 1. בדיקה: האם ההצבעה כבר קיימת ב-DB?
            var existingVote = _repository.GetAll()
                .FirstOrDefault(v => v.UserId == userId && v.SongRequestId == requestId);

           
            if (existingVote != null)
            {
                // --- מצב א': ביטול הצבעה (Unvote) ---
                _repoDelete.DeleteByKeys(userId, requestId);

                // עדכון הניקוד (מינימום 0)
                //request.PriorityScore = Math.Max(0, (request.PriorityScore ?? 0) - 10);
                //_requestRepository.UpdateItem(requestId, request);

                return new SongRequestVoteDto
                {
                    SongRequestId = requestId,
                    IsVoted = false
                    //NewPriorityScore = request.PriorityScore,
                    // אפשר להוסיף שדה ב-DTO כדי שה-React ידע שזה נמחק
                };
            }
            else
            {
                // --- מצב ב': הוספת הצבעה (Vote) ---
                var newVote = new SongRequestVote
                {
                    UserId = userId,
                    SongRequestId = requestId,
                };

                _repository.AddItem(newVote);

                // עדכון הניקוד
                //request.PriorityScore = (request.PriorityScore ?? 0) + 10;
                //_requestRepository.UpdateItem(requestId, request);

                var resultDto = _mapper.Map<SongRequestVoteDto>(newVote);
                resultDto.IsVoted= true;
                //resultDto.NewPriorityScore = request.PriorityScore;
                return resultDto;
            }
        }
    }
}
