<template lang="">
  <modal
    title="Cập nhật mô tả sản phẩm"
    modal-size="modal-xl"
    @close="
      () => {
        emits('close');
      }
    "
  >
    <template #body>
      <div class="col-sm-12 h-[calc(100vh-350px)] mb-24" v-if="data">
        <TinyEditor
          v-model="data.data"
        />
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
<script setup lang="ts">
import { computed, ref, onBeforeMount } from "vue";
import { useProductStore } from "@/stores/product";
import TinyEditor from "@/components/TinyEditor.vue";
const productStore = useProductStore();
const props = defineProps({
  id: {
    type: [Number, String as () => string | null],
    required: false,
    default: null,
  },
});

const emits = defineEmits(["close"]);

const data = computed<any>(() => productStore.$state.descriptionProduct);

const save = () => {
  productStore
    .updateDes(props.id, { description: data.value.data })
    .then((res) => emits("close", true))
    .catch((err) => console.log(err));
};

onBeforeMount(() => {
  productStore.getDescriptionProductById(props.id);
});
</script>
<style lang=""></style>
