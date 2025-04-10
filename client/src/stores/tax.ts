import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _update,
    _show,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useTaxStore = defineStore("tax", {
    state: () => {
        return {
            entries: {
                code: 200,
                message: "",
                data: [] as any[],
                meta: null as any,
            },
            entry: null,
        };
    },

    actions: {
        async getList(query: any) {
            await _getList(`${apiUrl}/taxes`, query)
                .then((res) => {
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async create(data: any) {
            return await _create(`${apiUrl}/taxes`, data);
        },
        async update(id: any, data: any) {
            return await _update(`${apiUrl}/taxes/${id}`, data);
        },
        async show(id: any) {
            await _show(`${apiUrl}/taxes/${id}`)
                .then((res) => {
                    this.entry = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        async delete(id: any) {
            return await _destroy(`${apiUrl}/taxes/${id}`);
        },
    },
});
