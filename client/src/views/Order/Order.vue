<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useOrderStore } from "../../stores/order";
import debounce from "lodash.debounce";
import { displayPrice} from '@/services/utils'
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";
import OrderDetailModal from './OrderDetailModal.vue'

const orderStore = useOrderStore();
const query = reactive({
  pageSize: 25,
  search: "",
  page: 1,
  notUse: null,
});
const showModal = ref(false);
const id = ref(null);
const orders = computed<any>(() => {
  return orderStore.$state.entries.data;
});
const currenPage = computed(() => {
  return orderStore.$state.entries.meta?.currentPage ?? query.page;
});
const pageSize = computed(() => {
  return orderStore.$state.entries.meta?.pageSize ?? query.pageSize;
});
const totalPages = computed(() => {
  return orderStore.$state.entries.meta?.totalPages ?? 1;
});
const totalCount = computed(() => {
  return orderStore.$state.entries.meta?.totalCount ?? 1;
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

const copyTextToClipboard = async (text: any) => {
  if (!navigator.clipboard) {
    // Clipboard API không được hỗ trợ (ví dụ: trên HTTP hoặc trình duyệt cũ)
    // Bạn có thể thêm phương pháp dự phòng ở đây (xem bên dưới)
    console.error("Clipboard API không khả dụng.");
    alert("Không thể sao chép. Trình duyệt hoặc kết nối không hỗ trợ.");
    return;
  }
  try {
    await navigator.clipboard.writeText(text);
    console.log("Đã sao chép vào clipboard:", text);
    // Tùy chọn: Hiển thị thông báo thành công cho người dùng
    alert("Đã sao chép thành công!");
  } catch (err) {
    console.error("Lỗi khi sao chép:", err);
    alert("Không thể sao chép. Đã xảy ra lỗi.");
  }
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
  await orderStore.getList(query);
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
            <input type="text" class="form-control mb-lg-0 p-2" id="customerCode" placeholder="Nhập tên, email, sdt.." v-model="query.search" />
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
            <th class="text-center">STT</th>
            <th>Mã đơn hàng</th>
            <th>Tổng tiền</th>
            <th>Người đặt</th>
            <th>Người nhận</th>
            <th>SDT nhận</th>
            <th>Địa chỉ nhận hàng</th>
            <th>Người xử lý</th>
            <th>Trạng thái</th>
            <th class="text-center">Actions</th>
          </tr>
        </thead>
        <tbody class="table-border-bottom-0">
          <tr v-for="(item, index) in orders" :key="index">
            <td class="text-center">
              <strong>{{ index + 1 }}</strong>
            </td>
            <td class="">
              <div class="flex gap-2">
                <span class="limmit-text max-w-[12rem]">{{ item.code }}</span>
                <i class="fa-regular fa-copy cursor-pointer" @click="copyTextToClipboard(item.code)"></i>
              </div>
            </td>
            <td class="text-right">
              {{ `${displayPrice(item.totalAmount)} đ` }}
            </td>
            <td class="">
              {{ item.customer?.name }}
            </td>
            <td class="">{{ item.receiverName }}</td>
            <td class="">{{ item.shippingPhone }}</td>
            <td class=""><span class="limmit-text max-w-[15rem]">{{ item.shippingAddress }}</span></td>
            <td class="">{{ item.user?.name }}</td>
            <td class="">
              {{ item.textStatus }}
            </td>
            <td class="text-center">
              <button type="button" class="btn btn-sm btn-icon btn-outline-primary me-1" @click="toggleEdit(item.id)">
                <span class="tf-icons bx bx-edit-alt bx-xs"></span>
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <Pagination :current-page="currenPage" :page-size="pageSize" :total-pages="totalPages" :total-count="totalCount" @change-page="changePage" />
  </div>
  <OrderDetailModal :id="6" />
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
