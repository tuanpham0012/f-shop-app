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
          '640': {
            slidesPerView: 4,
            spaceBetween: 20,
          },
          '768': {
            slidesPerView: 6,
            spaceBetween: 30,
          },
        }"
      >
        <swiper-slide v-for="(item, index) in brands" :key="index">
          <a href="category.html" class="cat-block">
            <figure>
              <span>
                <img
                  :src="item.image"
                  alt="Category image"
                  class="h-[75px] w-[120px] object-contain"
                />
              </span>
            </figure>
          </a>
        </swiper-slide>
      </swiper-component>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount } from "vue";
import { useBrandStore } from "@/stores/brand";
const brandStore = useBrandStore();

const brands = computed<any>(() => brandStore.$state.brands.data);

onBeforeMount(async () => {
  await brandStore.getList();
});
</script>
<style lang=""></style>
