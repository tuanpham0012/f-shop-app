import { defineStore } from "pinia";

export const useGlobalStore = defineStore("global", {
  state: () => {
    return {
      isSidebarMinimized: false,
      userName: "Vasili S",
      maxHeightScreen: 0
    };
  },

  actions: {
    toggleSidebar() {
      this.isSidebarMinimized = !this.isSidebarMinimized;
    },

    changeUserName(userName: string) {
      this.userName = userName;
    },
    setError({}){}
  },
});