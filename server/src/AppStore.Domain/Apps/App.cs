using System;

namespace AppStore.Domain.Apps
{
    public class App
    {
        public int AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public decimal Price { get; set; }
        public string ThumbUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
