import { defineStore } from "pinia";
import { _getList, _create, _show, _update, _destroy } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useAuthStore = defineStore("auth", {
  state: () => {
    return {
      token: null as string | null,
      info: null as any,
    };
  },

  actions: {
    async login(data:any) {
      await _create(`${apiUrl}/auth/login`, data)
          .then((res) => {
            this.token = res.data.data.token;
            if (this.token) {
              localStorage.setItem("token", this.token);
            }
          })
          .catch((err) => {
            console.log(err);
          });
    },
    async getInfo() {
      await _show(`${apiUrl}/auth/info`)
          .then((res) => {
            this.info = res.data.data;
          })
          .catch((err) => {
            console.log(err);
          });
    },
  },
});
