using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MusicService1.Models;

namespace MusicService1.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<UserPlaylist> UserPlaylists { get; set; }
        public DbSet<PlaylistDetail> PlaylistDetails { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
