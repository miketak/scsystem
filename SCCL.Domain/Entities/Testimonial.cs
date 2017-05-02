using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCL.Domain.Entities
{
    public class Testimonial
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public string Author { get; set; }
    }
}
