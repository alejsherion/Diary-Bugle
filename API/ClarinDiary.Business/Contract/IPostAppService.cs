using ClarinDiary.Business.Enums;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace ClarinDiary.Business.Contract
{
    public interface IPostAppService
    {
        /// <summary>
        /// Add a post (only writer)
        /// </summary>
        /// <param name="post">post data content</param>
        /// <returns></returns>
        ResponseResult<Post> Add(Post post);

        /// <summary>
        /// Update a post (only writer)
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        ResponseResult<Post> Update(Post post);

        /// <summary>
        /// Update the status for a post (only editor)
        /// </summary>
        /// <param name="postid">id post</param>
        /// <param name="editorId">id editor that updates</param>
        /// <param name="state">status indicated for post</param>
        /// <returns></returns>
        ResponseResult<Post> SetStatusPost(Guid postid, Guid editorId, PostStatusEnum state);

        /// <summary>
        /// lists the publications that are pending approval
        /// </summary>
        /// <returns></returns>
        ResponseResult<IEnumerable<Post>> GetPostPending();

        /// <summary>
        /// list approved publications
        /// </summary>
        /// <returns></returns>
        ResponseResult<IEnumerable<Post>> GetPostedOn();

        /// <summary>
        /// lists the posts made by a writer
        /// </summary>
        /// <param name="writerId"></param>
        /// <returns></returns>
        ResponseResult<IEnumerable<Post>> GetPostByWritter(Guid writerId);
    }
}
