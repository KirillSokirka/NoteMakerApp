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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoteApp.Controllers
{
    public class NoteController : Controller
    {

        private readonly NoteDbContext dbContext;

        public NoteController(NoteDbContext options)
        {
            dbContext = options;
        }

        public async Task<IActionResult> Index(string category, int currentPage = 1)
        {
            var categories = from note in dbContext.Notes
                             orderby note.Category
                             where !note.Category.Equals(null)
                             select note.Category;

            var notes = from note in dbContext.Notes
                        where note.Archived.Equals(false) 
                        select note;

            var archivedNotes = from note in dbContext.Notes
                                where note.Archived.Equals(true)
                                select note;
                             
            if(!string.IsNullOrEmpty(category))
            {
                notes = notes.Where(note => note.Category == category);
            }
            
            var elementPerPage = 5;
            var totalElements = await notes.CountAsync();
            var items = await notes.Skip((currentPage - 1) * elementPerPage).Take(elementPerPage).ToListAsync();

            var indexViewModel = new IndexViewModel()
            {
                Notes = items,
                ArchivedNotes = archivedNotes.Any() ? archivedNotes : null,
                PageModel = new PageViewModel(totalElements, currentPage, elementPerPage),
                FilterModel = new FilterViewModel()
                {
                    Categories = new SelectList(await categories.Distinct().ToListAsync()),                    
                }
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
        public async Task<IActionResult> ConfirmArchiving(int id, bool? archived)
        {
            if (archived is null)
            {
                return NotFound();
            }
            var note = await dbContext.Notes.FirstOrDefaultAsync(note => note.NoteId == id);
            note.Archived = (bool)archived;
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult ShowArchived(string category)
        {
            if (category is null)
            {
                return RedirectToAction("Index");
            }
            var notes = from note in dbContext.Notes
                        where category.Equals(note.Category) && note.Archived.Equals(true)
                        select note;
            
            if (notes.Count() == 0)
            {
                return NotFound();
            }
            return View(notes);
        }
    }
}
