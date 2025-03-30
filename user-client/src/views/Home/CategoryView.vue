<template lang="">
  <div class="container mb-4" v-if="popularCategories.length > 0">
    <h2 class="text-center text-2xl font-medium mb-2">
      Danh mục phổ biến
    </h2>
    <!-- End .title text-center -->

    <div class="cat-blocks-container">
      <swiper-component
        :id="'category'"
        :itemCount="popularCategories.length"
        :navigation="false"
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
        <swiper-slide v-for="(item, index) in popularCategories" :key="index">
          <div class="">
            <router-link
              :to="'/danh-muc/' + item.code"
              class="cat-block"
            >
              <figure>
                <img
                  :src="item.image"
                  alt="Category image"
                  class="w-[125px] h-[auto] object-contain"
                />
              </figure>
              <!-- <h3 :title="item.name" class="cat-block-title d-none d-lg-block whitespace-nowrap max-w-[100%] overflow-hidden text-ellipsis">{{ item.name }}</h3> -->
            </router-link>
          </div>
        </swiper-slide>
      </swiper-component>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount } from "vue";
import { useCategoryStore } from "@/stores/category";
const categoryStore = useCategoryStore();
const popularCategories = computed<any>(
  () => categoryStore.$state.popularCategory.data
);
onBeforeMount(async () => {
  await categoryStore.getListPopularCategory();
  await categoryStore.getListCategoryHasFeaturedProduct();
  await categoryStore.getListTopCategoryWithProduct();
});
</script>
<style lang=""></style>
