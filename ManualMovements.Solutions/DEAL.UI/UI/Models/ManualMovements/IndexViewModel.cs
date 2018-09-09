using PagedList;
using System;
using System.Collections.Generic;

namespace Deal.UI.Models.ManualMovements
{
    [Serializable]
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Indexes = new List<Index>().ToPagedList(1, 1);
            Index = new Index();
            Filter = new Filter();

        }
        public Index Index { get; set; }

        public String Message { get; set; }

        public bool Success { get; set; }

        public bool PersistFields { get; set; }

        public Filter Filter { get; set; }

        public IPagedList<Index> Indexes { get; set; }
    }
}