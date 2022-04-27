using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MusicService1.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public string SongType { get; set; }

        public List<Song> Songs { get; set; }
    }
}
