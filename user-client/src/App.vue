<script setup>
// import './assets/js/bootstrap.bundle.min.js'
// import './assets/js/jquery.plugin.min.js'


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

<style>
@import url('./assets/css/style.css');
@import url('./assets/css/bootstrap.min.css');
@import url('./assets/css/skins/skin-demo-21.css');
@import url('./assets/css/demos/demo-21.css');

</style>
