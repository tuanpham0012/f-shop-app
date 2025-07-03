import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";
import { provide } from "vue";

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
            provinces: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            wards: {
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
        async getProvinces() {
            await _getList(`${apiUrl}/master-data/provinces`, null)
                .then((res) => {
                    this.provinces = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getWards(provinceId: number) {
            await _getList(`${apiUrl}/master-data/wards/${provinceId}`, null)
                .then((res) => {
                    this.wards = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
});
