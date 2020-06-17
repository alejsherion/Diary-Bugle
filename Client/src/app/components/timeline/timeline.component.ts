import { Component, OnInit } from '@angular/core';
import { PostModel } from 'src/app/Models/post.model';
import { PostService } from 'src/app/services/post.service';
import { ResponseResult } from 'src/app/Models/result.models';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  styleUrls: ['./timeline.component.css']
})
export class TimelineComponent implements OnInit {

  publications: PostModel[];

  postSelected: PostModel = null;

  constructor(protected postService: PostService) { }

  ngOnInit(): void {
    this.listPost();
  }

  listPost() {
    this.postService.getPostedOn()
      .subscribe((data: ResponseResult<PostModel[]>) => {
        if (data.isSuccessful) {
          this.publications = data.result;
        } else {
          console.log(data.message);
        }
      }, er => console.log(er)
      );
  }

  selectPost = (publication) => {
    this.postSelected = publication;
  }

}
