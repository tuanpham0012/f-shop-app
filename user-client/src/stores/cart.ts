import { defineStore } from "pinia";
import { _getList, _create, _show, _update, _destroy } from "@/helpers/axiosConfig";
import { apiUrl } from "@/helpers/config";

export const useCartStore = defineStore("cart", {
  state: () => {
    return {
      carts: {
        code: 200,
        message: "",
        data: [],
        meta: null,
      },
      pending: false,
    };
  },

  actions: {
    async getList() {
      const token = localStorage.getItem("token");
      if (token) {
        await _getList(`${apiUrl}/cart`, null)
          .then((res) => {
            this.carts = res.data;
          })
          .catch((err) => {
            console.log(err);
          });
      }
    },

    async updateCart(id: any, data: any) {
      this.pending = true;
      await _update(`${apiUrl}/cart/${id}`, data)
        .then((res) => {
          this.getList();
        })
        .finally(() => {
          this.pending = false;
        });
    },

    async deleteCart(id: any) {
      await _destroy(`${apiUrl}/cart/${id}`).then((res) => {
        this.getList();
      });
    },

    async deleteCarts(data: any) {
      await _destroy(`${apiUrl}/cart/delete-all`, {}, data).then((res) => {
        this.getList();
      });
    },

    async addToCart(data: any) {
      await _create(`${apiUrl}/cart`, data).then((res) => {
        this.getList();
      });
    },

    async checkout(data: any) {
      return await _create(`${apiUrl}/cart/checkout`, data);
    },
    clearCart(){
      this.carts = {
        code: 200,
        message: "",
        data: [],
        meta: null,
      };
      localStorage.removeItem("cart");
    }
  },
});
