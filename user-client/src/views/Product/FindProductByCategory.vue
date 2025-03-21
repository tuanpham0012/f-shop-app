<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useRoute } from "vue-router";
import { useProductStore } from "../../stores/product";
import { useBrandStore } from "../../stores/brand";
import debounce from "lodash.debounce";
import { displayPrice } from "@/services/utils";
import { viewFile } from "@/helpers/helpers";

const productStore = useProductStore();
const brandStore = useBrandStore();
const route = useRoute();

const categoryCode = ref<string>("");
const query = reactive({
  pageSize: 20,
  search: "",
  page: 1,
  categoryId: null,
  brandIds: [],
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

  query.pageSize = value.pageSize;
  query.page = value.currentPage;
  await getListData();
};

const getListData = async () => {
  await productStore.getListProductByCategory(categoryCode.value, query);
};

watch(
  () => query.brandIds,
  (newValue) => {
    console.log(newValue);
  }
);

onBeforeMount(async () => {
  categoryCode.value = Array.isArray(route.params?.categoryCode)
    ? route.params?.categoryCode[0]
    : route.params?.categoryCode ?? "";
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
      <div class="row col-12">
        <aside class="col-lg-2 ps-4 box-border order-lg-first">
          <div class="sidebar sidebar-shop">
            <div class="widget widget-clean">
              <label class="text-3xl">Lọc:</label>
              <a href="#" class="sidebar-filter-clear text-2xl">Xoá tất cả</a>
            </div>

            <div class="widget">
              <h3 class="widget-title mb-2">
                <a
                  data-toggle="collapse"
                  href="#widget-4"
                  role="button"
                  aria-expanded="true"
                  aria-controls="widget-4"
                >
                  Brand
                </a>
              </h3>
              <!-- End .widget-title -->

              <div class="show">
                <div
                  class="form-check"
                  v-for="(item, index) in brands"
                  :key="index"
                >
                  <input
                    class="form-check-input w-[1.5rem] h-[1.5rem] me-3"
                    type="checkbox"
                    :value="item.id"
                    :id="'brand-' + item.id"
                    v-model="query.brandIds"
                  />
                  <label
                    class="form-check-label text-2xl"
                    :for="'brand-' + item.id"
                  >
                    {{ item.name }}
                  </label>
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
              <!-- End .widget-title -->

              <div class="">
                <div class="widget-body">
                  <div class="filter-price">
                    <div class="filter-price-text">
                      Price Range:
                      <span id="filter-price-range"></span>
                    </div>
                    <!-- End .filter-price-text -->

                    <div id="price-slider"></div>
                    <!-- End #price-slider -->
                  </div>
                  <!-- End .filter-price -->
                </div>
                <!-- End .widget-body -->
              </div>
              <!-- End .collapse -->
            </div>
            <!-- End .widget -->
          </div>
          <!-- End .sidebar sidebar-shop -->
        </aside>
        <div class="col-lg-10">
          <div class="toolbox">
            <div class="toolbox-left">
              <div class="toolbox-info">
                Hiển thị
                <span>{{ query.pageSize }} of {{ totalCount }}</span> Sản phẩm
              </div>
              <!-- End .toolbox-info -->
            </div>
            <!-- End .toolbox-left -->

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
            <div class="products mb-3">
              <div class="columns-5xl columns-3xs columns-2sm gap-8">
                <div
                  class=""
                  v-for="(item, index) in products"
                  :key="index"
                >
                  <div class="product product-7 text-center">
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
                      <!-- End .product-action -->
                    </figure>
                    <!-- End .product-media -->

                    <div class="product-body">
                      <div class="product-cat">
                        <a href="#">{{ item.category?.name }}</a>
                      </div>
                      <!-- End .product-cat -->
                      <h3 class="product-title">
                        <a href="product.html">{{ item.name }}</a>
                      </h3>
                      <!-- End .product-title -->
                      <div class="product-price text-red-500">
                        {{ displayPrice(item.price) }}đ
                      </div>
                      <!-- End .product-price -->
                      <div class="ratings-container">
                        <div class="ratings">
                          <div class="ratings-val" style="width: 20%"></div>
                          <!-- End .ratings-val -->
                        </div>
                        <!-- End .ratings -->
                        <span class="ratings-text">( 2 Reviews )</span>
                      </div>
                    </div>
                    <!-- End .product-body -->
                  </div>
                  <!-- End .product -->
                </div>
                <!-- End .col-sm-6 col-lg-4 col-xl-3 -->
              </div>
              <!-- End .row -->
            </div>

          <!-- End .products -->

          <nav aria-label="Page navigation">
            <ul class="pagination justify-content-center">
              <li class="page-item disabled">
                <a
                  class="page-link page-link-prev"
                  href="#"
                  aria-label="Previous"
                  tabindex="-1"
                  aria-disabled="true"
                >
                  <span aria-hidden="true"
                    ><i class="icon-long-arrow-left"></i></span
                  >Prev
                </a>
              </li>
              <li class="page-item active" aria-current="page">
                <a class="page-link" href="#">1</a>
              </li>
              <li class="page-item"><a class="page-link" href="#">2</a></li>
              <li class="page-item"><a class="page-link" href="#">3</a></li>
              <li class="page-item-total">of 6</li>
              <li class="page-item">
                <a class="page-link page-link-next" href="#" aria-label="Next">
                  Next
                  <span aria-hidden="true"
                    ><i class="icon-long-arrow-right"></i
                  ></span>
                </a>
              </li>
            </ul>
          </nav>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
