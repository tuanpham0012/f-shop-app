<script setup>
import { RouterLink, RouterView } from "vue-router";
import Header from "./layouts/Header.vue";
import Footer from "./layouts/Footer.vue";
import MobileMenu from "./layouts/MobileMenu.vue";
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
</style>
