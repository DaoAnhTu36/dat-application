import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseCreateComponent } from './warehouse-create/warehouse-create.component';
import { WarehouseDeleteComponent } from './warehouse-delete/warehouse-delete.component';
import { WarehouseIndexComponent } from './warehouse-index/warehouse-index.component';
import { WarehouseUpdateComponent } from './warehouse-update/warehouse-update.component';
import { WarehouseDetailComponent } from './warehouse-detail/warehouse-detail.component';

const routes: Routes = [
  { path: '', component: WarehouseIndexComponent },
  { path: 'index', component: WarehouseIndexComponent },
  { path: 'create', component: WarehouseCreateComponent },
  { path: 'update/:id', component: WarehouseUpdateComponent },
  { path: 'delete/:id', component: WarehouseDeleteComponent },
  { path: 'detail/:id', component: WarehouseDetailComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),],
  exports: [RouterModule]
})
export class WarehouseRoutingModule { }
