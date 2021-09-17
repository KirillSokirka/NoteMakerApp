using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class Note
    {
        public int NoteId {  get; set; }
        public string Title { get; set; }    
        public string Description { get; set; }
        public string Category { get; set; }    
        public DateTime Created { get; set; }   
        public DateTime MentionedDate { get; set; }   
        public bool Archived { get; set; }

    }
}
