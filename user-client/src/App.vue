<script setup>
import { onBeforeMount } from "vue";
import { RouterLink, RouterView } from "vue-router";
import Header from "./layouts/Header.vue";
import Footer from "./layouts/Footer.vue";
import MobileMenu from "./layouts/MobileMenu.vue";
import { useAuthStore } from "@/stores/auth";

const authStore = useAuthStore();

onBeforeMount(async () => {
  await authStore.getInfo();
  // await authStore.getListAddress();
  // await authStore.getListOrder();
  // await authStore.getListCart();
});
</script>

<template>
  <div class="page-wrapper">
    <!-- Header -->
    <Header />
    <main class="main">
      <RouterView />
    </main>
    <Footer />
  </div>
  <MobileMenu />
</template>

<style lang="scss">
@import url('./assets/css/style.css');
@import url('./assets/css/bootstrap.min.css');
//@import url('./assets/css/skins/skin-demo-21.css');
@import url('./assets/css/demos/demo-21.css');

:root {
  --slider-connect-bg: #7a7a7a;
  --slider-tooltip-bg: #4b4b4b;
  --bs-breadcrumb-font-size: .875rem;
}

.main{
  min-height: calc(100vh - 87px);
}

.cat-blocks-container {
  position: relative;
  padding-top: 0.5rem;
}

.cat-blocks-container [class*="col-"] {
  display: flex;
  align-items: stretch;
  justify-content: center;
}

.cat-block {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: center;
  text-align: center;
  height: 100%;
  figure {
    display: inline-flex;
    align-items: center;
    align-content: center;
    height: 100%;
    position: relative;
    flex: 1;
    margin: 0;
    padding: 0.8rem;
    span {
      position: relative;
      &::after {
        content: "";
        display: block;
        position: absolute;
        bottom: -0.2rem;
        left: 45%;
        width: 100%;
        margin-left: -45%;
        height: 0.2rem;
        border-radius: 50%;
        background-color: rgba(49, 49, 49, 0.3);
        transition: all 0.35s ease;
        filter: blur(3px);
        opacity: 0.3;
      }
    }
  }
  img {
    margin-left: auto;
    margin-right: auto;
    // max-width: 75px;
    // max-height: 75px;
    transition: transform 0.35s ease;
  }
  &:hover {
    img {
      transform: translateY(-1px);
    }
    figure span:after {
      opacity: 1;
    }
  }
}

.cat-block-title {
  color: #333;
  font-weight: 450;
  font-size: 1.8rem;
  letter-spacing: -0.01em;
  margin-top: 1rem;
  margin-bottom: 0;
}

.product-title {
  display: -webkit-box; /* Sử dụng flexbox cho nhiều dòng */
  -webkit-box-orient: vertical; /* Định hướng chiều dọc */
  -webkit-line-clamp: 2; /* Giới hạn số dòng hiển thị */
  overflow: hidden; /* Ẩn phần văn bản vượt ra ngoài */
  line-height: 1.1rem; /* Chiều cao dòng */
  height: 2.2rem; /* Chiều cao tối đa tương ứng với số dòng */
  padding-bottom: .2rem;
}
</style>
