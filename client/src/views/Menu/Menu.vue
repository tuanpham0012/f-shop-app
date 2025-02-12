<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useMenuStore } from "../../stores/menu";
import debounce from "lodash.debounce";
import MenuModal from "./MenuModal.vue";
import TableMenuRow from "./TableMenuRow.vue";
import { confirmAlert, successMessage, errorMessage } from "@/helpers/toast";

const menuStore = useMenuStore();
const query = reactive({
    type: null,
});
const showModal = ref(false);
const id = ref(null);
interface MenuItem {
    id: number;
    title: string;
    icon: string;
    url: string;
    hidden: boolean;
}

const menus = computed<MenuItem[]>(() => menuStore.$state.entries.data);

const toggleModal = (refresh = false) => {
    showModal.value = !showModal.value;
    if (refresh) getListData();
};
const toggleEdit = (value: any) => {
    console.log(value);
    
    id.value = value;
    toggleModal();
};

const toggleCreate = () => {
    id.value = null;
    toggleModal();
};

watch(
    () => query.type,
    () => {
        getListData();
    }
);

const getListData = async () => {
    await menuStore.getList(query);
};

onBeforeMount(async () => {
    await getListData();
});
</script>
<template>
    <div class="card">
        <h5 class="card-header">Cấu hình menu</h5>
        <div class="d-flex justify-content-between mx-3 my-2">
            <!-- <div class="d-flex">
                <div class="d-flex align-items-center w-auto me-2">
                    <select class="form-select" v-model="query.type">
                        <option selected :value="null">-- Tất cả--</option>
                        <option selected :value="2">Admin</option>
                        <option selected :value="1">User</option>
                    </select>
                </div>
            </div> -->
            <button class="btn btn-primary" @click="toggleCreate()">
                <i class="feather icon-plus"></i>
                Thêm mới
            </button>
        </div>
        <div class="table-responsive text-nowrap table-scroll">
            <table class="table table-hover table-bordered">
                <thead class="table-light">
                    <tr>
                        <th>Tên</th>
                        <th class="text-center">Icon</th>
                        <th>Path</th>
                        <th>Trạng thái</th>
                        <th class="text-center">Actions</th>
                    </tr>
                </thead>
                <tbody class="table-border-bottom-0">
                    <TableMenuRow v-for="(item, index) in menus" :key="index" :menu="item" :depth="0" @toggle-edit="toggleEdit"/>
                </tbody>
            </table>
        </div>
    </div>
    <MenuModal v-if="showModal" :id="id" @close="toggleModal" />
</template>

<style lang="scss" scoped>
.table-scroll {
    height: calc(100vh - 330px);
}
</style>
