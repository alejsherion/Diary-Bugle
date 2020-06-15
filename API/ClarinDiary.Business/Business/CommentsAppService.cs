using ClarinDiary.Business.Contract;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using ClarinDiary.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClarinDiary.Business.Business
{
    public class CommentsAppService: ICommentsAppService
    {
        #region Members
        private readonly IRepository<PostComment> CommentRepository; 
        #endregion

        #region Builder
        public CommentsAppService(IRepository<PostComment> commentRepository)
        {
            CommentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Add a comment in a post
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        public ResponseResult<dynamic> Add(PostComment postComment)
        {
            try
            {
                postComment = CommentRepository.Add(postComment);
                CommentRepository.Save();

                return ResponseResult<dynamic>.Success(postComment);
            }
            catch (Exception ex)
            {
                return ResponseResult<dynamic>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Remove a comment
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        public ResponseResult<dynamic> Remove(Guid commentId)
        {
            try
            {
                var comment = CommentRepository.GetById(commentId);
                if (comment != null)
                    CommentRepository.Remove(comment);

                return ResponseResult<dynamic>.Success();
            }
            catch (Exception ex)
            {
                return ResponseResult<dynamic>.Error(ex.Message);
            }
        }

        /// <summary>
        /// list comments on a post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public ResponseResult<IEnumerable<PostComment>> GetCommentsByPost(Guid postId)
        {
            try
            {
                var commentsByPost = CommentRepository
                    .Get()
                    .Where(c => c.IdPost == postId)
                    .ToList();

                return ResponseResult<IEnumerable<PostComment>>.Success(commentsByPost);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<PostComment>>.Error(ex.Message);
            }
        } 
        #endregion
    }
}
