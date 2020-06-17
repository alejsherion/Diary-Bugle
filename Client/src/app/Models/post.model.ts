import { CommentModel } from './comment.model';

export interface PostModel{
    id: string;
    state: number;
    postContent: string;
    postDate: Date;
    postTitle: string;
    author: string;
    idAuthor: string;
    idPublisher?: string;
    commentCount: number;

    comments: CommentModel[];
}