import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// Routes
import { AppRoutingModule } from './app-routing.module';
 
// Services
import { PersonService } from './services/person.service'
import { PostService} from './services/post.service'

// Components
import { EditorComponent } from './components/editor/editor.component';
import { WriterComponent } from './components/writer/writer.component';
import { TimelineComponent } from './components/timeline/timeline.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { PostedSelectedComponent } from './components/posted-selected/posted-selected.component';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { CommentComponent } from './components/comment/comment.component';
import { LoginmodalComponent } from './components/loginmodal/loginmodal.component';
import { CommentService } from './services/comment.service';

const isIE = window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;

@NgModule({
  declarations: [
    AppComponent,
    TimelineComponent,
    EditorComponent,
    WriterComponent,
    NavbarComponent,
    PostedSelectedComponent,
    NavmenuComponent,
    CommentComponent,
    LoginmodalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    MsalModule.forRoot({
      auth: {
        clientId: 'f69d0d16-2e6c-47b6-bb47-37c4bcbf310a',
        authority: 'https://login.microsoftonline.com/f9c481f1-e606-423a-8571-e01710ad6334',
        redirectUri: 'https://dailybuglegluky.azurewebsites.net/',
      },
      cache: {
        cacheLocation: 'localStorage',
        storeAuthStateInCookie: isIE, // set to true for IE 11
      },
    },
    {
      popUp: !isIE,
      consentScopes: [
        'user.read',
        'openid',
        'profile',
      ],
      unprotectedResources: [],
      protectedResourceMap: [
        ['https://graph.microsoft.com/v1.0/me', ['user.read']]
      ],
      extraQueryParameters: {}
    })
  ],
  providers: [
    PersonService,
    PostService,
    CommentService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
