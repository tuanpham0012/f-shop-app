import { defineStore } from "pinia";
import { _create, _getList, _show, _update } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";
import type { create } from "node_modules/axios/index.cjs";


export const useMenuStore = defineStore("menu", {
    state: () => {
        return {
            adminMenus: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            entries :{
                code: 200,
                message: "",
                data: [],
                meta: null,
            },
            menu: null
        };
    },

    actions: {
        async getAdminMenu(query: any) {
            await _getList(`${apiUrl}/menus/admin-menu`, query)
                .then((res) => {
                    this.adminMenus = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async getList(query: any) {
            await _getList(`${apiUrl}/menus`, query)
                .then((res) => {
                    this.entries = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async show(id: any) {
            await _show(`${apiUrl}/menus/${id}`)
                .then((res) => {
                    this.menu = res.data.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },

        async create(data: any) {
            return await _create(`${apiUrl}/menus`, data)
        },

        async update(id:any, data: any) {
            return await _update(`${apiUrl}/menus/${id}`, data)
        },
    },
});
