import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PersonModel } from '../Models/person.model';
import { Observable } from 'rxjs';
import { URL_API } from './urlHelper';

@Injectable()
export class PersonService {

    personOnLine: PersonModel;

    constructor(protected httpClient: HttpClient) { }

    setPersonOnline(person: PersonModel) {
        this.personOnLine = person;
    }
    getPersonOnline() {
        return this.personOnLine;
    }
    
    getPersonById(id: string) {
        return this.httpClient.get(`${URL_API}api/Person/GetById?id=${id}`);
    }
    
    getPersonByIdentification(identification: string) {
        console.log(`${URL_API}api/Person/GetByIdentification?identification=${identification}`)
        return this.httpClient.get(`${URL_API}api/Person/GetByIdentification?identification=${identification}`);
    }

}