import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

const url = apiUrl

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
            popularCategory: {
                code: 200,
                message: "",
                data: [],
                meta: null as any,
            },
            errors: null,
        };
    },

    actions: {

        async getListPopularCategory() {
            await _getList(`${url}/categories/popular-category`, null)
                .then((res) => {
                    console.log(res.data);
                    this.popularCategory = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        }
    },
});
