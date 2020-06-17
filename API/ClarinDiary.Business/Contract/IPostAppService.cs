using ClarinDiary.Business.DTO;
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
        ResponseResult<Post> Add(PostDTO post);

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
        ResponseResult<IEnumerable<PostDTO>> GetPostPending();

        /// <summary>
        /// list approved publications
        /// </summary>
        /// <returns></returns>
        ResponseResult<IEnumerable<PostDTO>> GetPostedOn();

        /// <summary>
        /// lists the posts made by a writer
        /// </summary>
        /// <param name="writerId"></param>
        /// <returns></returns>
        ResponseResult<IEnumerable<Post>> GetPostByWritter(Guid writerId);

        /// <summary>
        /// Translate the content of a post on demand
        /// </summary>
        /// <param name="postId">id post</param>
        /// <param name="langOrigin">language origin</param>
        /// <param name="langTarget">language target</param>
        /// <returns></returns>
        ResponseResult<PostDTO> GetTraslatePost(Guid postId, string langOrigin, string langTarget);
    }
}
