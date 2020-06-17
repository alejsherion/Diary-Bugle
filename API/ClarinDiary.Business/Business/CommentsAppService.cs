using ClarinDiary.Business.Contract;
using ClarinDiary.Business.DTO;
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
        private readonly IRepository<Person> PersonRepository;
        #endregion

        #region Builder
        public CommentsAppService(IRepository<PostComment> commentRepository, IRepository<Person> personRepository)
        {
            CommentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            PersonRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Add a comment in a post
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        public ResponseResult<dynamic> Add(PostCommentDTO postComment)
        {
            try
            {
                var comment = new PostComment()
                {
                    IdPerson = postComment.IdPerson,
                    Comment = postComment.Comment,
                    IdPost = postComment.IdPost
                };
                comment = CommentRepository.Add(comment);
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
        public ResponseResult<IEnumerable<PostCommentDTO>> GetCommentsByPost(Guid postId)
        {
            try
            {
                var commentsByPost = CommentRepository
                    .Get()
                    .Where(c => c.IdPost == postId)
                    .Select(c => new PostCommentDTO()
                    {
                        Id = c.Id,
                        IdPerson = c.IdPerson,
                        Comment = c.Comment,
                        IdPost = c.IdPost,
                        Author = c.IdPerson != null ? PersonRepository.GetById((Guid)c.IdPerson).FullName : "Anonymus"
                    })
                    .ToList();

                return ResponseResult<IEnumerable<PostCommentDTO>>.Success(commentsByPost);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<PostCommentDTO>>.Error(ex.Message);
            }
        } 
        #endregion
    }
}
