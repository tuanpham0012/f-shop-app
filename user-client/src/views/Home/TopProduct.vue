<template lang="">
  <div class="container mb-3" v-for="(category, i) in topCategories" :key="i">
    <div class="heading heading-flex mb-1">
      <div class="heading-center">
        <h2 class="text-center text-2xl font-medium">
          {{ category.name }}
        </h2>
        <!-- End .title -->
      </div>
      <!-- End .heading-left -->

      <div class="heading-right">
        <a href="category.html" class="title-link"
          >Xem thêm <i class="icon-long-arrow-right"></i
        ></a>
      </div>
      <!-- End .heading-right -->
    </div>
    <!-- End .heading -->

    <swiper-component
      :itemCount="category.products.length"
      :autoPlay="false"
      :navigation="false"
      :delay="4000"
      :breakpoints="{
        '0': {
          slidesPerView: 2,
          spaceBetween: 10,
        },
        '640': {
          slidesPerView: 2,
          spaceBetween: 10,
        },
        '768': {
          slidesPerView: 3,
          spaceBetween: 10,
        },
        '992': {
          slidesPerView: 5,
          spaceBetween: 10,
        },
      }"
      class="p-2 bg-white"
    >
      <swiper-slide
        class="py-2"
        v-for="(item, j) in category.products"
        :key="j"
      >
        <div class="box-border cursor-pointer my-2">
          <ProductCardComponent :item="item" @click="viewDetail(item.alias)" />
        </div>
      </swiper-slide>
    </swiper-component>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";
import { currencyFormatTenant } from "@/services/utils";
import { useRouter } from "vue-router";
import ProductCardComponent from "@/components/ProductCardComponent.vue";
const router = useRouter();
const categoryStore = useCategoryStore();
const topCategories = computed<any>(
  () => categoryStore.$state.topCategory.data
);
const viewDetail = (alias: string) => {
  router.push({ name: "ProductDetail", params: { productCode: alias } });
};
onBeforeMount(async () => {
  await categoryStore.getListTopCategoryWithProduct();
});
</script>
<style lang="scss" scoped>
.product {
  height: 360px;
}
</style>
