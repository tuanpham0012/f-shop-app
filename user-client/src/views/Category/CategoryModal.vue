<template>
  <modal
    :title="!id ? 'Thêm mới danh  mục sản phẩm' : 'Cập nhật danh  mục sản phẩm'"
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
              <label for="name" class="form-label required">Mã</label>
            </div>
            <div class="input-group">
              <input
                type="text"
                class="form-control"
                id="name"
                v-model="category.code"
              />
            </div>
            <Feedback :errors="errors?.Code" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="name" class="form-label required">Tên</label>
            </div>
            <div class="input-group">
              <input
                type="text"
                class="form-control"
                id="name"
                v-model="category.name"
              />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="parentId" class="form-label required"
                >Danh mục cha</label
              >
            </div>
            <div class="input-group">
              <select
                class="form-select"
                id="parentId"
                v-model="category.parentId"
              >
                <option :value="null">Chọn danh mục cha</option>
                <SelectTree v-for="(item, index) in categories" :entry="item" />
              </select>
            </div>
            <Feedback :errors="errors?.parentId" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="name" class="form-label"></label>
            </div>
            <div class="input-group mt-2">
              <input
                class="form-check-input me-2"
                type="checkbox"
                id="not_use"
                v-model="category.notUse"
              />
              <label for="not_use" class="">Ngưng sử dụng</label>
            </div>

            <Feedback :errors="errors?.Email" />
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
import {
  ref,
  reactive,
  computed,
  watch,
  onBeforeMount,
  type ComputedRef,
} from "vue";
import { useCategoryStore } from "@/stores/product";
import { successMessage } from "@/helpers/toast";
import { textCode } from "@/helpers/helpers";
const props = defineProps({
  id: {
    type: [Number, String as () => string | null],
    required: false,
    default: null,
  },
});

const emits = defineEmits(["close"]);

const categoryStore = useCategoryStore();

const newCategory = reactive({
  id: null,
  name: "",
  code: "",
  lft: 0,
  parentId: null,
  notUse: false,
});

const category = computed(() =>
  props.id && categoryStore.$state.entry
    ? categoryStore.$state.entry
    : newCategory
);

const categories: ComputedRef<any> = computed(() => {
  return categoryStore.$state.listTree.data;
});

const errors = ref<any>(null);

watch(
  () => category.value.code,
  async (value) => {
    if (value) {
      category.value.code = textCode(value);
    }
  }
);

const save = async () => {
  if (category.value.id == null) {
    await categoryStore
      .create(category.value)
      .then((res) => {
        console.log(res);
        successMessage(res.data?.message ?? "Thêm mới thành công!");
        emits("close", res.data.data);
      })
      .catch((err) => {
        console.log(err);
        errors.value = err.response.data.errors;
      });
  } else {
    await categoryStore
      .update(props.id, category.value)
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
onBeforeMount(async () => {
  if (props.id) {
    await categoryStore.show(props.id);
  }
  await categoryStore.getListTree({ notUse: false });
});
</script>
<style lang=""></style>
