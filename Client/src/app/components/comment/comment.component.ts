import { Component, OnInit, Input } from '@angular/core';
import { CommentModel } from 'src/app/Models/comment.model';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {

  @Input() commentary: CommentModel;

  constructor() { }

  ngOnInit(): void {
  }

}
