using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicService1.Models
{
    public class PlaylistDetail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "PlaylistInfo")]
        public int PlaylistId { get; set; }

        [Display(Name = "Song")]
        public int SongId { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist Playlist { get; set; }

        [ForeignKey("SongId")]
        public Song Song { get; set; }
    }
}
