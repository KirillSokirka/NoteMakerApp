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

namespace NoteApp.Controllers
{
    public class NoteController : Controller
    {

        private readonly NoteDbContext dbContext;
        public NoteController(NoteDbContext options)
        {
            dbContext = options;
        }

        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Notes.ToListAsync());
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
            if(note == null)
            {
                return NotFound();
            }
            await dbContext.Notes.AddAsync(note);
            await dbContext.SaveChangesAsync();          
            return View("Index", await dbContext.Notes.ToListAsync());           
        }        
    }
}
