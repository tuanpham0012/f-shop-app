<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch, type ComputedRef } from "vue";
import { useCategoryStore } from "../../stores/product";
import debounce from "lodash.debounce";
import CategoryModal from "./CategoryModal.vue";
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";
import TableCategoryRow from "./TableCategoryRow.vue";

const categoryStore = useCategoryStore();
const query = reactive({
  notUse: null,
});
const showModal = ref(false);
const id = ref(null);
const categories: ComputedRef<any> = computed(() => {
  return categoryStore.$state.listTree.data;
});
// const currenPage = computed(() => {
//   return categoryStore.$state.listTree.meta?.currentPage ?? query.page;
// });
// const pageSize = computed(() => {
//   return categoryStore.$state.listTree.meta?.pageSize ?? query.pageSize;
// });
// const totalPages = computed(() => {
//   return categoryStore.$state.listTree.meta?.totalPages ?? 1;
// });
// const totalCount = computed(() => {
//   return categoryStore.$state.listTree.meta?.totalCount ?? 1;
// });
// const changePage = async (value: any) => {
//   console.log(value);

//   query.pageSize = value.pageSize;
//   query.page = value.currentPage;
//   await getListData();
// };
const toggleModal = (refresh = false) => {
  showModal.value = !showModal.value;
  getListData();
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
    title: "Xoá dữ  liệu?",
    text: "Xác nhận xoá dữ  liệu!!!",
    confirmButtonText: "Xác nhận",
    cancelButtonText: "Huỷ",
  }).then((result) => {
    if (result.isConfirmed) {
      console.log("deleting...");
      categoryStore
        .delete(id)
        .then((res) => {
          successMessage(res.data?.message ?? "Xoá dữ  liệu thành công!");
          getListData();
        })
        .catch((err) => errorMessage(err.response.data.title));
    }
  });
};

const getListData = async () => {
  await categoryStore.getListTree(query);
};


onBeforeMount(async () => {
  await getListData();
});
</script>
<template>
  <div class="card">
    <h5 class="card-header">Danh mục sản phẩm</h5>
    <div class="d-flex justify-content-between mx-3 my-2">
      <div class="d-flex">
        <div class="d-flex align-items-center w-auto me-2">
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
            <th>Tên</th>
            <th>Số lượng sản phẩm</th>
            <th>Trạng thái</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody class="table-border-bottom-0">
            <TableCategoryRow v-for="(item, index) in categories" :key="index" :entry="item" @toggle-edit="toggleEdit" @toggle-delete="toggleDelete" />
        </tbody>
      </table>
    </div>
    <!-- <Pagination
      :current-page="currenPage"
      :page-size="pageSize"
      :total-pages="totalPages"
      :total-count="totalCount"
      @change-page="changePage"
    /> -->
  </div>
  <CategoryModal v-if="showModal" :id="id" @close="toggleModal" />
</template>

<style lang="scss" scoped>
.table-scroll {
  height: calc(100vh - 330px);
}
</style>
