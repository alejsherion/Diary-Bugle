import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { MsalGuard } from '@azure/msal-angular';
import { TimelineComponent } from './components/timeline/timeline.component';
import { WriterComponent } from './components/writer/writer.component';
import { EditorComponent } from './components/editor/editor.component';

const routes: Routes = [
    { path: 'timeline', component: TimelineComponent },
    { path: 'editor', component: EditorComponent, canActivate: [MsalGuard] },
    { path: 'writer', component: WriterComponent, canActivate: [MsalGuard] },
    { path: '**', component: TimelineComponent, canActivate: [MsalGuard] },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: false })],
    exports: [RouterModule]
})
export class AppRoutingModule { }