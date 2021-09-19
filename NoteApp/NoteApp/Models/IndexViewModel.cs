using NoteApp.Utiities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class IndexViewModel
    {
        public PaginationHelper PaginationHelper {  get; set; }
        public IEnumerable<Note> Notes {  get; set; }
    }
}
