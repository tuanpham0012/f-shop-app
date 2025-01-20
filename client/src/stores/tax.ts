import { defineStore } from "pinia";
import { _getList, _create, _update, _show } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useTaxStore = defineStore("tax", {
    state: () => {
        return {
            entries: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            entry: null,
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${apiUrl}/taxes`, query)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${apiUrl}/taxes`, data);
        },
        async update(id:any, data: any) {
            return await _update(`${apiUrl}/taxes/${id}`, data);
        },
        async show(id: any) {
            await _show(`${apiUrl}/taxes/${id}`)
                .then((res) => {
                    console.log(res.data);
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
});
