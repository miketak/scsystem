namespace SCCL.Domain.Entities
{
    public class Service
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

    }
}
