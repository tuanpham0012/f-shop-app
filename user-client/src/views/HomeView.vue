<script setup lang="ts">
import { computed, onBeforeMount, reactive, watch } from "vue";
import { useCategoryStore } from "@/stores/category";
import { useBrandStore } from "@/stores/brand";
import { useProductStore } from "@/stores/product";
import { viewFile } from "@/helpers/helpers";
import { currencyFormatTenant } from "@/services/utils";

const categoryStore = useCategoryStore();
const brandStore = useBrandStore();
const productStore = useProductStore();

const featuredProductQuery = reactive({
  pageSize: 12,
  page: 1,
  categoryId: null,
});

const brands = computed<any>(() => brandStore.$state.brands.data);
const popularCategories = computed<any>(
  () => categoryStore.$state.popularCategory.data
);

const topCategories = computed<any>(
  () => categoryStore.$state.topCategory.data
);

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
  await categoryStore.getListPopularCategory();
  await brandStore.getList();
  await productStore.getListFeaturedProduct(featuredProductQuery);
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
        <img src="../assets/images/demos/demo-21/banner/banner-1.jpg" />
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
        <img src="../assets/images/demos/demo-21/banner/banner-2.jpg" />
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
        <img src="../assets/images/demos/demo-21/banner/banner-3.jpg" />
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
      <!-- End .row -->
    </div>
  </div>

  <div class="container mb-3" v-if="brands.length > 0">
    <h2 class="text-center text-[2.6rem] font-medium mb-2">Thương hiệu</h2>
    <div class="cat-blocks-container">
      <swiper-component
        :ids="'brand'"
        :itemCount="brands.length"
        :auto-play="true"
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
          '1024': {
            slidesPerView: 8,
            spaceBetween: 40,
          },
        }"
      >
        <swiper-slide v-for="(item, index) in brands" :key="index">
          <a href="category.html" class="cat-block">
            <figure>
              <span>
                <img :src="item.image" alt="Category image" class="max-h-[55px] w-[auto]" />
              </span>
            </figure>
          </a>
        </swiper-slide>
      </swiper-component>
    </div>
  </div>
  <!-- End .container -->

  <div class="container my-3" v-if="popularCategories.length > 0">
    <h2 class="text-center text-[2.6rem] font-medium mb-4">
      Danh mục phổ biến
    </h2>
    <!-- End .title text-center -->

    <div class="cat-blocks-container">
      <swiper-component
        :slidesPerView="6"
        :id="'category'"
        :itemCount="popularCategories.length"
      >
        <swiper-slide v-for="(item, index) in popularCategories" :key="index">
          <div class="">
            <router-link
              :to="'/danh-muc/' + item.code"
              class="cat-block aspect-square"
            >
              <figure>
                <img
                  :src="item.image"
                  alt="Category image"
                  class="w-[125px] h-[auto] object-contain"
                />
              </figure>
              <h3 class="cat-block-title">{{ item.name }}</h3>
            </router-link>
          </div>
        </swiper-slide>
      </swiper-component>
    </div>
  </div>

  <div class="container category-banner">
    <div class="row">
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../assets/images/demos/demo-21/banner/footware.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">FOOTWEAR</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../assets/images/demos/demo-21/banner/accessories.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">ACCESSORIES</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../assets/images/demos/demo-21/banner/mens.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">MEN'S</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
      <div class="col-lg-3 col-md-6 col-sm-6 position-relative">
        <a href="category.html">
          <img src="../assets/images/demos/demo-21/banner/womens.jpg" />
        </a>
        <div class="banner-content">
          <a href="category.html"><h3 class="category">WOMEN'S</h3></a>
          <a href="category.html" class="action">SHOP NOW</a>
        </div>
      </div>
    </div>
  </div>

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
        <div class="row gap-y-10">
          <div
            class="col-6 col-lg-2 col-sm-4 px-2 box-border cursor-pointer"
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
.cat-blocks-container {
  position: relative;
  padding-top: 0.5rem;
}

.cat-blocks-container [class*="col-"] {
  display: flex;
  align-items: stretch;
  justify-content: center;
}

.cat-block {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: center;
  text-align: center;
  height: 100%;
  figure {
    display: inline-flex;
    align-items: center;
    align-content: center;
    height: 100%;
    position: relative;
    flex: 1;
    margin: 0;
    padding: 0.8rem;
    span {
      position: relative;
      &::after {
        content: "";
        display: block;
        position: absolute;
        bottom: -0.2rem;
        left: 45%;
        width: 105%;
        margin-left: -45%;
        height: 0.3rem;
        border-radius: 50%;
        background-color: rgba(0, 0, 0, 0.3);
        transition: all 0.35s ease;
        filter: blur(3px);
        opacity: 0.5;
      }
    }
  }
  img {
    margin-left: auto;
    margin-right: auto;
    // max-width: 75px;
    // max-height: 75px;
    transition: transform 0.35s ease;
  }
  &:hover {
    img {
      transform: translateY(-10px);
    }
    figure span:after {
      opacity: 1;
    }
  }
}

.cat-block-title {
  color: #333;
  font-weight: 450;
  font-size: 1.8rem;
  letter-spacing: -0.01em;
  margin-top: 1rem;
  margin-bottom: 0;
}

.product-title {
  display: -webkit-box; /* Sử dụng flexbox cho nhiều dòng */
  -webkit-box-orient: vertical; /* Định hướng chiều dọc */
  -webkit-line-clamp: 2; /* Giới hạn số dòng hiển thị */
  overflow: hidden; /* Ẩn phần văn bản vượt ra ngoài */
  line-height: 2rem; /* Chiều cao dòng */
  max-height: 4em; /* Chiều cao tối đa tương ứng với số dòng */
  position: relative; /* Để thêm dấu ba chấm */
}
</style>
