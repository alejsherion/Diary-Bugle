import { Component, OnInit } from '@angular/core';
import { PostModel } from 'src/app/Models/post.model';
import { PostService } from 'src/app/services/post.service';
import { ResponseResult } from 'src/app/Models/result.models';
import { PersonService } from 'src/app/services/person.service';
import { PostStatesEnum } from 'src/app/app.enums';
import { Router } from '@angular/router';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css']
})
export class EditorComponent implements OnInit {

  pendingPost: PostModel[];

  constructor(
    private postService: PostService, 
    private personService: PersonService,
    private router: Router) { }

  ngOnInit(): void {
    this.getPendingPost();
  }

  getPendingPost() {
    this.postService.getPostPending()
      .subscribe((data: ResponseResult<PostModel[]>) => {
        if (data.isSuccessful) {
          this.pendingPost = data.result;
        } else {
          console.log(data.message);
        }
      }, er => console.log(er));
  }

  appovedPost = (post) => {
    this.setPostStatus(post.id, PostStatesEnum.Approved);
  }

  rejectedPost = (post: PostModel) => {
    this.setPostStatus(post.id, PostStatesEnum.Rejected);
  }

  setPostStatus(postId, state: PostStatesEnum) {
    let signedPerson = this.personService.getPersonOnline();
    
    this.postService.setStatusPost(postId, signedPerson.id, state)
      .subscribe((data: ResponseResult<PostModel>) => {
        if (data.isSuccessful) {
          this.router.navigate(['timeline']);
        } else {
          console.log(data.message);
        }
      }, er => console.log(er));
  }
}
