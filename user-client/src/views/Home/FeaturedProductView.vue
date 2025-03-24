<template lang="">
  <div class="container">
    <hr class="mb-5 mt-8" />

    <div class="heading heading-center mb-3">
      <h2 class="text-center text-[2.6rem] font-medium mb-4">
        Sản phẩm nổi bật
      </h2>
      <!-- End .title -->

      <ul class="nav nav-pills justify-content-center mb-1" role="tablist">
        <li
          class="nav-item cursor-pointer"
          @click="featuredProductQuery.categoryId = null"
        >
          <a
            class="nav-link"
            :class="{ active: featuredProductQuery.categoryId == null }"
            >Tất cả</a
          >
        </li>
        <li
          class="nav-item cursor-pointer"
          v-for="(item, index) in featuredProducts.categories"
          :key="index"
          @click="featuredProductQuery.categoryId = item.id"
        >
          <a
            class="nav-link"
            :class="{ active: featuredProductQuery.categoryId == item.id }"
            >{{ item.name }} ({{ item.productCount }})</a
          >
        </li>
      </ul>
    </div>
    <!-- End .heading -->

    <div class="tab-content tab-content-carousel p-2 mb-3">
      <div
        class="p-0"
        id="arrivals-all-tab"
        role="tabpanel"
        aria-labelledby="arrivals-all-link"
      >
        <div
          class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4"
        >
          <div
            class="box-border cursor-pointer"
            v-for="(item, index) in featuredProducts.products"
            :key="index"
          >
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
                <h3 class="product-title">
                  {{ item.name }}
                </h3>
                <div class="product-cat">
                  {{ item.category?.name }}
                </div>

                <div class="">
                  <span class="text-3xl text-red-500">{{
                    currencyFormatTenant(item.price) + "đ"
                  }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- End .tab-content -->
    <div class="text-center">
      <button href="category.html" class="btn btn-viewMore">
        <span class="text-2xl">Xem thêm</span>
        <i class="icon-long-arrow-right"></i>
      </button>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";

import { useProductStore } from "@/stores/product";
import { currencyFormatTenant } from "@/services/utils";
const categoryStore = useCategoryStore();

const productStore = useProductStore();

const featuredProductQuery = reactive({
  pageSize: 20,
  page: 1,
  categoryId: null,
});

const featuredProducts = computed<any>(() => {
  return {
    products: productStore.$state.featuredProducts.data,
    categories: categoryStore.$state.featuredProductCategory.data,
  };
});
watch(
  () => featuredProductQuery.categoryId,
  async () => {
    await productStore.getListFeaturedProduct(featuredProductQuery);
  }
);
onBeforeMount(async () => {

  await productStore.getListFeaturedProduct(featuredProductQuery);
  await categoryStore.getListCategoryHasFeaturedProduct();
  await categoryStore.getListTopCategoryWithProduct();
});
</script>
<style lang=""></style>
