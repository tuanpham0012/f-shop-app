<template>
  <modal
    title="Thông tin đơn đặt hàng"
    modal-size="modal-xl"
    @close="
      () => {
        emits('close');
      }
    "
  >
    <template #body>
      <div class="grid grid-cols-12 bg-gray-100 p-3 px-4">
        <div class="col-span-8 bg-white p-3 rounded-md ">
            <div class="mb-2 md:grid md:grid-cols-7">
                <span class="md:col-span-2 font-semibold text-black">Người đặt hàng:</span><span class="md:col-span-5">aa</span>
            </div>
            <div class="mb-2 md:grid md:grid-cols-7">
                <span class="md:col-span-2 font-semibold text-black">Người nhận:</span><span class="md:col-span-5">aa</span>
            </div>
            <div class="mb-2 md:grid md:grid-cols-7">
                <span class="md:col-span-2 font-semibold text-black">SDT nhận hàng:</span><span class="md:col-span-5">aa</span>
            </div>
            <div class="mb-2 md:grid md:grid-cols-7">
                <span class="md:col-span-2 font-semibold text-black">Địa chỉ nhận hàng:</span><span class="md:col-span-5">aa</span>
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
import { useOrderStore } from "@/stores/order";
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

const orderStore = useOrderStore();

const newBrand = reactive({
  id: null,
  name: "",
  code: "",
  image: "",
  notUse: false,
});

const brand = computed<any>(() => (props.id && orderStore.$state.entry ? orderStore.$state.entry : newBrand));

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
    await orderStore
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
    await orderStore
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
    orderStore.show(props.id);
  }
});
</script>
<style lang="scss" scoped>
.modal-body{
    padding: 0;
}
</style>
