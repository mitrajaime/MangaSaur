using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaSaur.DataModels
{
    public class Post
    {
        public string Username { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int LikeCount { get; set; }
        public string ID { get; set; }
        public string ownerId { get; set; }
        public DateTime postDate { get; set; }
    }
}