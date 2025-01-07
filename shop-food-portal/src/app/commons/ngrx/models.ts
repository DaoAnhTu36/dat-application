export interface CustomerInfoDTO {
}

export interface CustomerState {
    customers: Customer[];
    selectedCustomer: Customer | null;
}

export interface Customer {
    id: number;
    name: string | undefined;
    email: string | undefined;
    accessToken: string | undefined;
    expiredDate: Date | undefined;
}

export const initialCustomerState: CustomerState = {
    customers: [],
    selectedCustomer: null,
};

export interface CustomerInformationDTO {
    name: string | undefined;
    email: string | undefined;
    accessToken: string | undefined;
    expiredDate: Date | undefined;
}