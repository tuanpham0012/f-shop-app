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

  <div class="container newsletter">
    <div
      class="background"
      style="
        background-image: url('src/assets/images/demos/demo-21/newsLetter/banner.jpg');
      "
    >
      <div class="subscribe">
        <div class="subscribe-title text-center">
          <h1 class="intro-title2nd">SUBSCRIBE FOR OUR NEWSLETTER</h1>
          <h1 class="intro-title1st">
            Learn about new offers and get more deals by joining our newsletter
          </h1>
        </div>
        <form action="#">
          <div class="input-group">
            <input
              type="email"
              placeholder="Enter your Email Address"
              aria-label="Email Adress"
            />
            <div class="input-group-append">
              <button class="btn btn-subscribe" type="submit">
                <span>Subscribe</span><i class="icon-long-arrow-right"></i>
              </button>
            </div>
            <!-- .End .input-group-append -->
          </div>
          <!-- .End .input-group -->
        </form>
      </div>
    </div>
  </div>

  <div class="container service mt-4 mb-4">
    <div class="col-sm-6 col-lg-3 col-noPadding">
      <div class="icon-box icon-box-side">
        <span class="icon-box-icon text-dark">
          <i class="icon-truck"></i>
        </span>

        <div class="icon-box-content">
          <h3 class="icon-box-title">Payment &amp; Delivery</h3>
          <!-- End .icon-box-title -->
          <p>Free shipping for orders over $50</p>
        </div>
        <!-- End .icon-box-content -->
      </div>
      <!-- End .icon-box -->
    </div>
    <!-- End .col-sm-6 col-lg-4 -->

    <div class="col-sm-6 col-lg-3">
      <div class="icon-box icon-box-side">
        <span class="icon-box-icon text-dark">
          <i class="icon-rotate-left"></i>
        </span>

        <div class="icon-box-content">
          <h3 class="icon-box-title">Return &amp; Refund</h3>
          <!-- End .icon-box-title -->
          <p>Free 100% money back guarantee</p>
        </div>
        <!-- End .icon-box-content -->
      </div>
      <!-- End .icon-box -->
    </div>
    <!-- End .col-sm-6 col-lg-4 -->

    <div class="col-sm-6 col-lg-3">
      <div class="icon-box icon-box-side">
        <span class="icon-box-icon text-dark">
          <i class="icon-life-ring"></i>
        </span>

        <div class="icon-box-content">
          <h3 class="icon-box-title">Quality Support</h3>
          <!-- End .icon-box-title -->
          <p>Alway online feedback 24/7</p>
        </div>
        <!-- End .icon-box-content -->
      </div>
      <!-- End .icon-box -->
    </div>
    <!-- End .col-sm-6 col-lg-4 -->

    <div class="col-sm-6 col-lg-3">
      <div class="icon-box icon-box-side">
        <span class="icon-box-icon text-dark">
          <i class="icon-envelope"></i>
        </span>

        <div class="icon-box-content">
          <h3 class="icon-box-title">JOIN OUR NEWSLETTER</h3>
          <!-- End .icon-box-title -->
          <p>10% off by subscribing to our newsletter</p>
        </div>
        <!-- End .icon-box-content -->
      </div>
      <!-- End .icon-box -->
    </div>
    <!-- End .col-sm-6 col-lg-4 -->
  </div>
</template>

<style lang="scss" scoped>

</style>
