using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCCL.Domain.Entities
{
    public class Solution
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<Service> SolServices { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

    }
}