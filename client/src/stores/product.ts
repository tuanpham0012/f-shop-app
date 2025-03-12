import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

const adminUrl = apiUrl + "/admin";

interface State {
    entries: any | [];
    entry: any | null;
    errors: any | null;
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
            entry: null,
            errors: null,
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${adminUrl}/products`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${adminUrl}/products`, data);
        },
        async show(id: any) {
            this.entry = null
            await _show(`${adminUrl}/products/${id}`)
                .then((res) => {
                    this.entry = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async setEntryNull() {
            this.entry = null
        },
        async update(id: any, data: any) {
            return await _update(`${adminUrl}/products/${id}`, data);
        },
        async delete(id: any) {
            return await _destroy("http://localhost:5077/Customer/" + id);
        },
    },
});

export const useCategoryStore = defineStore("category", {
    state: () => {
        return {
            entries: {
                code: 200,
                message: "",
                data: [],
                meta: null as any,
            },
            listTree: {
                code: 200,
                message: "",
                data: [],
                meta: null as any,
            },
            entry: null,
            errors: null,
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${adminUrl}/categories`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getListTree(query: any) {
            await _getList(`${adminUrl}/categories/get-tree`, query)
                .then((res) => {
                    console.log(res.data);
                    this.listTree = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${adminUrl}/categories`, data);
        },
        async show(id: any) {
            this.entry = null
            await _show(`${adminUrl}/categories/${id}`)
                .then((res) => {
                    this.entry = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async setEntryNull() {
            this.entry = null
        },
        async update(id: any, data: any) {
            return await _update(`${adminUrl}/categories/${id}`, data);
        },
        async delete(id: any) {
            return await _destroy(`${adminUrl}/categories/${id}`);
        },
    },
});

export const useBrandStore = defineStore("brand", {
    state: ():State => {
        return {
            entries: {
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
            await _getList(`${adminUrl}/brands`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${adminUrl}/brands`, data);
        },
        async update(id:any, data: any) {
            return await _update(`${adminUrl}/brands/${id}`, data);
        },
        async show(id: any) {
            await _show(`${adminUrl}/brands/${id}`)
                .then((res) => {
                    console.log(res.data);
                    this.entry = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async delete(id:any){
            return await _destroy(`${adminUrl}/brands/${id}`);
        }
    },
});
