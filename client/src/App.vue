<script setup>
import { RouterLink, RouterView } from "vue-router";
import SideBar from "./layouts/SideBar.vue";
import NavBar from "./layouts/NavBar.vue";
// import Customer from "./views/Customer/Customer.vue";
</script>

<template>
  <!-- <header>
    <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />

      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/about">About</RouterLink>
      </nav>
    </div>
  </header>

  <RouterView /> -->
  <!-- Layout wrapper -->
  <div class="layout-wrapper layout-content-navbar">
    <div class="layout-container">
      <!-- Menu -->
      <SideBar />
      <!-- Layout container -->
      <div class="layout-page">
         <!-- Navbar -->
         <NavBar />
         <!-- / Navbar -->
        <!-- Content wrapper -->
        <div class="content-wrapper">
          <div class="container-xxl flex-grow-1 container-p-y">
            <RouterView />
          </div>
          <!-- Content -->
           
        </div>
        <!-- Content wrapper -->
      </div>
      <!-- / Layout page -->
    </div>

    <!-- Overlay -->
    <div class="layout-overlay layout-menu-toggle"></div>
  </div>
</template>

<style lang="scss">
@import "assets/vendor/fonts/boxicons.css";
@import "assets/vendor/css/core.css";
@import "assets/vendor/css/theme-default.css";
@import "assets/css/demo.css";
@import "assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css";
@import "assets/vendor/libs/apex-charts/apex-charts.css";
@import "assets/css/style.scss";
</style>
