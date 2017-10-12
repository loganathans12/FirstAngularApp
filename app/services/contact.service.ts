import { DataService } from './data.service';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ContactService extends DataService {
   

  constructor(http:Http) { 
    super('https://s3.amazonaws.com/technical-challenge/v3/contacts.json', http);
  }

}
