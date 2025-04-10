import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useCartStore = defineStore("cart", {
    state: () => {
        return {
            carts: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            pending: false,
        };
    },

    actions: {
        async getList() {
            const token = import.meta.env.VITE_VUE_APP_TOKEN;
            if (token) {
                await _getList(`${apiUrl}/cart`, null)
                    .then((res) => {
                        this.carts = res.data;
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            }
        },

        async updateCart(id: any, data: any) {
            this.pending = true;
            await _update(`${apiUrl}/cart/${id}`, data)
                .then((res) => {
                    this.getList();
                })
                .finally(() => {
                    this.pending = false;
                });
        },

        async addToCart(data: any) {
            await _create(`${apiUrl}/cart`, data).then((res) => {
                this.getList();
            });
        },
    },
});
