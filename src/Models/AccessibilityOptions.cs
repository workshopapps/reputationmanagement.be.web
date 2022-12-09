namespace src.Models
{
    public class AccessibilityOptions
    {
        public string Language { get; set; } = "english";
        public bool LargeText { get; set; }
        public bool ScreenReader { get; set; }
        public bool HighContrast { get; set; }
    }
}
