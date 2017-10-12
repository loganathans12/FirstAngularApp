import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'favorite', pure:false })
export class FavoritePipe implements PipeTransform {

  transform(contacts: any[], isFavorite: boolean) {
    if(contacts == null) return null;

    let filteredContacts = contacts.filter(contact => contact.isFavorite == isFavorite);

    if (filteredContacts != null && filteredContacts.length > 0) {
      filteredContacts.sort(function (a, b) {
        var nameA = a.name.toUpperCase(); // ignore upper and lowercase
        var nameB = b.name.toUpperCase(); // ignore upper and lowercase
        if (nameA < nameB) {
          return -1;
        }
        if (nameA > nameB) {
          return 1;
        }

        // names must be equal
        return 0;
      });
    }

    return filteredContacts;

    

  }
}
