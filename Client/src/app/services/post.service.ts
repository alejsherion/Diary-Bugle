import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { URL_API } from './urlHelper';
import { PostModel } from '../Models/post.model';

@Injectable()
export class PostService {

    constructor(protected httpClient: HttpClient) { }

    getPostedOn() {
        return this.httpClient.get(`${URL_API}api/post/getpostedon`)            
    }

    getPostPending() {
        return this.httpClient.get(`${URL_API}api/post/GetPostPending`)
    }

    savePost(post: PostModel) {
        return this.httpClient.post(`${URL_API}api/Post/Add`, post);
    }

    setStatusPost(postId: string, editorId: string, state: number) {
        return this.httpClient.get(`${URL_API}api/Post/SetStatusPost?postid=${postId}&editorId=${editorId}&state=${state}`);
    }

    getTraslatePost(postId: string, langOrigin: string, langTarget: string) {
        return this.httpClient.get(`${URL_API}api/Post/GetTraslatePost?postid=${postId}&langOrigin=${langOrigin}&langTarget=${langTarget}`)
    }
}