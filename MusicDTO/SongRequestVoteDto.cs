using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDTO
{
    public class SongRequestVoteDto
    {
        public int SongRequestId { get; set; }
        public string? UserName { get; set; }
        public DateTime VotedAt { get; set; } = DateTime.Now;
        public bool IsVoted { get; set; } 

    }
}