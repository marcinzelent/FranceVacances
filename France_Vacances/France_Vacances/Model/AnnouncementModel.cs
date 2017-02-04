using System;

namespace France_Vacances.Model
{
    public class AnnouncementModel
    {
        public string AnnouncementId { get; set; }
        public string Content { get; set; }
        public string BackgroundPath { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }
    }
}

