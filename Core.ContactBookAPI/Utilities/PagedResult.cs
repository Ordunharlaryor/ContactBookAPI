using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Core.Utilities
{
        public class PagedResult<T>
        {
            public List<T> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }

            public PagedResult()
            {
            Items = new List<T>();
        
            }
        }

    }

