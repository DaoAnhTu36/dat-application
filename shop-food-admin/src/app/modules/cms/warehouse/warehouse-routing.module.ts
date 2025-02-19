import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('../dashboard/dashboard.module').then((m) => m.DashboardModule),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('../dashboard/dashboard.module').then((m) => m.DashboardModule),
  },
  {
    path: 'wh/transaction/inventory',
    loadChildren: () =>
      import('./inventory/inventory.module').then((m) => m.InventoryModule),
  },
  {
    path: 'wh/transaction/transaction',
    loadChildren: () =>
      import('./transaction/transaction.module').then(
        (m) => m.TransactionModule
      ),
  },
  {
    path: 'wh/transaction',
    loadChildren: () =>
      import('./transaction/transaction.module').then(
        (m) => m.TransactionModule
      ),
  },
  {
    path: 'wh/system/goods',
    loadChildren: () =>
      import('./product/product.module').then((m) => m.ProductModule),
  },
  {
    path: 'wh/system/supplier',
    loadChildren: () =>
      import('./supplier/supplier.module').then((m) => m.SupplierModule),
  },
  {
    path: 'wh/system/unit',
    loadChildren: () => import('./unit/unit.module').then((m) => m.UnitModule),
  },
  {
    path: 'wh/system/stock',
    loadChildren: () =>
      import('./warehouse/warehouse.module').then((m) => m.WarehouseModule),
  },
  {
    path: 'wh/system/category',
    loadChildren: () =>
      import('./category/category.module').then((m) => m.CategoryModule),
  },
  {
    path: 'wh/transaction/goods-retail',
    loadChildren: () =>
      import('./goods-retail/goods-retail.module').then(
        (m) => m.GoodsRetailModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WarehouseRoutingModule {}
