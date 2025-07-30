<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";

import { useProductStore } from "@/stores/product";
import { currencyFormatTenant } from "@/services/utils";
import BrandView from "./BrandView.vue";
import CategoryView from "./CategoryView.vue";
import FeaturedProductView from "./FeaturedProductView.vue";
import TopProduct from "./TopProduct.vue";

const categoryStore = useCategoryStore();

const productStore = useProductStore();

const topCategories = computed<any>(
  () => categoryStore.$state.topCategory.data
);
onBeforeMount(async () => {
  
  await categoryStore.getListCategoryHasFeaturedProduct();
  await categoryStore.getListTopCategoryWithProduct();
});
</script>

<template>
  <div class="intro-slider-container mb-5">
    <!-- End .intro-slider owl-carousel owl-simple -->

    <span class="slider-loader"></span
    ><!-- End .slider-loader -->
  </div>
  <!-- End .intro-slider-container -->

  <div class="container banner-container">
    <div class="col-lg-4 col-md-8 col-sm-10 col-12 col-pd1 position-relative">
      <a href="category.html">
        <img src="../../assets/images/demos/demo-21/banner/banner-1.jpg" />
      </a>
      <div class="banner-content">
        <div class="title">
          <a href="category.html"
            ><h3 class="darkWhite">It's a lifestyle.</h3></a
          >
        </div>
        <div class="content">
          <a href="category.html"><h3>Running Apparel</h3></a>
          <a href="category.html"><h3>END OF SEASON SALE</h3></a>
        </div>
        <div class="action">
          <a href="category.html" class="btn btn-demoprimary">
            <span>SHOP NOW</span>
            <i class="icon-long-arrow-right"></i>
          </a>
        </div>
      </div>
      <!-- End .row -->
    </div>
    <div class="col-lg-4 col-md-8 col-sm-10 col-12 col-pd1 position-relative">
      <a href="category.html">
        <img src="../../assets/images/demos/demo-21/banner/banner-2.jpg" />
      </a>
      <div class="banner-content">
        <div class="title">
          <a href="category.html"
            ><h3 class="darkWhite">Hike Your Next Trail</h3></a
          >
        </div>
        <div class="content">
          <a href="category.html"><h3>NEW SEASON</h3></a>
          <a href="category.html"><h3>NEW GEAR</h3></a>
        </div>
        <div class="action">
          <a href="category.html" class="btn btn-demoprimary">
            <span>DISCOVER NOW</span>
            <i class="icon-long-arrow-right"></i>
          </a>
        </div>
      </div>
      <!-- End .row -->
    </div>
    <div class="col-lg-4 col-md-8 col-sm-10 col-12 col-pd1 position-relative">
      <a href="category.html">
        <img src="../../assets/images/demos/demo-21/banner/banner-3.jpg" />
      </a>
      <div class="banner-content">
        <div class="title">
          <a href="category.html"><h3 class="darkWhite">Summer Sale</h3></a>
        </div>
        <div class="content">
          <a href="category.html"><h3>Swimwear sale</h3></a>
          <a href="category.html"><h3>Save up to 30%</h3></a>
        </div>
        <div class="action">
          <a href="category.html" class="btn btn-demoprimary">
            <span>SHOP NOW</span>
            <i class="icon-long-arrow-right"></i>
          </a>
        </div>
      </div>
    </div>
  </div>

  <BrandView />

  <CategoryView />

  <div class="container category-banner">
    <div class="row">
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../../assets/images/demos/demo-21/banner/footware.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">FOOTWEAR</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../../assets/images/demos/demo-21/banner/accessories.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">ACCESSORIES</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../../assets/images/demos/demo-21/banner/mens.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">MEN'S</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../../assets/images/demos/demo-21/banner/womens.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">WOMEN'S</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
    </div>
  </div>

  <FeaturedProductView />

  <TopProduct />

  <div class="container newsletter mb-10">
    
  </div>

</template>

<style lang="scss" scoped>

</style>
