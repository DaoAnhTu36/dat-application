// src/app/state/customer.reducer.ts
import { createReducer, on } from '@ngrx/store';
import { initialCustomerState } from '../models';
import { loadCustomers, addCustomer, updateCustomer, deleteCustomer, selectCustomer } from './actions';

export const customerReducer = createReducer(
    initialCustomerState,
    on(loadCustomers, (state, { customers }) => ({ ...state, customers })),
    on(addCustomer, (state, { customer }) => ({
        ...state,
        customers: [...state.customers, customer],
    })),
    on(updateCustomer, (state, { customer }) => ({
        ...state,
        customers: state.customers.map((c) =>
            c.id === customer.id ? { ...customer } : c
        ),
    })),
    on(deleteCustomer, (state, { customerId }) => ({
        ...state,
        customers: state.customers.filter((c) => c.id !== customerId),
    })),
    on(selectCustomer, (state, { customer }) => ({
        ...state,
        selectedCustomer: customer,
    }))
);
