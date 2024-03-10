using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;
using MangaSaur.DataModels;
using System;
using System.Collections.Generic;
using MangaSaur.Adapters;

namespace MangaSaur.Adapters
{
    internal class PostAdapter : RecyclerView.Adapter
    {
        public event EventHandler<PostAdapterClickEventArgs> ItemClick;
        public event EventHandler<PostAdapterClickEventArgs> ItemLongClick;
        List<Post> items;

        public PostAdapter(List<Post> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.post, parent, false);
            var vh = new PostAdapterViewHolder(itemView, OnClick, OnLongClick);

            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PostAdapterViewHolder;
            //holder.TextView.Text = items[position];

            holder.textViewUserName.Text = item.Username;
            holder.textViewPostBody.Text = item.Description;
            holder.textViewLike.Text = item.LikeCount.ToString() + " Likes";
        }

        public override int ItemCount => items.Count;

        void OnClick(PostAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PostAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PostAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView textViewUserName;
        public TextView textViewPostBody;
        public TextView textViewLike;
        public ImageView imageViewPicture;
        public ImageView imageViewLike;

        public PostAdapterViewHolder(View itemView, Action<PostAdapterClickEventArgs> clickListener,
                            Action<PostAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            textViewUserName = (TextView) itemView.FindViewById(Resource.Id.textViewUserName);
            textViewPostBody = (TextView) itemView.FindViewById(Resource.Id.textViewPostBody);
            textViewLike = (TextView) itemView.FindViewById(Resource.Id.textViewLike);
            imageViewPicture = (ImageView) itemView.FindViewById(Resource.Id.imageViewPicture);
            imageViewLike = (ImageView) itemView.FindViewById(Resource.Id.imageViewLike);

            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PostAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}