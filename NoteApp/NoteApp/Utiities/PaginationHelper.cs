using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Utiities
{
    public class PaginationHelper
    {
        public int PageNumber {  get; set; }

        public int TotalPages { get; set; }

        public PaginationHelper(int totalItems, int pageNumber, int itemPerPage)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalItems / (double)itemPerPage);
        }
        
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
