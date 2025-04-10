import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

interface State {
    customers: Customers;
    customer: Customer | null;
    errors: any | null;
}

interface Customer {
    id: number;
    name: string;
    email: string;
    emailVerifiedAt: string | null;
    password: string | null;
    phone: string;
    address: string;
    createdAt: string | null;
    updatedAt: string | null;
    status: number;
    gender: number;
    carts: Array<any>;
    orders: Array<any>;
}

interface Customers {
    code: number | null;
    message: string | null;
    data: Array<any>;
    meta: any | null;
    items: Customer[];
}

export const useCustomerStore = defineStore("customer", {
    state: (): State => {
        return {
            customers: {
                code: 200,
                message: "",
                data: [] as Customer[],
                meta: null,
            } as Customers,
            customer: null,
            errors: null,
        };
    },

    actions: {
        getList(query: any) {
            _getList(`${apiUrl}/customers`, query)
                .then((res) => {
                    this.customers = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${apiUrl}/customers`, data);
        },
        show(id: any) {
            _show(`${apiUrl}/customers/${id}`)
                .then((res) => {
                    this.customer = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async update(id: any, data: any) {
            return await _update(`${apiUrl}/customers/${id}`, data);
        },
        async delete(id: any) {
            return await _destroy(`${apiUrl}/customers/${id}`);
        },
    },
});
