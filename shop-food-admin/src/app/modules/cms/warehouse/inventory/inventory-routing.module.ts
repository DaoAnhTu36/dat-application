import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventoryIndexComponent } from './inventory-index/inventory-index.component';
import { InventoryDeleteComponent } from './inventory-delete/inventory-delete.component';
import { InventoryUpdateComponent } from './inventory-update/inventory-update.component';
import { InventoryCreateComponent } from './inventory-create/inventory-create.component';
import { InventoryDetailComponent } from './inventory-detail/inventory-detail.component';

const routes: Routes = [
  { path: '', component: InventoryIndexComponent },
  { path: 'index', component: InventoryIndexComponent },
  { path: 'create', component: InventoryCreateComponent },
  { path: 'update/:id', component: InventoryUpdateComponent },
  { path: 'delete/:id', component: InventoryDeleteComponent },
  { path: 'detail/:id', component: InventoryDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InventoryRoutingModule {}
