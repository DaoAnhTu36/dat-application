import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierCreateComponent } from './supplier-create/supplier-create.component';
import { SupplierDeleteComponent } from './supplier-delete/supplier-delete.component';
import { SupplierIndexComponent } from './supplier-index/supplier-index.component';
import { SupplierUpdateComponent } from './supplier-update/supplier-update.component';
import { SupplierDetailComponent } from './supplier-detail/supplier-detail.component';

const routes: Routes = [
  { path: '', component: SupplierIndexComponent },
  { path: 'index', component: SupplierIndexComponent },
  { path: 'create', component: SupplierCreateComponent },
  { path: 'update/:id', component: SupplierUpdateComponent },
  { path: 'delete/:id', component: SupplierDeleteComponent },
  { path: 'detail/:id', component: SupplierDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupplierRoutingModule { }
