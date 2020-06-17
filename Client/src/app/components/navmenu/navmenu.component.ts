import { Component, OnInit, Input } from '@angular/core';
import { PersonModel } from 'src/app/Models/person.model';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavmenuComponent implements OnInit {

  @Input() signedPerson: PersonModel;

  constructor() { }

  ngOnInit(): void {
  }

}
