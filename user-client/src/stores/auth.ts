import { defineStore } from "pinia";
import { _getList, _create, _show, _update, _destroy } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";
import { useCartStore } from "@/stores/cart";

export const useAuthStore = defineStore("auth", {
  state: () => {
    return {
      token: null as string | null,
      info: null as any,
      countTryLogin: 0,
    };
  },

  actions: {
    async login(data:any) {
      await _create(`${apiUrl}/auth/login`, data)
          .then((res) => {
            this.token = res.data.data.token;
            if (this.token) {
              localStorage.setItem("token", this.token);
              this.countTryLogin = 0;
            }
          })
          .catch((err) => {
            console.log(err);
            this.countTryLogin++;
          });
    },

    logout () {
      this.token = null;
      this.info = null;
      useCartStore().clearCart();
      localStorage.removeItem("token");
    },

    async socialLogin() {
      await _getList(`${apiUrl}/auth/login-google`, {})
    },
    async loginWithGoogle(token: string, type:number = 1) {
      await _create(`${apiUrl}/auth/login-google`, {token: token, type: type}).then((res) => {
                console.log("Login successful");
                console.log("User Info", res.data);
                this.info = res.data.data;
                localStorage.setItem("token", res.data.data.token);
                this.token = res.data.data.token;
            })
            .catch((error) => {
                // Handle error
                console.error("Login failed", error);
            })
    },
    async getInfo() {
      if(!this.token) return;
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
