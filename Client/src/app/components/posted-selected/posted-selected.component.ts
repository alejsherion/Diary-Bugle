import { Component, OnInit, Input } from '@angular/core';
import { PostModel } from 'src/app/Models/post.model';
import { PostService } from 'src/app/services/post.service';
import { ResponseResult } from 'src/app/Models/result.models';
import { PersonService } from 'src/app/services/person.service';
import { PersonModel } from 'src/app/Models/person.model';
import { CommentModel } from 'src/app/Models/comment.model';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-posted-selected',
  templateUrl: './posted-selected.component.html',
  styleUrls: ['./posted-selected.component.css']
})
export class PostedSelectedComponent implements OnInit {

  languageOptions = [
    { value: 'es', label: 'Español', img: './'},
    { value: 'en', label: 'English', img: './'},
    { value: 'de', label: 'Deutsche', img: './'},
    { value: 'ja', label: '日本語', img: './'}
  ]

  @Input() postSelected: PostModel;
  person: PersonModel;

  openCommentForm: boolean = false;
  selectedLanguage: string;
  lastLanguague: string;
  postComment: string;

  constructor(
    private postService: PostService, 
    private personService: PersonService,
    private commentService: CommentService) { }

  ngOnInit(): void {
    this.selectedLanguage = this.lastLanguague = 'es'; //default language    
    this.getPersonOnline();
    this.listComments();
  }

  clickon() {
    this.openCommentForm = !this.openCommentForm;
  }

  getPersonOnline = () => {
    this.person = this.personService.getPersonOnline();
  }

  selectechange(e) {
    let selected = e.target.value;
    this.postService.getTraslatePost(this.postSelected.id, this.lastLanguague, selected)
      .subscribe((data: ResponseResult<PostModel>) => {
        if (data.isSuccessful) {
          this.postSelected = data.result;
          this.lastLanguague = selected;
        } else {
          console.log(data.message);
        }
      }, er => console.log(er));
  }

  onSubmit() {
    let comment: CommentModel = {
      id: null,
      idPerson: this.person.id,
      idPost: this.postSelected.id,
      author: this.person.fullName,
      comment: this.postComment
    }

    this.commentService.saveComment(comment)
      .subscribe((data:ResponseResult<PostModel>) => {
        if (data.isSuccessful) {
          this.listComments();
        } else {
          console.log(data.message);
        }
      }, er => console.log(er));
  }

  listComments() {
    this.openCommentForm = false;

    this.commentService.getCommentsByPost(this.postSelected.id)
      .subscribe((data: ResponseResult<CommentModel[]>) => {
        if (data.isSuccessful) {
          this.postSelected.comments = data.result;
          this.postSelected.commentCount = data.result.length;
        } else {
          console.log(data.message);
        }
      }, er => console.log(er));
  }
}
