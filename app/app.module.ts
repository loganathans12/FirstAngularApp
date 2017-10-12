import { AppErrorHandler } from './app-error-handler';
import { ContactService } from './services/contact.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'hammerjs';

import { AppComponent } from './app.component';
import { ContactsComponent } from './contacts/contacts.component';
import { FavoritePipe } from './pipes/favorite.pipe';

@NgModule({
  declarations: [
    AppComponent,
    ContactsComponent,
    FavoritePipe
  ],
  imports: [
    BrowserModule,
    HttpModule,
    BrowserAnimationsModule,
  ],
  providers: [
    ContactService,
    { provide: ErrorHandler, useClass: AppErrorHandler }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
