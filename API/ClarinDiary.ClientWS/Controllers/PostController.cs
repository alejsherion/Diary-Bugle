using ClarinDiary.Business.Contract;
using ClarinDiary.Business.Enums;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ClarinDiary.ClientWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        #region Members
        private readonly IPostAppService _postAppService;
        #endregion

        #region Builder
        public PostController(IPostAppService postAppService) : base() => _postAppService = postAppService;
        #endregion

        #region Methods
        /// <summary>
        /// Update the status for a post (only editor)
        /// </summary>
        /// <param name="postid">id post</param>
        /// <param name="editorId">id editor that updates</param>
        /// <param name="state">status indicated for post</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SetStatusPost")]
        public ResponseResult<Post> SetStatusPost(Guid postid, Guid editorId, PostStatusEnum state) => _postAppService.SetStatusPost(postid, editorId, state);

        /// <summary>
        /// lists the publications that are pending approval
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPostPending")]
        public ResponseResult<IEnumerable<Post>> GetPostPending() => _postAppService.GetPostPending();

        /// <summary>
        /// list approved publications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPostedOn")]
        public ResponseResult<IEnumerable<Post>> GetPostedOn() => _postAppService.GetPostedOn();

        /// <summary>
        /// lists the posts made by a writer
        /// </summary>
        /// <param name="writerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPostByWritter")]
        public ResponseResult<IEnumerable<Post>> GetPostByWritter(Guid writerId) => _postAppService.GetPostByWritter(writerId);

        /// <summary>
        /// Translate the content of a post on demand
        /// </summary>
        /// <param name="postId">id post</param>
        /// <param name="langOrigin">language origin</param>
        /// <param name="langTarget">language target</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTraslatePost")]
        public ResponseResult<Post> GetTraslatePost(Guid postId, string langOrigin, string langTarget) => _postAppService.GetTraslatePost(postId, langOrigin, langTarget);

        /// <summary>
        /// Add a post (only writer)
        /// </summary>
        /// <param name="post">post data content</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public ResponseResult<Post> Add(Post post) => _postAppService.Add(post);

        /// <summary>
        /// Update a post (only writer)
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public ResponseResult<Post> Update(Post post) => _postAppService.Update(post);         
        #endregion
    }
}
