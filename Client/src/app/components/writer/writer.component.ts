import { Component, OnInit } from '@angular/core';
import { PostModel } from 'src/app/Models/post.model';
import { PersonModel } from 'src/app/Models/person.model';
import { PersonService } from 'src/app/services/person.service';
import { PostService } from 'src/app/services/post.service';
import { ResponseResult } from 'src/app/Models/result.models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-writer',
  templateUrl: './writer.component.html',
  styleUrls: ['./writer.component.css']
})
export class WriterComponent implements OnInit {

  postTitle: string;
  postContent: string;

  signedPerson: PersonModel;

  constructor(
    private personService: PersonService, 
    private postService: PostService,
    private router: Router) { }

  ngOnInit(): void {}

  onSubmit() {
    this.signedPerson = this.personService.getPersonOnline();
    let postData: PostModel  = {
      id: null,
      idAuthor: this.signedPerson.id,
      postContent: this.postContent,
      postTitle: this.postTitle,
      postDate: new Date(),
      comments: [],
      commentCount: 0,
      state: 0,
      author: this.signedPerson.fullName
    }

    this.postService.savePost(postData)
      .subscribe((data:ResponseResult<PostModel>) => {
        if (data.isSuccessful) {
          this.router.navigate(['timeline']);
        }
      }, er => console.log(er));
  }
}
