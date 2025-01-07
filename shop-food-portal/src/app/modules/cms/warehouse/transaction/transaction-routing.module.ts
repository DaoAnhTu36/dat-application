import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransactionCreateComponent } from './transaction-create/transaction-create.component';
import { TransactionDeleteComponent } from './transaction-delete/transaction-delete.component';
import { TransactionIndexComponent } from './transaction-index/transaction-index.component';
import { TransactionUpdateComponent } from './transaction-update/transaction-update.component';
import { TransactionDetailComponent } from './transaction-detail/transaction-detail.component';
import { TransactionImportComponent } from './transaction-import/transaction-import.component';
import { TransactionExportComponent } from './transaction-export/transaction-export.component';

const routes: Routes = [
  { path: '', component: TransactionIndexComponent },
  { path: 'index', component: TransactionIndexComponent },
  { path: 'create', component: TransactionCreateComponent },
  { path: 'update/:id', component: TransactionUpdateComponent },
  { path: 'delete/:id', component: TransactionDeleteComponent },
  { path: 'detail/:id', component: TransactionDetailComponent },
  { path: 'import', component: TransactionImportComponent },
  { path: 'export', component: TransactionExportComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TransactionRoutingModule {}
