<template lang="">
  <div class="container-lg">
    <hr class="mb-4 mt-4" />

    <div class="heading heading-center mb-2">
      <h2 class="text-center text-2xl font-medium mb-2">
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

    <div class="tab-content tab-content-carousel p-2 mb-2">
      <div
        class="p-0"
        id="arrivals-all-tab"
        role="tabpanel"
        aria-labelledby="arrivals-all-link"
      >
        <div
          class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-2 lg:gap-3"
        >
          <div
            class="box-border cursor-pointer"
            v-for="(item, index) in featuredProducts.products"
            :key="index"
          >
          <ProductCardComponent
                  :item="item"
                  @click="viewDetail(item.alias)"
                />
          </div>
        </div>
      </div>
    </div>
    <!-- End .tab-content -->
    <div class="text-center">
      <button href="category.html" class="btn btn-viewMore">
        <span class="text-xl">Xem thêm</span>
        <i class="icon-long-arrow-right"></i>
      </button>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";
import ProductCardComponent from "@/components/ProductCardComponent.vue";
import { useProductStore } from "@/stores/product";
import { currencyFormatTenant } from "@/services/utils";
import { useRouter } from "vue-router";
const categoryStore = useCategoryStore();

const productStore = useProductStore();
const router = useRouter();

const featuredProductQuery = reactive({
  pageSize: 10,
  page: 1,
  categoryId: null,
});

const featuredProducts = computed<any>(() => {
  return {
    products: productStore.$state.featuredProducts.data,
    categories: categoryStore.$state.featuredProductCategory.data,
  };
});

const viewDetail = (alias: string) => {
  router.push({ name: "ProductDetail", params: { productCode: alias } });
};

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
