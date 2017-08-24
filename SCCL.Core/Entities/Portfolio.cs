using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCL.Core.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public string ResearchArea { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string LinkUrl { get; set; }

        public byte[] Thumbnail { get; set; }

        public string ThumbnailMimeType { get; set; }

        public List<byte[]> DetailImages { get; set; }
        public string PortfolioType { get; set; }
        public string Summary { get; set; }
    }
}
