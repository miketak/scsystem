using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCL.Core.Entities
{
    public class PortfolioImage
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public byte[] Image { get; set; }

        public string ImageMimeType { get; set; }
    }
}
