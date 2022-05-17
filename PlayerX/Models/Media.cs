namespace PlayerX.Models
{
    public class Media : BaseModel
    {
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public Folder Folder { get; set; } = null!;
        public Media? Next { get; set; }
    }
}
