import { defineStore } from "pinia";
import { _create, _getList, _show, _update } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";
import type { create } from "node_modules/axios/index.cjs";

const url = apiUrl + "/admin";

export const useMenuStore = defineStore("menu", {
    state: () => {
        return {
            menu: {
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
            }
        };
    },

    actions: {
        async getMenu(query: any) {
            await _getList(`${apiUrl}/menus/admin-menu`, query)
                .then((res) => {
                    this.menu = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
});
