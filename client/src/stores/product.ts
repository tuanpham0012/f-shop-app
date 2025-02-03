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
            await _getList(`${apiUrl}/products`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data
                    this.entries?.data.map((item:any) => {
                        item.images = item.images ? JSON.parse(item.images) : []
                        return item
                    });
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        // async getListSupplier(query: any) {
        //     await _getList(`${apiUrl}/suppliers`, query)
        //         .then((res) => {
        //             console.log(res.data);
        //             this.suppliers = res.data;
        //         })
        //         .catch((err) => {
        //             console.log(err);
        //         });
        // },
        async create(data: any) {
            return await _create(`${apiUrl}/products`, data);
        },
        async show(id: any) {
            this.entry = null
            await _show(`${apiUrl}/products/${id}`)
                .then((res) => {
                    this.entry = res.data.data;
                    if(this.entry)
                    this.entry.images = this.entry.images ? JSON.parse(this.entry.images) : []
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async setEntryNull() {
            this.entry = null
        },
        async update(id: any, data: any) {
            return await _update(`${apiUrl}/products/${id}`, data);
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
            await _getList(`${apiUrl}/categories`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getListTree(query: any) {
            await _getList(`${apiUrl}/categories/get-tree`, query)
                .then((res) => {
                    console.log(res.data);
                    this.listTree = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${apiUrl}/categories`, data);
        },
        async show(id: any) {
            this.entry = null
            await _show(`${apiUrl}/categories/${id}`)
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
            return await _update(`${apiUrl}/categories/${id}`, data);
        },
        async delete(id: any) {
            return await _destroy(`${apiUrl}/categories/${id}`);
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
            await _getList(`${apiUrl}/brands`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${apiUrl}/brands`, data);
        },
        async update(id:any, data: any) {
            return await _update(`${apiUrl}/brands/${id}`, data);
        },
        async show(id: any) {
            await _show(`${apiUrl}/brands/${id}`)
                .then((res) => {
                    console.log(res.data);
                    this.entry = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async delete(id:any){
            return await _destroy(`${apiUrl}/brands/${id}`);
        }
    },
});
