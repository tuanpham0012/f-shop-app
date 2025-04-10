import { defineStore } from "pinia";
import { _getList } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

interface State {
    categories: any | [];
}

export const useCustomerStore = defineStore("category", {
    state: (): State => {
        return {
            categories: {
                code: 200,
                message: "",
                data: [],
                meta: null,
            }
        };
    },

    actions: {
        getList(query: any) {
            _getList(`${apiUrl}/Category`, query)
                .then((res) => {
                    this.categories = res.data;
                })
                .catch((err) => {
                    console.log(err);
                });
        },
    },
});
