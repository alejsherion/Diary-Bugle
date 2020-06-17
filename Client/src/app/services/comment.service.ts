import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { URL_API } from './urlHelper';
import { CommentModel } from '../Models/comment.model';

@Injectable()
export class CommentService {

    constructor(protected httpClient: HttpClient) { }

    saveComment(comment: CommentModel) {
        return this.httpClient.post(`${URL_API}api/Comments/Add`, comment);
    }

    getCommentsByPost(postId: string) {
        return this.httpClient.get(`${URL_API}api/Comments/GetCommentsByPost?postId=${postId}`);
    }
}