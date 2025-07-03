import { defineStore } from "pinia";
import { _getList, _create, _update, _show, _destroy } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useUserStore = defineStore("user", {
    state: () => {
        return {
            deliveryAddresses: {
                code: 200,
                message: "",
                data: [] as any[],
                meta: null as any,
            },
            deliveryAddresse: null as any,
        };
    },

    actions: {
        async getListDeliveryAddresses(query: any) {
            await _getList(`${apiUrl}/user/delivery/get`, query)
                .then((res) => {
                    this.deliveryAddresses = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async createDeliveryAddresses(data: any) {
            return await _create(`${apiUrl}/user/delivery/create`, data);
        },
        async updateDeliveryAddresses(id:any, data: any) {
            return await _update(`${apiUrl}/user/delivery/${id}`, data);
        },
        async show(id: any) {
            await _show(`${apiUrl}/user/delivery/${id}`)
                .then((res) => {
                    this.deliveryAddresse = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async delete(id: any) {
                    return await _destroy(`${apiUrl}/taxes/${id}`);
                },
    },
});
