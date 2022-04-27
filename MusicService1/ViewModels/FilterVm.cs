using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicService1.Models;

namespace MusicService1.ViewModels
{
    public class FilterVm
    {
        public IEnumerable<Song> Songs { get; set; }
        public SelectList Genres { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string? SearchString { get; set; }
    }
}
