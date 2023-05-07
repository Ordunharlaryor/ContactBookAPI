using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Core.DTOs
{
        public class PagedResultDTO<T>
        {
            public List<T> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }

            public PagedResultDTO(List<T> items, int totalCount, int pageNumber, int pageSize)
            {
                Items = items;
                TotalCount = totalCount;
                PageNumber = pageNumber;
                PageSize = pageSize;
            }
        }
    }

