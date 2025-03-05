import { defineStore } from "pinia";
import {
    _getList,
    _create,
    _show,
    _update,
    _destroy,
} from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useBrandStore = defineStore("brand", {
    state: () => {
        return {
            brands: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            }
        };
    },

    actions: {
        async getList() {
            await _getList(`${apiUrl}/brands`, null)
                .then((res) => {
                    console.log(res.data);
                    this.brands = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        }
    },
});