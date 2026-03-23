using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDTO
{
    public class SongRequestDto
    {
        public int Id { get; set; }
        public string SongDes { get; set; }
        public int CreatorId { get; set; }
        public string? CreatorName { get; set; }
        public int? VotesCount { get; set; } 
        public double? PriorityScore { get; set; } 
        public bool? IsFulfilled { get; set; }
        public int? FulfillerId { get; set; }
        public string? FulfillerName { get; set; }
        public DateOnly? Date { get; set; }
        public bool? isVotedByMe { get; set; } = false;

    }
}
