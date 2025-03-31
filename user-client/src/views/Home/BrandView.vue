<template lang="">
  <div class="container mb-5" v-if="brands.length > 0">
    <h2 class="text-center text-2xl font-medium mb-2">Thương hiệu</h2>
    <div class="cat-blocks-container">
      <swiper-component
        :itemCount="brands.length"
        :auto-play="true"
        :navigation="false"
        :delay="4000"
        :breakpoints="{
          '0': {
            slidesPerView: 2,
            spaceBetween: 10,
          },
          '768': {
            slidesPerView: 4,
            spaceBetween: 30,
          },
          '1200': {
            slidesPerView: 5,
            spaceBetween: 30,
          },
        }"
      >
        <swiper-slide v-for="(item, index) in brands" :key="index">
          <router-link
              :to="'/thuong-hieu/' + item.code"
            >
            <BrandCardComponent
              :srcImage="item.image"
              :label="item.name"
              width="140px"
              height="60px"
              textLabel="sm"
            ></BrandCardComponent>
          </router-link>
        </swiper-slide>
      </swiper-component>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount } from "vue";
import { useBrandStore } from "@/stores/brand";
import BrandCardComponent from "@/components/BrandCardComponent.vue";
const brandStore = useBrandStore();

const brands = computed<any>(() => brandStore.$state.brands.data);

onBeforeMount(async () => {
  await brandStore.getList();
});
</script>
<style lang=""></style>
