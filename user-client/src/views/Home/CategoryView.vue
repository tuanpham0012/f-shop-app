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
          '768': {
            slidesPerView: 4,
            spaceBetween: 20,
          },
          '1200': {
            slidesPerView: 5,
            spaceBetween: 30,
          },
        }"
      >
        <swiper-slide v-for="(item, index) in popularCategories" :key="index">
            <router-link
              :to="{ name: 'Category', params: {categoryCode: item.code}}"
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
import { useCategoryStore } from "@/stores/category";
import BrandCardComponent from "@/components/BrandCardComponent.vue";
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
