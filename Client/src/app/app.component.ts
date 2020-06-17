import { Component } from '@angular/core';
import { PersonModel } from './Models/person.model';
import { PersonService } from './services/person.service';
import { ResponseResult } from './Models/result.models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Daily Bugle';

  // Set dafault person
  person: PersonModel = {
    fullName: "Anonymus",
    id: '',
    idRol: '',
    identification: '111111',
    rolCode: '3'
  };

  constructor(private personService: PersonService) { }

  getPerson = (id) => {
    this.personService.getPersonById(id)
      .subscribe((data: ResponseResult<PersonModel>) => {
        console.log(data)
        if (data.isSuccessful) {
          this.person = data.result;
        } else {
          console.log(data.message);
        }
      }, er => console.log(er));
  }

  setSignPerson(username) {
    this.personService.getPersonByIdentification(username)
      .subscribe((data: ResponseResult<PersonModel>) => {
        if (data.isSuccessful) {
          this.person = data.result;
          this.personService.setPersonOnline(this.person);
        } else {
          console.log(data.message)
        }
      }, er => console.log(er));
  }

}
