namespace PlayerX.Models
{
    public class Menu : BaseModel
    {
        public string Name { get; set; } = "Default Name";
        public string ImgPath { get; set; } = "/Images/folder-icon.png";
        public Folder? Folder { get; set; }
        public string? UrlPath { get; set; }
    }
}
