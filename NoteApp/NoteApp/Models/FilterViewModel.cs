using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoteApp.Models
{
    public class FilterViewModel
    {       
        public SelectList Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}
