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
    entries: any | [];
    entry: any | null;
    errors: any | null;
    categories: any | [],
    suppliers: any
}


export const useProductStore = defineStore("product", {
    state: (): State => {
        return {
            entries: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            categories: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            suppliers: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            entry: null,
            errors: null,
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${apiUrl}/products`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getListCategory(query: any) {
            await _getList(`${apiUrl}/categories`, query)
                .then((res) => {
                    console.log(res.data);
                    this.categories = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getListSupplier(query: any) {
            await _getList(`${apiUrl}/suppliers`, query)
                .then((res) => {
                    console.log(res.data);
                    this.suppliers = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${apiUrl}/products`, data);
        },
        show(id: any) {
            _show(`${apiUrl}/products/${id}`)
                .then((res) => {
                    this.entry = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async update(id: any, data: any) {
            return await _update("http://localhost:5077/Customer/" + id, data);
        },
        async delete(id: any) {
            return await _destroy("http://localhost:5077/Customer/" + id);
        },
    },
});
