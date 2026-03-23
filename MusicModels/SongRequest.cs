using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicModels
{
    public class SongRequest
    {
        public int Id { get; set; }
        public string SongDes { get; set; }
        public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public int CreatorId { get; set; }
        public User User { get; set; }
        public int? FulfillerId { get; set; }
        public User Fulfiller { get; set; }
        public virtual ICollection<SongRequestVote> Votes { get; set; } = new List<SongRequestVote>();
        public double? PriorityScore { get; set; } 
        public bool IsFulfilled { get; set; } = false;
    }
}
