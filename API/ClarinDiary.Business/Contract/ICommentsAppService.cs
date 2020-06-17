using ClarinDiary.Business.DTO;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace ClarinDiary.Business.Contract
{
    public interface ICommentsAppService
    {
        /// <summary>
        /// Add a comment in a post
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        ResponseResult<dynamic> Add(PostCommentDTO postComment);

        /// <summary>
        /// Remove a comment
        /// </summary>
        /// <param name="postComment"></param>
        /// <returns></returns>
        ResponseResult<dynamic> Remove(Guid commentId);

        /// <summary>
        /// list comments on a post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        ResponseResult<IEnumerable<PostCommentDTO>> GetCommentsByPost(Guid postId);
    }
}
