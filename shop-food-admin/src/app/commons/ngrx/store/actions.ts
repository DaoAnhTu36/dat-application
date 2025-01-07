// src/app/state/customer.actions.ts
import { createAction, props } from '@ngrx/store';
import { Customer } from '../models';

export const loadCustomers = createAction('[Customer API] Load Customers', props<{ customers: Customer[] }>());
export const addCustomer = createAction('[Customer Form] Add Customer', props<{ customer: Customer }>());
export const updateCustomer = createAction('[Customer Form] Update Customer', props<{ customer: Customer }>());
export const deleteCustomer = createAction('[Customer List] Delete Customer', props<{ customerId: number }>());
export const selectCustomer = createAction('[Customer List] Select Customer', props<{ customer: Customer }>());
