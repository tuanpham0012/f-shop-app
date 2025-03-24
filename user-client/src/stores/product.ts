import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useProductStore = defineStore("product", {
    state: () => {
        return {
            newProducts: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            featuredProducts: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            listProductByCategory: {
                code: 200,
                message: "",
                data: [],
                meta: null as any
            }
        };
    },

    actions: {
        async getListNewProduct(query: any) {
            await _getList(`${apiUrl}/products/new-product`, query)
                .then((res) => {
                    this.newProducts = res.data
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getListFeaturedProduct(query: any) {
            await _getList(`${apiUrl}/products/featured-product`, query)
                .then((res) => {
                    this.featuredProducts = res.data
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async getListProductByCategory(categoryCode:string, query: any) {
            await _getList(`${apiUrl}/products/${categoryCode}`, query)
                .then((res) => {
                    this.listProductByCategory = res.data
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
})
