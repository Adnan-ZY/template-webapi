using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Models
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
