using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NoteApp.Data;
using Microsoft.EntityFrameworkCore;
using NoteApp.Utiities;

namespace NoteApp.Controllers
{
    public class NoteController : Controller
    {

        private readonly NoteDbContext dbContext;

        public NoteController(NoteDbContext options)
        {
            dbContext = options;
        }

        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var elementPerPage = 3;
            var totalElements = await dbContext.Notes.CountAsync();
            var items = await dbContext.Notes.Skip((currentPage - 1) * elementPerPage).Take(elementPerPage).ToListAsync();
            var paginationHelper = new PaginationHelper(totalElements, currentPage, elementPerPage);
            var indexViewModel = new IndexViewModel()
            {
                Notes = items,
                PaginationHelper = paginationHelper
            };
            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (note == null)
            {
                return NotFound();
            }
            await dbContext.Notes.AddAsync(note);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var note = await dbContext.Notes.FirstOrDefaultAsync(note =>
                note.NoteId == id);
            if (note is null)
            {
                return NotFound();
            }
            return PartialView(note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Note note)
        {           
            if (note.NoteId is 0)
            {
                note.NoteId = id;
            }
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var note = await dbContext.Notes.FirstOrDefaultAsync(note =>
               note.NoteId == id);
            if (note is null)
            {
                return NotFound();
            }
            return PartialView(note);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var note = await dbContext.Notes.FirstOrDefaultAsync(note =>
             note.NoteId == id);
            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Archive(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var note = await dbContext.Notes.FirstOrDefaultAsync(note
                => note.NoteId == id);
            if (note is null)
            {
                return NotFound();
            }
            return PartialView(note);
        }

        [HttpPost, ActionName("Archive")]
        public async Task<IActionResult> ConfirmArchiving(int id,[Bind("Archived")] Note noteArchived)
        {
            if(noteArchived is null)
            {
                return NotFound();
            }
            var note = await dbContext.Notes.FirstOrDefaultAsync(note => note.NoteId == id);
            note.Archived = noteArchived.Archived;
            dbContext.Notes.Update(noteArchived);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }                    
    }
}
