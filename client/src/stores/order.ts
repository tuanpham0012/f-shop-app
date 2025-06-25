import { defineStore } from "pinia";
import { _getList, _create, _update, _show, _destroy } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

const adminUrl = apiUrl + "/admin";

export const useOrderStore = defineStore("order", {
    state: () => {
        return {
            entries: {
                code: 200,
                message: "",
                data: [] as any[],
                meta: null as any,
            },
            entry: null as any,
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${adminUrl}/orders`, query)
                .then((res) => {
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${adminUrl}/orders`, data);
        },
        async update(id: any, data: any) {
            return await _update(`${apiUrl}/taxes/${id}`, data);
        },
        async show(id: any) {
            await _show(`${adminUrl}/orders/${id}`)
                .then((res) => {
                    this.entry = res.data.data;
                    this.getOrderDetails(id);
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getOrderDetails(id: any) {
            await _show(`${adminUrl}/orders/order-details/${id}`)
                .then((res) => {
                    if (this.entry) {
                        this.entry.orderDetails = res.data.data;
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async delete(id: any) {
            return await _destroy(`${apiUrl}/taxes/${id}`);
        },

        async changeOrderStatus(data: any) {
            return await _create(`${adminUrl}/orders/change-order-status`, data);
        }
    },
});
