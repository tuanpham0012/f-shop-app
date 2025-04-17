<template>
  <modal
    :title="
      !id ? 'Thêm mới thương hiệu sản phẩm' : 'Cập nhật thương hiệu sản phẩm'
    "
    modal-size="modal-lg"
    @close="
      () => {
        emits('close');
      }
    "
  >
    <template #body>
      <div class="col-sm-12 p-3">
        <div class="row">
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="name" class="form-label required">Tiêu đề </label>
            </div>
            <div class="input-group">
              <input
                type="text"
                class="form-control"
                id="name"
                v-model="brand.name"
              />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-sm-6 mb-3">
            <div class="col-sm-12">
              <label for="code" class="form-label required">Mã </label>
            </div>
            <div class="input-group">
              <input
                type="text"
                class="form-control"
                id="code"
                disabled
                :value="brand.code"
              />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-sm-12 row mb-3">
            <div class="col-sm-8 mb-3">
              <label for="formFile" class="form-label">Hình ảnh</label>
              <input
                class="form-control"
                type="file"
                id="formFile"
                @change="productImage"
                accept="image/png, image/gif, image/jpeg"
              />
            </div>
            <div class="col-sm-4 d-flex py-1 justify-center">
              <img :src="brand.image" loading="lazy" class="h-[75px] rounded-sm" />
            </div>
          </div>
          <div class="col-sm-6 mb-3">
            <input
              class="form-check-input me-2"
              type="checkbox"
              id="not_use"
              v-model="brand.notUse"
            />
            <label for="not_use" class="">Ngưng sử dụng</label>
            <Feedback :errors="errors?.notUse" />
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
import { useBrandStore } from "@/stores/product";
import { successMessage } from "@/helpers/toast";
import { removeVietnameseTones, textCode } from "@/services/utils";
import { resizeImage, createSlug } from "@/helpers/helpers";
const props = defineProps({
  id: {
    type: [Number, String as () => string | null],
    required: false,
    default: null,
  },
});

const emits = defineEmits(["close"]);

const brandStore = useBrandStore();

const newBrand = reactive({
  id: null,
  name: "",
  code: "",
  image: "",
  notUse: false,
});

const brand = computed<any>(() =>
  props.id && brandStore.$state.entry ? brandStore.$state.entry : newBrand
);

const errors = ref<any>(null);

const productImage = (event: any) => {
  // Reference to the DOM input element
  const file = event.target.files[0];
  if (file) {
    const reader = new FileReader();
    reader.onload = async (e: any) => {
      brand.value.image = await resizeImage(e.target.result, file.type, null, null, 250);
    };
    reader.readAsDataURL(file);
  }
  const fileInput = document.getElementById("formFile") as HTMLInputElement;
  fileInput.value = "";
};

const save = async () => {
  if (brand.value.id == null) {
    await brandStore
      .create(brand.value)
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
    await brandStore
      .update(props.id, brand.value)
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

watch(
  () => brand.value.name,
  (value) => {
    brand.value.code = value ? createSlug(value) : "";
  }
);
onBeforeMount(() => {
  if (props.id) {
    brandStore.show(props.id);
  }
});
</script>
<style lang=""></style>
