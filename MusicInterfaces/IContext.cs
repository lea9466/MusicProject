using MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicIinterfaces
{
    public interface IContext
    {
        DbSet<Song> Songs { get; set; }
        DbSet<Chord> Chords { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserFavoriteSong> UserFavoriteSongs { get; set; }
        DbSet<WordLine> WordLines { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<SongRequest> SongRequests { get; set; }
        DbSet<SongRequestVote> songRequestVotes { get; set; }

        public void save();
    }
}
