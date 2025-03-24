<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";

import { useProductStore } from "@/stores/product";
import { currencyFormatTenant } from "@/services/utils";
import BrandView from "./BrandView.vue";
import CategoryView from "./CategoryView.vue";
import FeaturedProductView from "./FeaturedProductView.vue";

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

  <div class="container mb-3" v-for="(category, i) in topCategories" :key="i">
    <div class="heading heading-flex mb-2">
      <div class="heading-center">
        <h2 class="text-center text-[2.8rem] font-medium mb-4">
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
      :slidesPerView="6"
      :spaceBetween="15"
      :itemCount="category.products.length"
      :navigation="false"
      :auto-play="false"
      :delay="4000"
      class="py-4"
    >
      <swiper-slide
        class="py-2"
        v-for="(item, j) in category.products"
        :key="j"
      >
        <div class="box-border cursor-pointer my-2">
          <div
            class="product d-flex flex-col mt-0 py-1 box-border min-h-[310px] rounded-xl"
          >
            <figure
              class="product-media d-flex items-center bg-gray-50 positon-relative aspect-square mb-3 px-3"
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
            <div
              class="product-body p-0 px-3 py-2 d-flex flex-col justify-between flex-grow"
            >
              <h3 class="product-title text-3xl">
                {{ item.name }}
              </h3>
              <div class="">
                <span class="text-2xl text-red-500">{{
                  currencyFormatTenant(item.price) + "đ"
                }}</span>
              </div>
            </div>
          </div>
        </div>
      </swiper-slide>
    </swiper-component>
  </div>

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
