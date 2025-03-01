<template>
    <modal
        :title="!id ? 'Thêm mới danh mục' : 'Cập nhật danh mục'"
        modal-size="modal-lg"
        @close="
            () => {
                emits('close');
            }
        "
    >
        <template #body>
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="name" class="form-label required"
                                >Danh mục cha</label
                            >
                        </div>
                        <div class="input-group">
                            <select class="form-select" v-model="menu.parentId">
                                <TreeSelect v-for="(menu, index) in menus" :menu="menu" :depth="0" />
                            </select>
                        </div>
                        <Feedback :errors="errors?.parentId" />
                    </div>

                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="title" class="form-label required"
                                >Tên danh mục</label
                            >
                        </div>
                        <div class="input-group">
                            <input
                                type="text"
                                class="form-control"
                                id="title"
                                v-model="menu.title"
                            />
                        </div>
                        <Feedback :errors="errors?.title" />
                    </div>
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="phone" class="form-label">Icon</label>
                            <a
                                href="https://fontawesome.com/v6/search?o=r&m=free"
                                class="text-cyan-500 fs-6"
                                target="_blank"
                                >Tra cứu</a
                            >
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-11">
                                <input
                                    type="text"
                                    class="form-control"
                                    v-model="menu.icon"
                                />
                            </div>
                            <div class="col-sm-1 px-0 justify-items-center align-content-center" v-html="menu.icon"></div>
                        </div>

                        <Feedback :errors="errors?.Phone" />
                    </div>

                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="url" class="form-label"
                                >Đường dẫn</label
                            >
                        </div>
                        <div class="input-group">
                            <input
                                type="text"
                                :disabled="menu.groupMenu"
                                class="form-control"
                                id="url"
                                v-model="menu.url"
                            />
                        </div>
                        <Feedback :errors="errors?.url" />
                    </div>

                    <div class="col-sm-6 mb-3">
                        <div class="form-check">
                            <input
                                class="form-check-input"
                                type="checkbox"
                                v-model="menu.hidden"
                                id="defaultCheck1"
                            />
                            <label class="form-check-label" for="defaultCheck1">
                                Không hiển thị
                            </label>
                        </div>
                        <Feedback :errors="errors?.hidden" />
                    </div>
                    <div class="col-sm-6 mb-3">
                        <div class="form-check">
                            <input
                                class="form-check-input"
                                type="checkbox"
                                v-model="menu.groupMenu"
                                id="groupMenu"
                            />
                            <label class="form-check-label" for="groupMenu">
                                Nhóm Danh Mục
                            </label>
                        </div>
                        <Feedback :errors="errors?.hidden" />
                    </div>
                </div>
            </div>
        </template>
        <template #footer>
            <button class="btn btn-success" @click="save()">Lưu lại</button>
            <button
                class="btn btn-secondary"
                @click="
                    () => {
                        emits('close');
                    }
                "
            >
                Đóng
            </button>
        </template>
    </modal>
</template>
<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useMenuStore } from "@/stores/menu";
import { successMessage } from "@/helpers/toast";
import TreeSelect from "./TreeSelect.vue";
const props = defineProps({
    id: {
        type: [Number, String as () => string | null],
        required: false,
        default: null,
    },
});

const emits = defineEmits(["close"]);

const menuStore = useMenuStore();

const newMenu = reactive({
    id: null,
    title: "",
    url: "",
    icon: "",
    parentId: 2,
    position: 1,
    hidden: false,
    groupMenu: false,
});

const menu = computed(() =>
    props.id && menuStore.$state.menu ? menuStore.$state.menu : newMenu
);

const menus = computed(() => menuStore.$state.entries.data);

const errors = ref<any>(null);

watch( () => menu.value.groupMenu , (newValue) => {
    if(newValue) {
        menu.value.url = "";
        menu.value.position = 0;
    }
})

const save = async () => {
    if (menu.value.id == null) {
        await menuStore
            .create(menu.value)
            .then((res) => {
                console.log(res);
                successMessage(res.data?.message ?? "Thêm mới thành công!");
                emits("close", true);
            })
            .catch((err) => {
                console.log(err);
                errors.value = err.response.data.errors;
            });
    } else {
        await menuStore
            .update(props.id, menu.value)
            .then((res) => {
                successMessage(res.data?.message ?? "Cập nhật thành công!");
                emits("close", true);
            })
            .catch((err) => {
                console.log(err);
                errors.value = err.response.data.errors;
            });
    }
};
onBeforeMount(() => {
    if (props.id) {
        menuStore.show(props.id);
    }
});
</script>
<style lang=""></style>
