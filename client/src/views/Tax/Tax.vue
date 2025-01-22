<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useTaxStore } from "../../stores/tax";
import debounce from "lodash.debounce";
import TaxModal from "./TaxModal.vue";
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";

const taxStore = useTaxStore();
const query = reactive({
  pageSize: 30,
  search: "",
  page: 1,
  status: null,
});
const showModal = ref(false);
const id = ref(null);
const taxes = computed(() => {
  return taxStore.$state.entries.data;
});
const currenPage = computed(() => {
  return taxStore.$state.entries.meta?.currentPage ?? query.page;
});
const pageSize = computed(() => {
  return taxStore.$state.entries.meta?.pageSize ?? query.pageSize;
});
const totalPages = computed(() => {
  return taxStore.$state.entries.meta?.totalPages ?? 1;
});
const totalCount = computed(() => {
  return taxStore.$state.entries.meta?.totalCount ?? 1;
});
const changePage = async (value: any) => {
  console.log(value);

  query.pageSize = value.pageSize;
  query.page = value.currentPage;
  await getListData();
};
const toggleModal = (refresh = null) => {
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
    title: "Xoá dữ liệu?",
    text: "Xác nhận xoá dữ liệu!!!",
    confirmButtonText: "Xác nhận",
    cancelButtonText: "Huỷ",
  }).then((result) => {
    if (result.isConfirmed) {
      console.log("deleting...");
      taxStore
        .delete(id)
        .then((res) => {
          successMessage(res.data?.message ?? "Xoá dữ liệu thành công!");
          if (taxes.value.length <= 1 && currenPage.value > 1) {
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
  () => query.status,
  () => {
    getListData();
  }
);

const getListData = async () => {
  await taxStore.getList(query);
};

onBeforeMount(async () => {
  await getListData();
});
</script>
<template>
  <div class="card">
    <h5 class="card-header">Khách hàng</h5>
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
          <select class="form-select" v-model="query.status">
            <option selected :value="null">Chọn trạng thái</option>
            <option selected :value="0">Chưa kích hoạt</option>
            <option selected :value="1">Hoạt động</option>
            <option selected :value="2">Đã khoá</option>
            <!-- <option
              v-for="(item, index) in props.statuses"
              :key="index"
              :value="index"
            > @click="toggleShowModal()"
              {{ item }}
            </option> -->
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
            <th>Tên</th>
            <th>Tỉ lệ</th>
            <th>Trạng thái</th>
            <th class="text-center">Actions</th>
          </tr>
        </thead>
        <tbody class="table-border-bottom-0">
          <tr v-for="(item, index) in taxes" :key="index">
            <td>
              <strong>{{ index + 1 }}</strong>
            </td>
            <td>
              {{ item.name }}
            </td>
            <td>{{ item.value }}</td>
            <td> {{ item.notUse ? "Ngưng sử dụng" : "Sử dụng" }}</td>
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
  <TaxModal v-if="showModal" :id="id" @close="toggleModal" />
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
