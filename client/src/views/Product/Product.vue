<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import {
  useProductStore,
  useBrandStore,
  useCategoryStore,
} from "../../stores/product";
import debounce from "lodash.debounce";
import ProductModal from "./ProductModal.vue";
import ProductViewDetail from "./ProductViewDetail.vue";
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";
import { displayPrice } from "@/services/utils";
import ProductDescriptionModal from "./ProductDescriptionModal.vue";

const productStore = useProductStore();
const brandStore = useBrandStore();
const categoryStore = useCategoryStore();
const query = reactive({
  pageSize: 25,
  search: "",
  page: 1,
  categoryId: null,
  brandId: null,
});
const showModal = ref(false);
const showViewDetailModal = ref(false);
const showDescriptionModal = ref(false);
const id = ref(null);

const categories = computed<any>(() => categoryStore.$state.entries.data);
const brands = computed<any>(() => brandStore.$state.entries.data);

const products = computed<any>(() => productStore.$state.entries.data);

const currenPage = computed<any>(
  () => productStore.$state.entries.meta?.currentPage ?? query.page
);

const pageSize = computed<any>(
  () => productStore.$state.entries.meta?.pageSize ?? query.pageSize
);

const totalPages = computed<any>(
  () => productStore.$state.entries.meta?.totalPages ?? 1
);

const totalCount = computed<any>(
  () => productStore.$state.entries.meta?.totalCount ?? 1
);
const changePage = async (value: any) => {
  console.log(value);

  query.pageSize = value.pageSize;
  query.page = value.currentPage;
  await getListData();
};
const toggleModal = (refresh = false) => {
  showModal.value = !showModal.value;
  if (refresh) getListData();
};
const toggleEdit = (value: any) => {
  id.value = value;
  toggleModal();
};

const toggleCreate = () => {
  id.value = null;
  toggleModal();
};

const toggleDelete = (id: any) => {
  confirmAlert({
    title: "Xoá khách hàng?",
    text: "Xác nhận xoá khách hàng!!!",
    confirmButtonText: "Xác nhận",
    cancelButtonText: "Huỷ",
  }).then((result) => {
    if (result.isConfirmed) {
      console.log("deleting...");
      productStore
        .delete(id)
        .then((res) => {
          successMessage(res.data?.message ?? "Xoá khách hàng thành công!");
          if (products.value.length <= 1 && currenPage.value > 1) {
            query.page -= 1;
          }
          getListData();
        })
        .catch((err) => errorMessage("Errorr!!"));
    }
  });
};

watch(
  () => query.search,
  debounce(() => {
    query.page = 1;
    getListData();
  }, 500)
);

watch(
  () => [query.categoryId, query.brandId],
  async () => {
    await getListData();
  }
);

const getListData = async () => {
  await productStore.getList(query);
};

onBeforeMount(async () => {
  await getListData();
  await categoryStore.getList({});
  await brandStore.getList({});
});
</script>
<template>
  <div class="card">
    <h5 class="card-header">Sản phẩm</h5>
    <div class="d-flex justify-content-between mx-3 my-2">
      <div class="d-flex">
        <div class="d-flex align-items-center w-auto me-2">
          <!-- <div class="w-75px me-1">
                        <label for="customerCode" class="col-form-label"
                            >Tìm kiếm</label
                        >
                    </div> -->
          <div class="w-100px ms-1">
            <input
              type="text"
              class="form-control mb-lg-0 p-2"
              id="customerCode"
              placeholder="Tìm kiếm (tên sản phẩm)"
              v-model="query.search"
            />
          </div>
        </div>
        <div class="w-[250px] me-2">
          <select-search
            placeholder="-- Loại sản phẩm --"
            :listData="categories"
            display="name"
            keyValue="id"
            v-model="query.categoryId"
          ></select-search>
        </div>
        <div class="w-[250px] me-2">
          <select-search
            placeholder="-- Thương hiệu --"
            :listData="brands"
            display="name"
            keyValue="id"
            v-model="query.brandId"
          ></select-search>
        </div>
      </div>
      <button class="btn btn-primary" @click="toggleCreate()">
        <i class="feather icon-plus"></i>
        Thêm mới
      </button>
    </div>
    <div class="table-responsive text-nowrap table-scroll">
      <table class="table table-hover">
        <thead class="table-light">
          <tr>
            <th>STT</th>
            <th>Hình ảnh</th>
            <th>Tên sản phẩm</th>
            <th>Phân loại</th>
            <th>Thương hiệu</th>
            <th>Giá bán</th>
            <th>Đơn vị bán</th>
            <th>Đơn vị mua</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody class="table-border-bottom-0">
          <tr v-for="(item, index) in products" :key="index">
            <td>
              <strong>{{ index + 1 }}</strong>
            </td>
            <td>
              <img
                :src="item.imageThumb"
                class="w-[60px] h-[60px] object-contain"
                loading="lazy"
              />
            </td>
            <td class="max-w-[350px]">
              <strong>{{ item.name }}</strong>
              <p class="m-0 text-break overflow-hidden">
                {{ item.skuCount }} biến thể
              </p>
            </td>
            <td>{{ item.category?.name }}</td>
            <td>{{ item.brand?.name }}</td>
            <td class="text-right">{{ displayPrice(item.price) + " đ" }}</td>
            <td>{{ item.unitBuy }}</td>
            <td>{{ item.unitSell }}</td>
            <td class="text-center">
              <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-info me-1"
                title="Xem chi tiết"
                @click="
                  () => {
                    id = item.id;
                    showViewDetailModal = !showViewDetailModal;
                  }
                "
              >
                <i class="fa fa-solid fa-info"></i>
              </button>
              <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-primary me-1"
                title="Chỉnh sửa"
                @click="toggleEdit(item.id)"
              >
                <i class="fa-solid fa-pen-to-square"></i>
              </button>
              <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-success me-1"
                title="Chỉnh sửa mô tả"
                @click="
                  () => {
                    id = item.id;
                    showDescriptionModal = !showDescriptionModal;
                  }
                "
              >
              <i class="fa-solid fa-file-word"></i>
              </button>
              <button
                type="button"
                class="btn btn-sm btn-icon btn-outline-secondary me-1"
                title="Xoá sản phẩm"
                @click="toggleDelete(item.id)"
              >
                <span class="tf-icons bx bx-trash-alt bx-xs"></span>
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <Pagination
      :current-page="currenPage"
      :page-size="pageSize"
      :total-pages="totalPages"
      :total-count="totalCount"
      @change-page="changePage"
    />
  </div>
  <ProductModal
    v-if="showModal"
    :id="id"
    @close="
      (value) => {
        toggleModal(value);
        id = null;
      }
    "
  />
  <ProductViewDetail
    v-if="showViewDetailModal"
    :id="id"
    @close="
      () => {
        showViewDetailModal = !showViewDetailModal;
        id = null;
      }
    "
  />
  <ProductDescriptionModal
    v-if="showDescriptionModal"
    :id="id"
    @close="
      () => {
        showDescriptionModal = !showDescriptionModal;
        id = null;
      }
    "
  />
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
