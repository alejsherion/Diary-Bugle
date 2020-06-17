using ClarinDiary.Business.Contract;
using ClarinDiary.Business.DTO;
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
        private readonly IRepository<Person> PersonRepository;
        #endregion

        #region Builder
        public PostAppService(IRepository<Post> postRepository, IRepository<Person> personRepository)
        {
            PostRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            PersonRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a post (only writer)
        /// </summary>
        /// <param name="post">post data content</param>
        /// <returns></returns>
        public ResponseResult<Post> Add(PostDTO post)
        {
            try
            {
                var _post = new Post()
                {
                    PostContent = post.PostContent,
                    PostTitle = post.PostTitle,
                    IdAuthor = post.IdAuthor,
                    PostDate = DateTime.Now,
                    State = (int)PostStatusEnum.PendingApproval
                };
                _post = PostRepository.Add(_post);
                PostRepository.Save();

                return ResponseResult<Post>.Success(_post);
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
        public ResponseResult<IEnumerable<PostDTO>> GetPostPending()
        {
            try
            {
                var postByWriter = PostRepository
                    .Get()
                    .Where(p => p.State == (int)PostStatusEnum.PendingApproval)
                    .Select(p => new PostDTO()
                    {
                        Id = p.Id,
                        IdAuthor = p.IdAuthor,
                        PostContent = p.PostContent,
                        PostTitle = p.PostTitle,
                        PostDate = p.PostDate,
                        Author = PersonRepository.GetById(p.IdAuthor).FullName
                    })
                    .ToList();

                return ResponseResult<IEnumerable<PostDTO>>.Success(postByWriter);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<PostDTO>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// list approved publications
        /// </summary>
        /// <returns></returns>
        public ResponseResult<IEnumerable<PostDTO>> GetPostedOn()
        {
            try
            {
                var postByWriter = PostRepository
                    .Get()
                    .Where(p => p.State == (int)PostStatusEnum.Approved)
                    .Select(p => new PostDTO()
                    {
                        Id = p.Id,
                        IdAuthor = p.IdAuthor,
                        PostContent = p.PostContent,
                        PostTitle = p.PostTitle,
                        PostDate = p.PostDate,
                        Author = PersonRepository.GetById(p.IdAuthor).FullName
                    })
                    .OrderByDescending(p => p.PostDate)
                    .ToList();

                return ResponseResult<IEnumerable<PostDTO>>.Success(postByWriter);
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<PostDTO>>.Error(ex.Message);
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

        /// <summary>
        /// Translate the content of a post on demand
        /// </summary>
        /// <param name="postId">id post</param>
        /// <param name="langOrigin">language origin</param>
        /// <param name="langTarget">language target</param>
        /// <returns></returns>
        public ResponseResult<PostDTO> GetTraslatePost(Guid postId, string langOrigin, string langTarget)
        {
            try
            {
                var post = PostRepository.GetById(postId);
                if (post == null)
                    return ResponseResult<PostDTO>.Error("Post does not exist!.");

                var postDto = new PostDTO()
                {
                    Id = post.Id,
                    Author = PersonRepository.GetById(post.IdAuthor).FullName,
                    PostDate = post.PostDate,
                    IdAuthor = post.IdAuthor,
                    PostTitle = TraslatorHelper.TraslateText(post.PostTitle, langOrigin, langTarget),
                    PostContent = TraslatorHelper.TraslateText(post.PostContent, langOrigin, langTarget)
                };

                return ResponseResult<PostDTO>.Success(postDto);
            }
            catch (Exception ex)
            {
                return ResponseResult<PostDTO>.Error(ex.Message);
            }
        }
        #endregion
    }
}
