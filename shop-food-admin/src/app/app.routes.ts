import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./modules/cms/warehouse/warehouse.module').then(
        (m) => m.WarehouseModule
      ),
  },
  {
    path: 'wh',
    loadChildren: () =>
      import('./modules/cms/warehouse/warehouse.module').then(
        (m) => m.WarehouseModule
      ),
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
];
