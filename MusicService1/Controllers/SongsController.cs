using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicService1.Data;
using MusicService1.Models;
using MusicService1.ViewModels;

namespace MusicService1.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public SongsController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string id, int? gid, int page = 1)
        {
            int pageSize = 6;
            //*
            IQueryable<Song> songs = _context.Songs
                .Include(s => s.Genre);

            //*
            if (gid != null && gid != 0)
                songs = songs.Where(s => s.GenreId == gid);

            List<Genre> genres = _context.Genres.ToList();
            //*
            genres.Insert(0, new Genre() { Id = 0, SongType = "All genres" });

            //*
            var count = await songs.CountAsync();
            var items = await songs.Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            //*
            FilterVm filterVm = new FilterVm()
            {
                Songs = items,
                Genres = new SelectList(genres, "Id", "SongType"),
                PageViewModel = pageViewModel
            };

            //*
            var songsSearch = from s in _context.Songs select s;
            if (!String.IsNullOrEmpty(id))
            {
                songsSearch = songsSearch.Where(s => s.Name!.Contains(id));
            }
            /*
            var applicationDbContext = _context.Songs
                .Include(s => s.Genre);*/
            return View(filterVm);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "SongType");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Audio,Image,GenreId")] Song song,
            IFormFile image, IFormFile audio)
        {
            if (ModelState.IsValid)
            {
                //*
                if(image != null)
                {
                    var name = Path.Combine(_host.WebRootPath + "/img", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    song.Image = "img/" + image.FileName;
                }
                else
                {
                    song.Image = "img/default_song.jpg";
                }

                if (audio != null)
                {
                    var name = Path.Combine(_host.WebRootPath + "/audio", Path.GetFileName(audio.FileName));
                    await audio.CopyToAsync(new FileStream(name, FileMode.Create));
                    song.Audio = "audio/" + audio.FileName;
                }
                 
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "SongType", song.GenreId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "SongType", song.GenreId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Audio,Image,GenreId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "SongType", song.GenreId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
