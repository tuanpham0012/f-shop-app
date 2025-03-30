<template lang="">
  <div class="container mb-3" v-for="(category, i) in topCategories" :key="i">
    <div class="heading heading-flex mb-2">
      <div class="heading-center">
        <h2 class="text-center text-2xl font-medium mb-4">
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
          spaceBetween: 30,
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
          <div class="product mt-0 py-1 box-border h-[100%] rounded-xl">
            <figure
              class="product-media d-flex items-center bg-gray-50 positon-relative aspect-square mb-1 px-3"
            >
              <span
                class="product-label label-circle label-new"
                v-if="item.isNew"
                >New</span
              >
              <span
                class="product-label label-circle label-top"
                v-if="item.isFeatured"
                >Top</span
              >
              <!-- <span class="product-label label-circle label-sale">Sale</span> -->
              <a href="product.html">
                <img
                  :src="item.imageThumb"
                  alt="Product image"
                  class="product-image object-contain"
                />
              </a>
              <div class="product-action">
                <button
                  href="#"
                  class="btn-product btn-cart"
                  title="Add to cart"
                >
                  <span>Thêm vào giỏ hàng</span>
                </button>
              </div>
            </figure>
            <div class="product-body p-0 px-3 py-2">
              <p class="product-title text-base">
                {{ item.name }}
              </p>
              <div class="product-cat text-sm">
                {{ item.category?.name }}
              </div>

              <div class="">
                <span class="text-base text-red-500">{{
                  currencyFormatTenant(item.price) + "đ"
                }}</span>
              </div>
            </div>
          </div>
        </div>
      </swiper-slide>
    </swiper-component>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";
import { currencyFormatTenant } from "@/services/utils";
const categoryStore = useCategoryStore();
const topCategories = computed<any>(
  () => categoryStore.$state.topCategory.data
);
onBeforeMount(async () => {
  await categoryStore.getListTopCategoryWithProduct();
});
</script>
<style lang="scss" scoped>
.product{
	height: 360px;
}
</style>
