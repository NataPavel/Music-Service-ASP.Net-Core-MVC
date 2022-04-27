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
    public class UserPlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserPlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserPlaylists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserPlaylists.Include(u => u.AppUser).Include(u => u.Song);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserPlaylists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.UserPlaylists
                .Include(u => u.AppUser)
                .Include(u => u.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            return View(userPlaylist);
        }

        // GET: UserPlaylists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio");
            return View();
        }

        // POST: UserPlaylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,UserId")] UserPlaylist userPlaylist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", userPlaylist.UserId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio", userPlaylist.SongId);
            return View(userPlaylist);
        }

        // GET: UserPlaylists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.UserPlaylists.FindAsync(id);
            if (userPlaylist == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", userPlaylist.UserId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio", userPlaylist.SongId);
            return View(userPlaylist);
        }

        // POST: UserPlaylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,UserId")] UserPlaylist userPlaylist)
        {
            if (id != userPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPlaylistExists(userPlaylist.Id))
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
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", userPlaylist.UserId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Audio", userPlaylist.SongId);
            return View(userPlaylist);
        }

        // GET: UserPlaylists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.UserPlaylists
                .Include(u => u.AppUser)
                .Include(u => u.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            return View(userPlaylist);
        }

        // POST: UserPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userPlaylist = await _context.UserPlaylists.FindAsync(id);
            _context.UserPlaylists.Remove(userPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlaylistExists(int id)
        {
            return _context.UserPlaylists.Any(e => e.Id == id);
        }
    }
}
