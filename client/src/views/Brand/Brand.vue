<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useBrandStore } from "../../stores/product";
import debounce from "lodash.debounce";
import BrandModal from "./BrandModal.vue";
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";
import { viewFile } from "@/helpers/helpers";

const brandStore = useBrandStore();
const query = reactive({
  pageSize: 25,
  search: "",
  page: 1,
  notUse: null,
});
const showModal = ref(false);
const id = ref(null);
const brands = computed(() => {
  return brandStore.$state.entries.data;
});
const currenPage = computed(() => {
  return brandStore.$state.entries.meta?.currentPage ?? query.page;
});
const pageSize = computed(() => {
  return brandStore.$state.entries.meta?.pageSize ?? query.pageSize;
});
const totalPages = computed(() => {
  return brandStore.$state.entries.meta?.totalPages ?? 1;
});
const totalCount = computed(() => {
  return brandStore.$state.entries.meta?.totalCount ?? 1;
});
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
    title: "Xoá bản ghi?",
    text: "Xác nhận xoá bản ghi này!!!",
    confirmButtonText: "Xác nhận",
    cancelButtonText: "Huỷ",
  }).then((result) => {
    if (result.isConfirmed) {
      console.log("deleting...");
      brandStore
        .delete(id)
        .then((res) => {
          successMessage(res.data?.message ?? "Xoá bản ghi thành công!");
          if (brands.value.length <= 1 && currenPage.value > 1) {
            query.page -= 1;
          }
          getListData();
        })
        .catch((err) => errorMessage(err.response.data.title));
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
  () => query.notUse,
  () => {
    getListData();
  }
);

const getListData = async () => {
  await brandStore.getList(query);
};

onBeforeMount(async () => {
  await getListData();
});
</script>
<template>
  <div class="card">
    <h5 class="card-header">Thương hiệu</h5>
    <div class="d-flex justify-content-between mx-3 my-2">
      <div class="d-flex">
        <div class="d-flex align-items-center w-auto me-2">
          <div class="w-75px me-1">
            <label for="customerCode" class="col-form-label">Tìm kiếm</label>
          </div>
          <div class="w-100px ms-1">
            <input
              type="text"
              class="form-control mb-lg-0 p-2"
              id="customerCode"
              placeholder="Nhập tên, email, sdt.."
              v-model="query.search"
            />
          </div>
        </div>
        <div class="d-flex align-items-center w-auto me-2">
          <select class="form-select" v-model="query.notUse">
            <option selected :value="null">Chọn trạng thái</option>
            <option selected :value="true">Ngưng sử dụng</option>
            <option selected :value="false">Sử dụng</option>
          </select>
        </div>
        <!-- <div class="w-[250px] me-2"><select-search placeholder="-- Vui lòng Chọn --" :listData="customers" display="name" keyValue="id"></select-search></div> -->
        
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
            <th>Mã</th>
            <th>Tên</th>
            <th class="text-center">Hình ảnh</th>
            <th>Trạng thái</th>
            <th class="text-center">Actions</th>
          </tr>
        </thead>
        <tbody class="table-border-bottom-0">
          <tr v-for="(item, index) in brands" :key="index">
            <td>
              <strong>{{ index + 1 }}</strong>
            </td>
            <td class="max-w-[350px]">
              {{ item.code }}
            </td>
            <td class="max-w-[350px]">
              {{ item.name }}
            </td>
            <td class="text-center"> <img :src="item.image" class="h-[45px] object-fill m-auto rounded-md" loading="lazy" /></td>
            <td class="max-w-[350px]">
              {{ item.notUse ? "Ngưng sử dụng" : "Sử dụng" }}
            </td>
            <td class="text-center">
              <button type="button" class="btn btn-sm btn-icon btn-outline-primary me-1" @click="toggleEdit(item.id)">
                <span class="tf-icons bx bx-edit-alt bx-xs"></span>
              </button>
              <button type="button" class="btn btn-sm btn-icon btn-outline-secondary me-1" @click="toggleDelete(item.id)">
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
  <BrandModal v-if="showModal" :id="id" @close="toggleModal" />
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
