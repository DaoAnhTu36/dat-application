// src/app/state/customer.selectors.ts
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { CustomerState } from '../models';

export const selectCustomerState = createFeatureSelector<CustomerState>('customerState');

export const selectAllCustomers = createSelector(
    selectCustomerState,
    (state) => state.customers
);

export const selectSelectedCustomer = createSelector(
    selectCustomerState,
    (state) => state.selectedCustomer
);
