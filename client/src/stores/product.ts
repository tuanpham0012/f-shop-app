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


export const useProductStore = defineStore("product", {
    state: () => {
        return {
            entries: {
                code: 200,
                message: "",
                data: [] as any[],
                meta: null as any,
            },
            entry: null as any,
            errors: null,
            descriptionProduct: '' as any,
            searchProduct: {
                code: 200,
                message: "",
                data: [] as any[],
                meta: null as any,
                loading: false,
                loadFull: false,
            },
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${adminUrl}/products`, query)
                .then((res) => {
                    this.entries = res.data
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${adminUrl}/products`, data);
        },
        async show(id: any, loadDescription = false) {
            this.entry = null
            await _show(`${adminUrl}/products/${id}`)
                .then( async (res) => {
                    this.entry = res.data.data;
                    await this.getSkuProduct(id)
                    if(loadDescription){
                        await this.getDescriptionProductById(id)
                    }
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

        async getDescriptionProductById(id:any) {
            await _show(`${adminUrl}/products/description/${id}`)
                .then((res) => {
                    this.descriptionProduct = res.data
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async updateDes(id:any, data: any) {
            
            return await _update(`${adminUrl}/products/description/${id}`, data);
        },

        async getSkuProduct(id:any) {
                    await _show(`${adminUrl}/products/sku/${id}`)
                        .then((res) => {
                            this.entry.options = res.data.data?.options
                            this.entry.skus = res.data.data?.skus
                        })
                        .catch((err) => {
                            console.log(err);
                        });
                },
        async getListSearchProduct(search: any) {
            if(this.searchProduct.loadFull) {
                return;
            }
            this.searchProduct.loading = true;
            if(!search || search.length < 1) {
                return;
            }
            await _getList(`${adminUrl}/products/search`,  search)
                .then((res:any) => {
                    if(res.data.data.length > 0) {
                        this.searchProduct.data.push(...res.data.data);
                        this.searchProduct.loadFull = false;
                    }else {
                        this.searchProduct.loadFull = true;
                    }
                })
                .catch((err) => {
                    console.log(err);
                }).finally(() => {
                    this.searchProduct.loading = false;
                });
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
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async getListTree(query: any) {
            await _getList(`${adminUrl}/categories/get-tree`, query)
                .then((res) => {
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
    state: () => {
        return {
            entries: {
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
            await _getList(`${adminUrl}/brands`, query)
                .then((res) => {
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
