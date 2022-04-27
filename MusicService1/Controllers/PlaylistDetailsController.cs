using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicService1.Data;
using MusicService1.Models;

namespace MusicService1.Controllers
{
    public class PlaylistDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaylistDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlaylistDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlaylistDetails.Include(p => p.Playlist).Include(p => p.Song);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlaylistDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistDetail = await _context.PlaylistDetails
                .Include(p => p.Playlist)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlistDetail == null)
            {
                return NotFound();
            }

            return View(playlistDetail);
        }

        // GET: PlaylistDetails/Create
        public IActionResult Create()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name");
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio");
            return View();
        }

        // POST: PlaylistDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaylistId,SongId")] PlaylistDetail playlistDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlistDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", playlistDetail.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio", playlistDetail.SongId);
            return View(playlistDetail);
        }

        // GET: PlaylistDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistDetail = await _context.PlaylistDetails.FindAsync(id);
            if (playlistDetail == null)
            {
                return NotFound();
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", playlistDetail.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio", playlistDetail.SongId);
            return View(playlistDetail);
        }

        // POST: PlaylistDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlaylistId,SongId")] PlaylistDetail playlistDetail)
        {
            if (id != playlistDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlistDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistDetailExists(playlistDetail.Id))
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
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", playlistDetail.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio", playlistDetail.SongId);
            return View(playlistDetail);
        }

        // GET: PlaylistDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistDetail = await _context.PlaylistDetails
                .Include(p => p.Playlist)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlistDetail == null)
            {
                return NotFound();
            }

            return View(playlistDetail);
        }

        // POST: PlaylistDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlistDetail = await _context.PlaylistDetails.FindAsync(id);
            _context.PlaylistDetails.Remove(playlistDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistDetailExists(int id)
        {
            return _context.PlaylistDetails.Any(e => e.Id == id);
        }
    }
}
