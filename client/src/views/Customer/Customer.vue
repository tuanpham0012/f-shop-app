<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useCustomerStore } from "../../stores/customer";
import debounce from "lodash.debounce";
import CustomerModal from "./CustomerModal.vue";
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";

const customerStore = useCustomerStore();
const query = reactive({
  pageSize: 30,
  search: "",
  page: 1,
  status: null,
});
const showModal = ref(false);
const id = ref(null);
const customers = computed(() => {
  return customerStore.$state.customers.data;
});
const currenPage = computed(() => {
  return customerStore.$state.customers.meta?.currentPage ?? query.page;
});
const pageSize = computed(() => {
  return customerStore.$state.customers.meta?.pageSize ?? query.pageSize;
});
const totalPages = computed(() => {
  return customerStore.$state.customers.meta?.totalPages ?? 1;
});
const totalCount = computed(() => {
  return customerStore.$state.customers.meta?.totalCount ?? 1;
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
    title: "Xoá khách hàng?",
    text: "Xác nhận xoá khách hàng!!!",
    confirmButtonText: "Xác nhận",
    cancelButtonText: "Huỷ",
  }).then((result) => {
    if (result.isConfirmed) {
      console.log("deleting...");
      customerStore
        .delete(id)
        .then((res) => {
          successMessage(res.data?.message ?? "Xoá khách hàng thành công!");
          if (customers.value.length <= 1 && currenPage.value > 1) {
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
  () => query.status,
  () => {
    getListData();
  }
);

const getListData = async () => {
  await customerStore.getList(query);
};

const statusTag = (status: number) => {
  switch (status) {
    case 0:
      return '<span class="badge bg-label-warning me-1">Chưa kích hoạt</span>';
    case 1:
      return '<span class="badge bg-label-success me-1">Hoạt động</span>';
    case 2:
      return '<span class="badge bg-label-primary me-1">Đã khoá</span>';
    default:
      break;
  }
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
        <div class="w-[250px] me-2"><select-search placeholder="-- Vui lòng Chọn --" :listData="customers" display="name" keyValue="id"></select-search></div>
        
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
            <th>Khách hàng</th>
            <th>Điện thoại</th>
            <th>Email</th>
            <th>Trạng thái</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody class="table-border-bottom-0">
          <tr v-for="(item, index) in customers" :key="index">
            <td>
              <strong>{{ index + 1 }}</strong>
            </td>
            <td class="max-w-[350px]">
              <strong>{{ item.name }}</strong>
              <p class="m-0 text-break overflow-hidden" :title="item.address">
                <i class="bx bxs-map bx-xs"></i>{{ item.address }}
              </p>
            </td>
            <td>{{ item.phone }}</td>
            <td>{{ item.email }}</td>
            <td v-html="statusTag(item.status)"></td>
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
  <CustomerModal v-if="showModal" :id="id" @close="toggleModal" />
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
