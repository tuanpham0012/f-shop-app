import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

const url = apiUrl + "/admin";

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
        async getListCategory(query: any) {
            await _getList(`${url}/categories/get-tree`, query)
                .then((res) => {
                    console.log(res.data);
                    this.listTree = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
});
