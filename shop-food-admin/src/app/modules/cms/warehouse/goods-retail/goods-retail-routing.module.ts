import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GoodsRetailIndexComponent } from './goods-retail-index/goods-retail-index.component';
import { GoodsRetailUpdateComponent } from './goods-retail-update/goods-retail-update.component';

const routes: Routes = [
  { path: '', component: GoodsRetailIndexComponent },
  { path: 'index', component: GoodsRetailIndexComponent },
  { path: 'update/:id', component: GoodsRetailUpdateComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GoodsRetailRoutingModule {}
