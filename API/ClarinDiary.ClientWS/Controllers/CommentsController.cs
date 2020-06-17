using ClarinDiary.Business.Contract;
using ClarinDiary.Business.DTO;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ClarinDiary.ClientWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        #region Members
        private readonly ICommentsAppService _commentsAppService;
        #endregion

        #region Builder
        public CommentsController(ICommentsAppService commentsAppService) : base() => _commentsAppService = commentsAppService;
        #endregion

        #region Methods
        /// <summary>
        /// list comments on a post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentsByPost")]
        public ResponseResult<IEnumerable<PostCommentDTO>> GetCommentsByPost(Guid postId) => _commentsAppService.GetCommentsByPost(postId);

        /// <summary>
        /// Add a comment in a post
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public ResponseResult<dynamic> Add(PostCommentDTO postComment) => _commentsAppService.Add(postComment);

        /// <summary>
        /// Remove a comment
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Remove")]
        public ResponseResult<dynamic> Remove(Guid commentId) => _commentsAppService.Remove(commentId); 
        #endregion
    }
}
