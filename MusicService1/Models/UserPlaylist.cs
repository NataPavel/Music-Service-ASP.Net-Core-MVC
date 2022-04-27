using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicService1.Models
{
    public class UserPlaylist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Song")]
        public int SongId { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("SongId")]
        public Song Song { get; set; }

        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
    }
}
