using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NoteApp.Models;

namespace NoteApp.Utiities
{
    public class NoteUpWriter
    {
        public static void UpWrite(Note note)
        {
            note.Created = note.Created == DateTime.MinValue ? DateTime.Now : note.Created;

            note.MentionedDate = GetMentionedDate(note.Description);
        }

        private static DateTime GetMentionedDate(string descriprtion)
        {
            var regex = new Regex(@"(\d{1,2})/(\d{1,2})/(\d{2,4})");
            var matches = regex.Match(descriprtion);
            if(matches.Success)
            {                
                return new DateTime(Convert.ToInt32(matches.Groups[3].Value), 
                    Convert.ToInt32(matches.Groups[2].Value), Convert.ToInt32(matches.Groups[1].Value));
            }
            return DateTime.MinValue;
        }
    }
}
