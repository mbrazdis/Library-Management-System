using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMB3.ViewModel
{
    public class BookModel
    {
        public int BookId { get; set; }
        public int BookTypeId { get; set; }
        public string BookTypeName { get; set; }
        public int PublisherId { get; set; }
        public string PublishName { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public int Stock {  get; set; }
        public bool Active { get; set; }

    }
}
