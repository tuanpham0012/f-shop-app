<template lang="">
  <modal
    title="Chi tiết sản phẩm"
    modal-size="modal-xl"
    @close="
      () => {
        emits('close');
      }
    "
  >
    <template #body>
      <div class="container" v-if="product">
        <nav aria-label="breadcrumb" class="breadcrumb-nav mb-2">
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
              <h4 class="text-xl text-medium text-dark">
                {{ product.name }}
              </h4>
              <div class="d-flex flex-row my-3 justify-start text-md">
                <div class="text-warning mb-1 me-2 border-r pe-2">
                  <i class="fa fa-star"></i>
                  <span class="ms-1"> 4.5 </span>
                </div>
                <span class="text-muted border-r me-2 pe-2"
                  ><i class="fas fa-shopping-basket fa-sm mx-1"></i>Đã bán
                  154</span
                >
                <span class="text-muted border-r me-2 pe-2"
                  ><i class="fa-solid fa-comment-dots fa-sm mx-1"></i>154 Đánh
                  giá</span
                >
                <span class="text-success">Còn hàng</span>
              </div>

              <div class="mb-4 bg-gray-100 d-flex gap-3 p-3 rounded-sm">
                <span class="text-xl text-red-500 font-medium">{{
                  displayPrice(skuSelect?.price ?? product.price) + "đ"
                }}</span>
                <span class="text-md text-gray-400 font-normal line-through">{{
                  displayPrice(skuSelect?.price ?? product.price) + "đ"
                }}</span>
              </div>

              <div class="row mt-5 mb-4 text-lg">
                <div
                  class="row col-12 mb-4 items-start"
                  v-for="(option, i) in product.options"
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
                    'border-danger text-danger shadow-2xl shadow-cyan-800/30':
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
              </div>
            </div>
          </div>
          <div class="container col-12 mt-4 bg-white mb-6">
            <div class="border-b border-gray-200 dark:border-gray-700">
              <ul
                class="flex flex-wrap -mb-px text-xl font-medium text-center text-gray-500 dark:text-gray-400"
              >
                <li
                  class="me-2"
                  v-for="(item, index) in tabViews"
                  :key="index"
                  @click="tabViewIndex = item.id"
                >
                  <a
                    href="#"
                    class="tab-view inline-flex gap-2 items-center justify-center p-4 border-b-2 border-transparent rounded-t-lg group"
                    :class="{ active: tabViewIndex == item.id }"
                  >
                    <i :class="item.icon"></i>{{ item.label }}
                  </a>
                </li>
              </ul>
            </div>
            <div class="p-5">
              <ProductDescription
                :alias="$route.params.productCode"
                v-if="tabViewIndex == 1"
              />
              <div class="Comment" v-if="tabViewIndex == 2">
                this is comment tab
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="w-[100%] h-[80vh]" v-else>
        <LoadingComponent />
      </div>
    </template>
    <template #footer>
      <button
        class="btn btn-secondary"
        @click="
          () => {
            emits('close');
          }
        "
      >
        Đóng
      </button>
    </template>
  </modal>
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

const props = defineProps({
  id: {
    type: [Number, String as () => string | null],
    required: false,
    default: null,
  },
});

const emits = defineEmits(["close"]);

const modules = ref([FreeMode, Navigation, Thumbs]);

const thumbsSwiper = ref(null);

const route = useRoute();

const productStore = useProductStore();

const selected = ref<any>([]);
const skuSelect = ref<any>(null);

const quantity = ref(1);

const tabViewIndex = ref(1);

const tabViews = reactive([
  {
    id: 1,
    label: "Thông tin sản phẩm",
    icon: "fa-solid fa-circle-info",
  },
  {
    id: 2,
    label: "Đánh giá",
    icon: "fa-solid fa-comment-dots",
  },
]);

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

  product.value.skus.every((sku: any) => {
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

const product = computed<any>(() => productStore.$state.entry);

watch(
  () => quantity.value,
  (newValue) => {
    if (newValue < 1) quantity.value = 1;
  }
);

onBeforeMount(async () => {
  if (props.id != null) await productStore.show(props.id, true);
});
</script>
<style lang="scss">
.swiper-button-prev:after,
.swiper-button-next:after {
  font-family: "swiper-icons";
  font-size: 3rem;
  font-weight: 500;
}
li {
  .tab-view {
    color: var(--bs-secondary);
    &:hover {
      border-bottom-color: var(--bs-info);
      color: var(--bs-info);
    }
    &.active {
      border-bottom-color: var(--bs-primary);
      color: var(--bs-primary);
      pointer-events: none;
    }
  }
}
</style>
