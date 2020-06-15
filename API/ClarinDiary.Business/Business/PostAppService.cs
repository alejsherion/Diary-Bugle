using ClarinDiary.Business.Contract;
using ClarinDiary.Business.Enums;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using ClarinDiary.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClarinDiary.Business.Business
{
    public class PostAppService : IPostAppService
    {
        #region Members
        private readonly IRepository<Post> PostRepository;
        #endregion

        #region Builder
        public PostAppService(IRepository<Post> postRepository)
        {
            PostRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a post (only writer)
        /// </summary>
        /// <param name="post">post data content</param>
        /// <returns></returns>
        public ResponseResult<Post> Add(Post post)
        {
            try
            {
                post.PostDate = DateTime.Now;
                post.State = (int)PostStatusEnum.PendingApproval;
                post = PostRepository.Add(post);
                PostRepository.Save();

                return ResponseResult<Post>.Success(post);
            }
            catch (Exception ex)
            {
                return ResponseResult<Post>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update a post (only writer)
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public ResponseResult<Post> Update(Post post)
        {
            try
            {
                var validation = PostRepository.GetById(post.Id);
                if (validation == null)
                    return ResponseResult<Post>.Error("Post already exist!.");

                post.IdPublisher = null;
                post.PostDate = DateTime.Now;
                post.State = (int)PostStatusEnum.PendingApproval;
                PostRepository.Update(post);
                PostRepository.Save();

                return ResponseResult<Post>.Success();
            }
            catch (Exception ex)
            {
                return ResponseResult<Post>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update the status for a post (only editor)
        /// </summary>
        /// <param name="postid">id post</param>
        /// <param name="editorId">id editor that updates</param>
        /// <param name="state">status indicated for post</param>
        /// <returns></returns>
        public ResponseResult<Post> SetStatusPost(Guid postid, Guid editorId, PostStatusEnum state)
        {
            try
            {
                var post = PostRepository.GetById(postid);
                if (post == null)
                    return ResponseResult<Post>.Error("Post does not exist!.");

                post.IdPublisher = editorId;
                post.State = (int)state;
                PostRepository.Update(post);
                PostRepository.Save();

                return ResponseResult<Post>.Success();
            }
            catch (Exception ex)
            {
                return ResponseResult<Post>.Error(ex.Message);
            }
        }

        /// <summary>
        /// lists the publications that are pending approval
        /// </summary>
        /// <returns></returns>
        public ResponseResult<IEnumerable<Post>> GetPostPending()
        {
            try
            {
                var postByWriter = PostRepository
                    .Get()
                    .Where(p => p.State == (int)PostStatusEnum.PendingApproval)
                    .ToList();

                return ResponseResult<IEnumerable<Post>>.Success(postByWriter);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<Post>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// list approved publications
        /// </summary>
        /// <returns></returns>
        public ResponseResult<IEnumerable<Post>> GetPostedOn()
        {
            try
            {
                var postByWriter = PostRepository
                    .Get()
                    .Where(p => p.State == (int)PostStatusEnum.Approved)
                    .ToList();

                return ResponseResult<IEnumerable<Post>>.Success(postByWriter);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<Post>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// lists the posts made by a writer
        /// </summary>
        /// <param name="writerId"></param>
        /// <returns></returns>
        public ResponseResult<IEnumerable<Post>> GetPostByWritter(Guid writerId)
        {
            try
            {
                var postByWriter = PostRepository
                    .Get()
                    .Where(p => p.IdAuthor == writerId)
                    .ToList();

                return ResponseResult<IEnumerable<Post>>.Success(postByWriter);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<Post>>.Error(ex.Message);
            }
        } 
        #endregion
    }
}
