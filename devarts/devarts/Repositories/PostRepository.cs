using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Repositories
{
    public class PostRepository : IPostRepository
    {
        private SiteDbContext _db;

        public PostRepository()
        {
            _db = new SiteDbContext();
        }

        /// POSTY

        public IQueryable<Post> GetAllPosts()
        {
            return _db.Posts.OrderByDescending(p => p.Id);
        }

        public Post GetPostById(int id)
        {
            return _db.Posts.FirstOrDefault(p => p.Id == id);
        }

        public Post GetPostByPostLink(string postLink)
        {
            return _db.Posts.FirstOrDefault(p => p.PostLink.ToLower() == postLink.ToLower());
        }

        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }

        /// ZDJĘCIA DO POSTÓW

        public PostImage GetPostImageById(int id)
        {
            return _db.PostImages.FirstOrDefault(f => f.Id == id);
        }

        public PostImage GetImageFileByImageFileNameAndPost(string imgFileName, string entryLink)
        {
            return _db.PostImages.FirstOrDefault(i => i.ImageFileName == imgFileName && i.PostLink == entryLink);
        }

        public PostImage GetPostByImageFile(string imageFileName)
        {
            return _db.PostImages.FirstOrDefault(f => f.ImageFileName == imageFileName);
        }

        public List<PostImage> GetAllImages()
        {
            return _db.PostImages.OrderBy(u => u.PostId).ToList();
        }

        public PostImage GetMainImageOfPost(string entrylink)
        {
            return _db.PostImages.OrderBy(u => u.Id).FirstOrDefault(u => u.PostLink == entrylink);
        }

        public List<PostImage> GetImagesByPost(string entryLink)
        {
            return _db.PostImages.Where(img => img.PostLink == entryLink).ToList();
        }

        public List<PostImage> GetImagesByPostId(int postId)
        {
            return _db.PostImages.Where(p => p.PostId == postId).ToList();
        }

        public List<PostImage> GetImagesByPostLink(string postLink)
        {
            return _db.PostImages.Where(p => p.PostLink == postLink).ToList();
        }

        public void AddImage(PostImage postImage)
        {
            _db.PostImages.Add(postImage);
        }

        public void DeleteImage(PostImage postImage)
        {
            _db.PostImages.Remove(postImage);
            _db.SaveChanges();
        }

        /// KOMENTARZE DO POSTÓW

        //public void AddComment(Comments)

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}