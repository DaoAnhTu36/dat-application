import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';

@NgModule({
  declarations: [],
  imports: [CommonModule, DashboardRoutingModule],
  providers: [provideCharts(withDefaultRegisterables())],
})
export class DashboardModule {}
