<template>
  <div class="container" v-if="product">
    <nav aria-label="breadcrumb" class="breadcrumb-nav mb-2">
      <div class="container">
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="index.html">Sản phẩm</a></li>
          <li class="breadcrumb-item">
            <a href="#">{{ product.category?.name }}</a>
          </li>
          <li class="breadcrumb-item active" aria-current="page">
            {{ product.name }}
          </li>
        </ol>
      </div>
      <!-- End .container -->
    </nav>

    <div class="row gx-5">
      <div class="col-lg-6 bg-white">
        <swiper
          :style="{
            '--swiper-navigation-color': '#fff',
            '--swiper-pagination-color': '#fff',
          }"
          :spaceBetween="10"
          :navigation="true"
          :centeredSlides="true"
          :thumbs="{ swiper: thumbsSwiper }"
          :modules="modules"
          class="mySwiper2"
        >
          <swiper-slide v-for="(img, index) in product.images"
            ><div
              class="rounded-3 d-flex justify-content-center w-[100] h-auto aspect-square d-flex justify-center items-center p-4 box-border"
            >
              <a
                data-fslightbox="mygalley"
                class="rounded-4"
                target="_blank"
                data-type="image"
              >
                <img
                  class="w-100 h-100 object-contain"
                  loading="lazy"
                  :src="img.path"
                />
              </a></div
          ></swiper-slide>
        </swiper>
        <swiper
          @swiper="setThumbsSwiper"
          :spaceBetween="4"
          :slidesPerView="5"
          :freeMode="true"
          :watchSlidesProgress="true"
          :modules="modules"
          class="mySwiper my-3"
        >
          <swiper-slide v-for="(img, index) in product.images" :key="index">
            <img
              class="w-[75px] h-[75px] object-contain border p-1 rounded-sm"
              loading="lazy"
              :src="img.path"
            />
          </swiper-slide>
        </swiper>
      </div>
      <div class="col-lg-6 bg-white">
        <div class="ps-lg-3 pt-4">
          <h4 class="title text-dark">
            {{ product.name }}
          </h4>
          <div class="d-flex flex-row my-3 justify-start text-2xl">
            <div class="text-warning mb-1 me-2 border-r pe-2">
              <i class="fa fa-star"></i>
              <span class="ms-1"> 4.5 </span>
            </div>
            <span class="text-muted border-r me-2 pe-2"
              ><i class="fas fa-shopping-basket fa-sm mx-1"></i>Đã bán 154</span
            >
            <span class="text-muted border-r me-2 pe-2"
              ><i class="fa-solid fa-comment-dots fa-sm mx-1"></i>154 Đánh
              giá</span
            >
            <span class="text-success">Còn hàng</span>
          </div>

          <div class="mb-4 bg-gray-100 d-flex gap-3 p-3 rounded-sm">
            <span class="text-4xl text-red-500 font-medium">{{
              displayPrice(skuSelect?.price ?? product.price) + "đ"
            }}</span>
            <span class="text-2xl text-gray-400 font-normal line-through">{{
              displayPrice(skuSelect?.price ?? product.price) + "đ"
            }}</span>
          </div>

          <div class="row mt-5 mb-4 text-2xl">
            <div
              class="row col-12 mb-4 items-start"
              v-for="(option, i) in options"
              :key="i"
            >
              <div class="col-3">
                <label class="py-2 font-medium">{{ option.name }}:</label>
              </div>
              <div class="d-flex gap-2 flex-wrap col-9">
                <div
                  class="p-[4px] mx-2 border rounded-md cursor-pointer text-gray-800 font-semibold"
                  v-for="(value, j) in option.optionValues"
                  :key="j"
                  :class="{
                    'border-primary shadow-2xl shadow-cyan-800/30':
                      selected.findIndex((x:any) => x.optionValueId == value.id) >=
                      0,
                  }"
                  @click="selectOption(value)"
                >
                  <div
                    v-if="option.visual == 1"
                    :class="`w-[25px] h-[25px]`"
                    :style="`background-color: ${value.value}`"
                  ></div>
                  <div v-else>
                    <span class="p-2 font-normal">
                      {{ value.label }}
                    </span>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-12 mt-2">
              <div class="details-filter-row details-row-size">
                <div class="col-3">
                  <label for="qty">Số lượng:</label>
                </div>
                <div class="col-9 product-details-quantity">
                  <div class="relative">
                    <div class="absolute left-0 top-0">
                      <button
                        style="min-width: 26px"
                        class="btn btn-decrement btn-spinner"
                        type="button"
                        @click="quantity--"
                        :disabled="quantity <= 1"
                      >
                        <i class="icon-minus text-2xl"></i>
                      </button>
                    </div>
                    <input
                      type="text"
                      style="
                        text-align: center;
                        font-size: 1.5rem;
                        height: 45px;
                      "
                      class="form-control"
                      v-model="quantity"
                    />
                    <div class="absolute right-0 top-0">
                      <button
                        style="min-width: 26px"
                        class="btn btn-increment btn-spinner z-20"
                        type="button"
                        @click="quantity++"
                      >
                        <i class="icon-plus text-2xl"></i>
                      </button>
                    </div>
                  </div>
                </div>
                <!-- End .product-details-quantity -->
              </div>
            </div>
          </div>
          <div class="col-12 py-5">
            <div class="grid grid-cols-12 gap-3">
              <button class="btn btn-icon btn-outline-primary py-4 col-span-5">
                <span class="text-2xl font-medium"
                  ><i class="fa-solid fa-cart-plus text-2xl"></i> Thêm vào giỏ hàng</span
                >
              </button>
              <button class="btn btn-icon btn-outline-danger py-4 col-span-5">
                <span class="text-2xl font-medium"
                  >Mua ngay</span
                >
              </button>
              <button class="btn btn-icon btn-outline-success py-4 col-span-2">
                <span class="text-2xl font-medium"
                  ><i class="fa-solid fa-heart-circle-plus"></i></span
                >
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="container col-12 mt-4 bg-white mb-6">
        <div class="border-b border-gray-200 dark:border-gray-700">
          <ul class="flex flex-wrap -mb-px text-2xl font-medium text-center text-gray-500 dark:text-gray-400">
              <li class="me-2">
                  <a href="#" class="inline-flex items-center justify-center p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300 group">
                      <svg class="w-4 h-4 me-2 text-gray-400 group-hover:text-gray-500 dark:text-gray-500 dark:group-hover:text-gray-300" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                          <path d="M10 0a10 10 0 1 0 10 10A10.011 10.011 0 0 0 10 0Zm0 5a3 3 0 1 1 0 6 3 3 0 0 1 0-6Zm0 13a8.949 8.949 0 0 1-4.951-1.488A3.987 3.987 0 0 1 9 13h2a3.987 3.987 0 0 1 3.951 3.512A8.949 8.949 0 0 1 10 18Z"/>
                      </svg>Profile
                  </a>
              </li>
              <li class="me-2">
                  <a href="#" class="inline-flex items-center justify-center p-4 text-blue-600 border-b-2 border-blue-600 rounded-t-lg active dark:text-blue-500 dark:border-blue-500 group" aria-current="page">
                      <svg class="w-4 h-4 me-2 text-blue-600 dark:text-blue-500" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 18 18">
                          <path d="M6.143 0H1.857A1.857 1.857 0 0 0 0 1.857v4.286C0 7.169.831 8 1.857 8h4.286A1.857 1.857 0 0 0 8 6.143V1.857A1.857 1.857 0 0 0 6.143 0Zm10 0h-4.286A1.857 1.857 0 0 0 10 1.857v4.286C10 7.169 10.831 8 11.857 8h4.286A1.857 1.857 0 0 0 18 6.143V1.857A1.857 1.857 0 0 0 16.143 0Zm-10 10H1.857A1.857 1.857 0 0 0 0 11.857v4.286C0 17.169.831 18 1.857 18h4.286A1.857 1.857 0 0 0 8 16.143v-4.286A1.857 1.857 0 0 0 6.143 10Zm10 0h-4.286A1.857 1.857 0 0 0 10 11.857v4.286c0 1.026.831 1.857 1.857 1.857h4.286A1.857 1.857 0 0 0 18 16.143v-4.286A1.857 1.857 0 0 0 16.143 10Z"/>
                      </svg>Dashboard
                  </a>
              </li>
              <li class="me-2">
                  <a href="#" class="inline-flex items-center justify-center p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300 group">
                      <svg class="w-4 h-4 me-2 text-gray-400 group-hover:text-gray-500 dark:text-gray-500 dark:group-hover:text-gray-300" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                          <path d="M5 11.424V1a1 1 0 1 0-2 0v10.424a3.228 3.228 0 0 0 0 6.152V19a1 1 0 1 0 2 0v-1.424a3.228 3.228 0 0 0 0-6.152ZM19.25 14.5A3.243 3.243 0 0 0 17 11.424V1a1 1 0 0 0-2 0v10.424a3.227 3.227 0 0 0 0 6.152V19a1 1 0 1 0 2 0v-1.424a3.243 3.243 0 0 0 2.25-3.076Zm-6-9A3.243 3.243 0 0 0 11 2.424V1a1 1 0 0 0-2 0v1.424a3.228 3.228 0 0 0 0 6.152V19a1 1 0 1 0 2 0V8.576A3.243 3.243 0 0 0 13.25 5.5Z"/>
                      </svg>Settings
                  </a>
              </li>
              <li class="me-2">
                  <a href="#" class="inline-flex items-center justify-center p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300 group">
                      <svg class="w-4 h-4 me-2 text-gray-400 group-hover:text-gray-500 dark:text-gray-500 dark:group-hover:text-gray-300" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 18 20">
                          <path d="M16 1h-3.278A1.992 1.992 0 0 0 11 0H7a1.993 1.993 0 0 0-1.722 1H2a2 2 0 0 0-2 2v15a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2Zm-3 14H5a1 1 0 0 1 0-2h8a1 1 0 0 1 0 2Zm0-4H5a1 1 0 0 1 0-2h8a1 1 0 1 1 0 2Zm0-5H5a1 1 0 0 1 0-2h2V2h4v2h2a1 1 0 1 1 0 2Z"/>
                      </svg>Contacts
                  </a>
              </li>
              <li>
                  <a class="inline-block p-4 text-gray-400 rounded-t-lg cursor-not-allowed dark:text-gray-500">Disabled</a>
              </li>
          </ul>
        </div>
        <div class="p-5">
          <ProductDescription :alias="$route.params.productCode" />
        </div>
      </div>
    </div>
  </div>
  <div class="w-[100%] h-[80vh]" v-else>
    <LoadingComponent />
  </div>
  

</template>
<script lang="ts" setup>
import { reactive, ref, onBeforeMount, computed, watch } from "vue";
import { useProductStore } from "@/stores/product";
import { useRoute, useRouter } from "vue-router";
import { errorMessage } from "@/helpers/toast";
import { isNumber } from "@/helpers/helpers";
import { displayPrice } from "@/services/utils";

import { Swiper, SwiperSlide } from "swiper/vue";

// Import Swiper styles
import "swiper/css";

import "swiper/css/free-mode";
import "swiper/css/navigation";
import "swiper/css/thumbs";
import { FreeMode, Navigation, Thumbs } from "swiper/modules";
import ProductDescription from "./ProductDescription.vue";
import LoadingComponent from "@/components/LoadingComponent.vue";

const modules = ref([FreeMode, Navigation, Thumbs]);

const thumbsSwiper = ref(null);

const route = useRoute();

const productStore = useProductStore();

const selected = ref<any>([]);
const skuSelect = ref<any>(null);

const quantity = ref(1);

const setThumbsSwiper = (swiper: any) => {
  thumbsSwiper.value = swiper;
};

const selectOption = (value: any) => {
  skuSelect.value = null;
  const item = {
    optionId: value.optionId,
    optionValueId: value.id,
  };
  //check item selected
  const index = selected.value.findIndex(
    (x: any) => x.optionId == value.optionId
  );
  let oldValue = null;
  if (index >= 0) {
    oldValue = selected.value[index].optionValueId;
    selected.value.splice(index, 1);
  }

  selected.value.push(item);

  skus.value.every((sku: any) => {
    const check = onlyInLeft(sku.variants, selected.value, isSameUser);

    if (check.length == 0) {
      skuSelect.value = sku;
      return false;
    }
    return true;
  });
};

const isSameUser = (a: any, b: any) =>
  a.optionId === b.optionId && a.optionValueId === b.optionValueId;

// Get items that only occur in the left array,
// using the compareFunction to determine equality.
const onlyInLeft = (left: any, right: any, compareFunction: any) =>
  left.filter(
    (leftValue: any) =>
      !right.some((rightValue: any) => compareFunction(leftValue, rightValue))
  );

const product = computed<any>(() => productStore.$state.product);
const options = computed<any>(() => productStore.$state.options);
const skus = computed<any>(() => productStore.$state.skus);

watch(
  () => quantity.value,
  (newValue) => {
    if (newValue < 1) quantity.value = 1;
  }
);

onBeforeMount(async () => {
  if (route.params.productCode != null)
    await productStore.getProductByAlias(route.params.productCode);
});
</script>
<style lang="scss">
.swiper-button-prev:after,
.swiper-button-next:after {
  font-family: "swiper-icons";
  font-size: 3rem;
  font-weight: 500;
}
</style>
