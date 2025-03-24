<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useProductStore } from "../../stores/product";
import { useBrandStore } from "../../stores/brand";
import { displayPrice } from "@/services/utils";
import { viewFile } from "@/helpers/helpers";
import Pagination from "@/components/pagination/Pagination.vue";

const productStore = useProductStore();
const brandStore = useBrandStore();
const route = useRoute();
const router = useRouter();

const categoryCode = ref<string>("");

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
  query.brandCode = value;
};

watch(
  () => query,
  async (newValue) => {
    console.log(newValue);
    router.push({ query: query });
    await getListData();
  },
  { deep: true }
);

onBeforeMount(async () => {
  categoryCode.value = Array.isArray(route.params?.categoryCode)
    ? route.params?.categoryCode[0]
    : route.params?.categoryCode ?? "";
  console.log(route.query);
  query.page = route.query.page ?? query.page;
  query.pageSize = route.query.pageSize ?? query.pageSize;
  query.brandCode = route.query.brandCode ?? query.brandCode;
  await getListData();
  await brandStore.getListBrandByCategory({ categoryCode: categoryCode.value });
});
</script>
<template>
  <nav aria-label="breadcrumb" class="breadcrumb-nav mb-2">
    <div class="container">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="index.html">Home</a></li>
        <li class="breadcrumb-item"><a href="#">Shop</a></li>
        <li class="breadcrumb-item active" aria-current="page">
          Grid 4 Columns
        </li>
      </ol>
    </div>
    <!-- End .container -->
  </nav>
  <!-- End .breadcrumb-nav -->

  <div class="page-content">
    <div class="container">
      <div class="row col-12 justify-center lg:justify-start">
        <aside class="col-12 col-lg-2 ps-4 box-border order-lg-first">
          <div class="sidebar sidebar-shop">
            <div class="widget widget-clean">
              <label class="text-3xl">Lọc:</label>
              <a href="#" class="sidebar-filter-clear text-2xl">Xoá tất cả</a>
            </div>

            <div class="widget widget-collapsible">
              <h3 class="widget-title mb-2">
                <a class="cursor-pointer" @click="showMore('filter-brand')">
                  Brand
                </a>
              </h3>
              <div
                class="filter-brand collapsed hidden lg:grid grid-cols-5 md:grid-cols-5 lg:grid-cols-2 gap-3 my-3"
              >
                <div
                  class="w-[100%] h-[50px] border-1 border-solid d-flex justify-center items-center rounded-lg cursor-pointer"
                  v-for="(item, index) in brands"
                  :key="index"
                  @click="setBrandCode(item.code)"
                  :class="{ 'brand-active': query.brandCode == item.code }"
                >
                  <img
                    :src="item.image"
                    class="w-[80px] h-[40px] object-contain"
                  />
                </div>
              </div>
            </div>

            <div class="widget widget-collapsible">
              <h3 class="widget-title">
                <a
                  data-toggle="collapse"
                  href="#widget-5"
                  role="button"
                  aria-expanded="true"
                  aria-controls="widget-5"
                >
                  Price
                </a>
              </h3>
              <div class="">
                <div class="widget-body">
                  <div class="filter-price">
                    <div class="filter-price-text">
                      Price Range:
                      <span id="filter-price-range"></span>
                    </div>
                    <div id="price-slider"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </aside>
        <div class="col-12 col-sm-10">
          <div class="toolbox">
            <div class="toolbox-left">
              <div class="toolbox-info">
                <!-- Hiển thị
                <span>{{ query.pageSize }} of {{ totalCount }}</span> Sản phẩm -->
              </div>
            </div>

            <div class="toolbox-right">
              <div class="toolbox-sort text-2xl">
                <label for="sortby">Sắp xếp theo giá:</label>
                <div class="select-custom">
                  <select name="sortby" id="sortby" class="form-control">
                    <option value="popularity" select="selected">Không</option>
                    <option value="rating">Giá thấp đến cao</option>
                    <option value="date">Giá cao đến thấp</option>
                  </select>
                </div>
              </div>
              <!-- End .toolbox-sort -->
            </div>
            <!-- End .toolbox-right -->
          </div>
          <!-- End .toolbox -->
          <div class="products min-h-[calc(100vh-320px)] mb-3">
            <div
              class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 md:gap-8 sm:gap-4"
            >
              <div class="" v-for="(item, index) in products" :key="index">
                <div class="product h-100 d-flex flex-col">
                  <figure
                    class="product-media bg-gray-100 aspect-square d-flex items-center"
                  >
                    <a href="product.html">
                      <img
                        :src="viewFile(item.images[0])"
                        alt="Product image"
                        class="product-image"
                      />
                    </a>
                    <div class="product-action">
                      <a href="#" class="btn-product btn-cart"
                        ><span>add to cart</span></a
                      >
                    </div>
                  </figure>

                  <div
                    class="product-body d-flex flex-1 flex-col justify-between"
                  >
                    <div>
                      <h3 class="product-title">
                        <a href="product.html">{{ item.name }}</a>
                      </h3>
                    </div>
                    <div>
                      <div class="text-2xl text-red-500 my-2">
                        {{ displayPrice(item.price) }}đ
                      </div>
                      <div class="ratings-container">
                        <div class="ratings">
                          <div class="ratings-val" style="width: 20%"></div>
                        </div>
                        <span class="ratings-text">( 2 Reviews )</span>
                      </div>
                    </div>
                  </div>
                </div>
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
}
</style>
