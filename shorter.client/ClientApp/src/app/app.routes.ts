import { Routes } from '@angular/router';
import { ShortlinksComponent } from './shortlinks/shortlinks.component';
import { RedirectComponent } from './redirect/redirect.component';

export const routes: Routes = [
  { path: 'shortlinks', component: ShortlinksComponent },
  { path: ':shortUrl', component: RedirectComponent },
  { path: '', redirectTo: '/shortlinks', pathMatch: 'full' }
];
