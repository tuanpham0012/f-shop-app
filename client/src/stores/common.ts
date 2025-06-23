import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useCommonStore = defineStore("common", {
    state: () => {
        return {
            paymentMethods: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            shippingUnits: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
        };
    },

    actions: {
        async getPaymentMethods() {
            await _getList(`${apiUrl}/master-data/payment-methods`, null)
                .then((res) => {
                    this.paymentMethods = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getShippingUnits() {
            await _getList(`${apiUrl}/master-data/shipping-units`, null)
                .then((res) => {
                    this.shippingUnits = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
});
