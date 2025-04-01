<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useProductStore } from "../../stores/product";
import { useBrandStore } from "../../stores/brand";
import { currencyFormatTenant, displayPrice } from "@/services/utils";
import { viewFile } from "@/helpers/helpers";
import Pagination from "@/components/pagination/Pagination.vue";
import Slider from "@vueform/slider";
import ProductCardComponent from "@/components/ProductCardComponent.vue";
import BrandCardComponent from "@/components/BrandCardComponent.vue";

const productStore = useProductStore();
const brandStore = useBrandStore();
const route = useRoute();
const router = useRouter();

const categoryCode = ref<string>("");
const priceRange = ref([10000, 2000000]);

const selectFilterPrice = reactive([
  {
    label: "Tất cả",
    value: [null, null],
  },
  {
    label: "Dưới 3 triệu",
    value: [0, 3000000],
  },
  {
    label: "Từ 3 - 6 triệu",
    value: [3000000, 6000000],
  },
  {
    label: "Từ 6 - 10 triệu",
    value: [6000000, 10000000],
  },
  {
    label: "Trên 10 triệu",
    value: [10000000, null],
  },
]);

const selectOptionPrice = ref(-1);

const showMore = (container: string) => {
  const headers = document.querySelectorAll("." + container);
  headers.forEach((header: Element) => {
    const content = window.getComputedStyle(header);
    if (content.getPropertyValue("display") === "grid") {
      (header as HTMLElement).style.display = "none";
    } else {
      (header as HTMLElement).style.display = "grid";
    }
  });
};
const query = reactive<any>({
  page: 1,
  brandCode: "",
  minPrice: null,
  maxPrice: null,
  orderBy: null,
});

const products = computed<any>(
  () => productStore.$state.listProductByCategory.data
);
const brands = computed<any>(() => brandStore.$state.brandByCategory.data);

const currenPage = computed<any>(
  () =>
    productStore.$state.listProductByCategory.meta?.currentPage ?? query.page
);

const pageSize = computed<any>(
  () =>
    productStore.$state.listProductByCategory.meta?.pageSize ?? query.pageSize
);

const totalPages = computed<any>(
  () => productStore.$state.listProductByCategory.meta?.totalPages ?? 1
);

const totalCount = computed<any>(
  () => productStore.$state.listProductByCategory.meta?.totalCount ?? 1
);
const changePage = async (value: any) => {
  console.log(value);
  // query.pageSize = value.pageSize;
  query.page = value.currentPage;
  await getListData();
};

const getListData = async () => {
  await productStore.getListProductByCategory(categoryCode.value, {
    ...query,
    pageSize: 20,
  });
};

const setBrandCode = (value: any) => {
  query.brandCode = query.brandCode == value ? "" : value;
};

const errorImg = (value: any, index: any) => {
  brands.value[index].imagePreview = 1;
};

const viewDetail = (alias: string) => {
  router.push({ name: "ProductDetail", params: { productCode: alias } });
};

watch(
  () => query,
  async (newValue) => {
    let q: any = {};
    Object.entries(newValue).forEach((value: any) => {
      if (value[1] != "" && value[1] != null) {
        q[value[0]] = value[1];
      }
    });

    router.push({ query: q });

    await getListData();
  },
  { deep: true }
);

watch(
  () => selectOptionPrice.value,
  (newValue) => {
    if (selectOptionPrice.value == -1) return;
    if (
      selectFilterPrice[newValue].value[0] != null &&
      selectFilterPrice[newValue].value[1] != null
    ) {
      priceRange.value = [
        selectFilterPrice[newValue].value[0],
        selectFilterPrice[newValue].value[1],
      ];
    } else {
      query.minPrice = selectFilterPrice[newValue].value[0];
      query.maxPrice = selectFilterPrice[newValue].value[1];
    }
  }
);

watch(
  () => priceRange.value,
  (newValue) => {
    console.log(newValue);

    if (
      selectOptionPrice.value != -1 &&
      (newValue[0] != selectFilterPrice[selectOptionPrice.value].value[0] ||
        newValue[1] != selectFilterPrice[selectOptionPrice.value].value[1])
    ) {
      selectOptionPrice.value = -1;
    }
    query.minPrice = newValue[0];
    query.maxPrice = newValue[1];
  },
  { deep: true }
);

onBeforeMount(async () => {
  categoryCode.value = Array.isArray(route.params?.categoryCode)
    ? route.params?.categoryCode[0]
    : route.params?.categoryCode ?? "";
  console.log(route.query);
  query.page = route.query.page ?? query.page;
  // query.pageSize = route.query.pageSize ?? query.pageSize;
  query.brandCode = route.query.brandCode ?? query.brandCode;
  await getListData();
  await brandStore.getListBrandByCategory({ categoryCode: categoryCode.value });
});
</script>
<template>
  <div class="page-content">
    <!-- End .breadcrumb-nav -->
    <div class="container-md">
      <nav aria-label="breadcrumb" class="breadcrumb-nav">
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="index.html">Sản phẩm</a></li>
          <li class="breadcrumb-item"><a href="#">Danh mục</a></li>
          <li class="breadcrumb-item active" aria-current="page">
            {{ products[0]?.category.name }}
          </li>
        </ol>
        <!-- End .container -->
      </nav>
      <div class="grid grid-cols-12 gap-0">
        <aside
          class="hidden xl:block col-span-2 min-w-[200px] px-2 box-border order-lg-first bg-white pt-4 border-1 rounded-md"
        >
          <div class="sidebar sidebar-shop">
            <div class="widget-collapsible mb-4">
              <h3 class="widget-title mb-2">Lựa chọn hãng</h3>
              <div
                class="collapsed hidden xl:grid grid-cols-[repeat(auto-fill,minmax(80px,1fr))] gap-2 my-3 justify-items-center"
              >
                <div
                  class="w-[100%] max-w-[100px] h-[50px] cursor-pointer"
                  v-for="(item, index) in brands"
                  :key="index"
                  @click="setBrandCode(item.code)"
                  :class="{ 'active': query.brandCode == item.code }"
                >
                  <BrandCardComponent :src-image="item.image" :label="item.name" width="90px" height="40px" />
                </div>
              </div>
            </div>

            <div class="widget widget-collapsible">
              <p class="widget-title mb-0">Mức giá</p>
              <div class="border-b border-gray-400 pb-2">
                <div class="widget-body">
                  <div class="filter-price pb-0">
                    <div class="filter-price-text">
                      <ul class="flex flex-col gap-2">
                        <li
                          v-for="(item, index) in selectFilterPrice"
                          :key="index"
                        >
                          <div>
                            <input
                              :id="'selectPrice' + index"
                              type="checkbox"
                              class="form-checkbox me-2"
                              @click="selectOptionPrice = index"
                              :checked="index == selectOptionPrice"
                            /><label
                              :for="'selectPrice' + index"
                              class="text-sm font-medium"
                              >{{ item.label }}
                            </label>
                          </div>
                        </li>
                      </ul>
                      <div class="grid grid-cols-12 gap-1 mb-2">
                        <div class="col-span-6 col-end-7">
                          <input
                            class="form-control"
                            disabled
                            type="text"
                            :value="currencyFormatTenant(priceRange[0]) + 'đ'"
                          />
                        </div>
                        <div class="col-span-6 col-start-7">
                          <input
                            class="form-control"
                            disabled
                            type="text"
                            :value="currencyFormatTenant(priceRange[1]) + 'đ'"
                          />
                        </div>
                      </div>
                      <div class="max-w-[80%] m-auto">
                        <Slider
                          v-model="priceRange"
                          :max="10000000"
                          :tooltips="false"
                        />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </aside>
        <div
          class="col-span-12 xl:col-span-10 bg-white border-1 rounded-lg px-3 ms-2 xxl:ms-3 pt-2"
        >
          <div class="toolbox">
            <div class="toolbox-right ms-5">
              <div class="toolbox-sort text-base">
                <label for="sortby">Sắp xếp theo giá:</label>
                <div class="select-custom">
                  <select
                    name="sortby"
                    id="sortby"
                    class="form-control"
                    v-model="query.orderBy"
                  >
                    <option :value="null">Không</option>
                    <option :value="1">Giá thấp đến cao</option>
                    <option :value="2">Giá cao đến thấp</option>
                  </select>
                </div>
                <div class="block xl:hidden">
                  <button
                    class="btn btn-icon"
                    type="button"
                    @click="showMore('filter-brand')"
                  >
                    <i class="fa fa-filter"></i>
                  </button>
                </div>
              </div>

              <!-- End .toolbox-sort -->
            </div>
            <!-- End .toolbox-right -->
          </div>

          <aside
            class="col-12 hidden filter-brand collapsed px-3 box-border bg-white border-1 rounded-md"
          >
            <div class="">
              <div class="widget-collapsible mb-3">
                <h3 class="widget-title mb-4 py-4 border-b border-gray-400">
                  <a class="cursor-pointer" @click="showMore('filter-brand')"
                    >Bộ lọc tìm kiếm</a
                  >
                </h3>
                <p class="text-base font-medium mb-4">Lựa chọn hãng</p>
                <div
                  class="grid grid-cols-[repeat(auto-fill,minmax(80px,1fr))] gap-3 my-3 justify-items-center border-b border-gray-400 pb-4"
                >
                  <div
                    class="w-[100%] max-w-[95px] h-[45px] border-1 border-solid d-flex justify-center items-center rounded-lg cursor-pointer"
                    v-for="(item, index) in brands"
                    :key="index"
                    @click="setBrandCode(item.code)"
                    :class="{
                      'brand-active': query.brandCode == item.code,
                    }"
                  >
                    <img
                      :src="item.image"
                      @error="errorImg($event, index)"
                      class="w-[80px] h-[40px] object-contain"
                      v-if="item.imagePreview == null"
                    />
                    <span class="text-sm" v-else>{{ item.name }}</span>
                  </div>
                </div>
              </div>

              <div class="widget widget-collapsible mb-3">
                <p class="text-base font-medium">Mức giá</p>
                <div class="border-b border-gray-400 pb-2">
                  <div class="widget-body p-0">
                    <div class="filter-price pb-0">
                      <div class="filter-price-text">
                        <ul class="flex flex-col gap-2">
                          <li
                            v-for="(item, index) in selectFilterPrice"
                            :key="index"
                          >
                            <div>
                              <input
                                :id="'selectPrice' + index"
                                type="checkbox"
                                class="form-checkbox me-2"
                                @click="selectOptionPrice = index"
                                :checked="index == selectOptionPrice"
                              /><label
                                :for="'selectPrice' + index"
                                class="text-sm font-medium"
                                >{{ item.label }}
                              </label>
                            </div>
                          </li>
                        </ul>
                        <div
                          class="grid grid-cols-12 gap-3 mb-4 justify-items-center"
                        >
                          <div class="col-span-3 col-end-4">
                            <input
                              class="form-control"
                              disabled
                              type="text"
                              :value="currencyFormatTenant(priceRange[0]) + 'đ'"
                            />
                          </div>

                          <div class="col-span-3 col-start-4">
                            <input
                              class="form-control"
                              disabled
                              type="text"
                              :value="currencyFormatTenant(priceRange[1]) + 'đ'"
                            />
                          </div>
                        </div>
                        <div class="max-w-[80%] m-auto">
                          <Slider
                            v-model="priceRange"
                            :max="10000000"
                            :tooltips="false"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </aside>
          <!-- End .toolbox -->
          <div class="products min-h-[calc(100vh-320px)] my-3">
            <div
              class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-2 sm:gap-3"
            >
              <div class="" v-for="(item, index) in products" :key="index">
                <ProductCardComponent
                  :item="item"
                  @click="viewDetail(item.alias)"
                />
              </div>
            </div>
          </div>

          <Pagination
            :current-page="currenPage"
            :page-size="pageSize"
            :total-pages="totalPages"
            :total-count="totalCount"
            @change-page="changePage"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.brand-active {
  border-color: rgba(
    var(--bs-link-color-rgb),
    var(--bs-link-opacity, 1)
  ) !important;
  color: rgba(var(--bs-link-color-rgb), var(--bs-link-opacity, 1)) !important;
  background-color: rgb(247, 255, 255);
}
.form-control {
  font-size: .875rem;
  font-weight: 400;
}
.btn {
  min-width: auto;
  width: 60px;
  font-size: 1.3rem;
}

.toolbox .form-control {
  color: #000000;
}

.sidebar-shop .widget-title {
  font-weight: 400;
  font-size: 1rem;
}
</style>
<style src="@vueform/slider/themes/default.css"></style>
