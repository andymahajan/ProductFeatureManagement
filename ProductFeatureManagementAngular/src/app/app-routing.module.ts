import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeatureListComponent } from './components/features/feature-list/feature-list.component';
import { FeatureFormComponent } from './components/features/feature-form/feature-form.component';

const routes: Routes = [
  { path: '', redirectTo: 'features/list', pathMatch: 'full' },
  { path: 'features/list', component: FeatureListComponent },
  { path: 'features/create', component: FeatureFormComponent },
  { path: 'features/edit/:id', component: FeatureFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
