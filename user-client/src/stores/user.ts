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
            deliveryAddress: null as any,
        };
    },

    actions: {
        async getListDeliveryAddresses(query: any) {
            await _getList(`${apiUrl}/user/delivery-address/get-list`, query)
                .then((res) => {
                    this.deliveryAddresses = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async createDeliveryAddresses(data: any) {
            return await _create(`${apiUrl}/user/delivery-address/create`, data);
        },
        async updateDeliveryAddresses(id:any, data: any) {
            return await _update(`${apiUrl}/user/delivery-address/update/${id}`, data);
        },
        async getDeliveryAddress(id: any) {
            if(!id) {
                this.deliveryAddress = null;
                return;
            }
            await _show(`${apiUrl}/user/delivery-address/edit/${id}`)
                .then((res) => {
                    this.deliveryAddress = res.data.data;
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
