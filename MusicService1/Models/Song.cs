using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicService1.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Audio { get; set; }

        public string Image { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        /*[Required]
        [Display(Name = "Author")]
        public string AuthorId { get; set; }*/

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        /*[ForeignKey("AuthorId")]
        public AppUser AppUser { get; set; } //если не выйдет, то добавить айди в АпЮзер*/

        public List<PlaylistDetail> PlaylistDetails { get; set; }
        public List<UserPlaylist> UserPlaylists { get; set; }
    }
}
