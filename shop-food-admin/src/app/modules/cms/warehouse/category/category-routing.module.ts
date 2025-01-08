import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryDeleteComponent } from './category-delete/category-delete.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { CategoryIndexComponent } from './category-index/category-index.component';
import { CategoryUpdateComponent } from './category-update/category-update.component';
import { CategoryCreateComponent } from './category-create/category-create.component';

const routes: Routes = [
  { path: '', component: CategoryIndexComponent },
  { path: 'index', component: CategoryIndexComponent },
  { path: 'create', component: CategoryCreateComponent },
  { path: 'update/:id', component: CategoryUpdateComponent },
  { path: 'delete/:id', component: CategoryDeleteComponent },
  { path: 'detail/:id', component: CategoryDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoryRoutingModule {}
