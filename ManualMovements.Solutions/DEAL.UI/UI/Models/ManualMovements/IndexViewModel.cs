using PagedList;
using System;

namespace Deal.UI.Models.ManualMovements
{
    [Serializable]
    public class IndexViewModel
    {
        public Index Index { get; set; }

        public String Message { get; set; }

        public bool Success { get; set; }

        public bool PersistFields { get; set; }

        public IPagedList<Index> Indexes { get; set; }
    }
}