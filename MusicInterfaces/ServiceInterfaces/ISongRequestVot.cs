using MusicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface ISongRequestVot
    {
        public SongRequestVoteDto ToggleVote(int userId, int requestId);
    }
}
