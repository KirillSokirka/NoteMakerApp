using NoteApp.Utiities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class IndexViewModel
    {
        public PageViewModel PageModel {  get; set; }
        public FilterViewModel FilterModel { get; set; }
        public IEnumerable<Note> Notes {  get; set; }
    }
}
