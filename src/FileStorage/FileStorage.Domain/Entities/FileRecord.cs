namespace Domain.Entities
{
    public class FileRecord
    {
        public Guid FileId { get; init; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public long Size { get; private set; }
        public string StoragePath { get; private set; }

        public FileRecord(string fileName, string contentType, long size, string storagePath)
        {
            FileId = Guid.NewGuid();
            FileName = fileName;
            ContentType = contentType;
            Size = size;
            StoragePath = storagePath;
        }

        public void SetStoragePath(string storagePath)
        {
            StoragePath = storagePath;   
        }
    }
}