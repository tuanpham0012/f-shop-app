<template>
  <modal
    title="Thêm mới đơn hàng"
    modal-size="modal-xl"
    @close="
      () => {
        emits('close');
      }
    "
  >
    <template #body>
      <div class="p-3 min-h-[75vh] bg-gray-100">
        <div class="grid grid-cols-12 gap-3 bg-white p-3 rounded-md">
          <div class="col-span-4">
            <div class="col-span-4">
              <label for="name" class="form-label required">Khách hàng</label>
            </div>
            <div class="input-group">
              <input type="text" class="form-control" id="name" v-model="newOrder.code" />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-span-4">
            <div class="col-sm-12">
              <label for="code" class="form-label required">Tên người nhận </label>
            </div>
            <div class="input-group">
              <input type="text" class="form-control" id="code" :value="newOrder.code" />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-span-4">
            <div class="col-sm-12">
              <label for="code" class="form-label required">SDT nhận hàng </label>
            </div>
            <div class="input-group">
              <input type="text" class="form-control" id="code" :value="newOrder.code" />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-span-8">
            <div class="col-sm-12">
              <label for="code" class="form-label required">Địa chỉ nhận hàng </label>
            </div>
            <div class="input-group">
              <input type="text" class="form-control" id="code" :value="newOrder.code" />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-span-4">
            <div class="col-sm-12">
              <label for="code" class="form-label required">Hình thức vận chuyển </label>
            </div>
            <div class="input-group">
              <input type="text" class="form-control" id="code" :value="newOrder.code" />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-span-4">
            <div class="col-sm-12">
              <label for="code" class="form-label required"
                >Phương thức thanh toán
              </label>
            </div>
            <div class="input-group">
              <input type="text" class="form-control" id="code" :value="newOrder.code" />
            </div>
            <Feedback :errors="errors?.Name" />
          </div>
        </div>
        <div class="grid grid-cols-12 gap-3 bg-white p-3 rounded-md mt-2">
          <div class="col-span-6">
            <div class="col-span-4">
              <label for="name" class="form-label required">Sản phẩm</label>
            </div>
            <InputSearch
              v-model="searchProduct"
              :placeholder="'Tìm kiếm sản phẩm...'"
              :filteredItems="searchProducts"
              @change-searchQuery="searchProduct"
            />
            <Feedback :errors="errors?.Name" />
          </div>
          <div class="col-span-12 border-t border-gray-200">
            <div class="flex flex-col justify-start items-start w-full space-y-1">
              <div
                class="flex flex-col justify-start items-start bg-gray-50 py-3 px-6 w-full"
                v-for="(item, index) in newOrder.orderDetails"
                :key="index"
              >
                <div class="flex flex-row justify-start items-center space-x-6 w-full">
                  <div class="flex w-[160px] aspect-square">
                    <img
                      class="w-full object-contain"
                      :src="item.sku.imagePath"
                      alt="img"
                    />
                  </div>
                  <div
                    class="flex-row flex justify-between items-start w-full space-y-2 pb-2"
                  >
                    <div class="w-full flex flex-col justify-start items-start space-y-4">
                      <h3 class="text-base font-semibold leading-6 text-gray-800">
                        {{ item.productName }}
                      </h3>
                      <div class="flex justify-start items-start flex-col space-y-2">
                        <p
                          class="grid grid-cols-3 w-full text-sm leading-none text-gray-800"
                          v-for="(variant, index) in item.sku.variants"
                          :key="index"
                        >
                          <span class="text-gray-400">{{ variant.optionName }}: </span>
                          <span class="col-span-2">{{ variant.valueName }}</span>
                        </p>
                      </div>
                    </div>
                    <div class="flex justify-between space-x-6 items-start w-full">
                      <p class="text-sm leading-6">
                        {{ displayPrice(item.unitPrice) }} đ
                        <span
                          class="text-red-300 line-through"
                          v-if="item.discountAmount && item.discountAmount > 0"
                        >
                          $45.00</span
                        >
                      </p>
                      <p class="text-sm leading-6 text-gray-800">x{{ item.quantity }}</p>
                      <p class="text-sm font-semibold leading-6 text-gray-800">
                        {{ displayPrice(item.totalAmount) }} đ
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
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
import { useProductStore } from "@/stores/product";
import { successMessage } from "@/helpers/toast";
import { displayPrice, textCode } from "@/services/utils";
import { createSlug } from "@/helpers/helpers";
import InputSearch from "@/components/input-form/InputSearch.vue";
import debounce from "lodash.debounce";

const emits = defineEmits(["close"]);

const productStore = useProductStore();

const searchProducts = computed(() => {
  return productStore.searchProduct.data ?? [];
});

const newOrder = reactive({
  id: null,
  code: "",
  customerId: 1,
  totalAmount: 0,
  totalPrice: 0,
  orderDetails: [] as any,
});

const errors = ref<any>(null);

const searchProduct = (searchValue:string) => {
console.log("Search value changed:", searchValue);
    
  if (searchValue.length > 0) {
    productStore.getListSearchProduct(searchValue);
  }
}


const save = async () => {
  // if (brand.value.id == null) {
  //     await brandStore
  //         .create(brand.value)
  //         .then((res) => {
  //             console.log(res);
  //             successMessage(res.data?.message ?? "Thêm mới thành công!");
  //             emits("close", res.data.data);
  //         })
  //         .catch((err) => {
  //             console.log(err);
  //             errors.value = err.response.data.errors;
  //         });
  // } else {
  //     await brandStore
  //         .update(props.id, brand.value)
  //         .then((res) => {
  //             successMessage(res.data?.message ?? "Cập nhật thành công!");
  //             emits("close", true);
  //         })
  //         .catch((err) => {
  //             console.log(err);
  //             errors.value = err.response.data.errors;
  //         });
  // }
};

onBeforeMount(async() => { });
</script>
<style lang=""></style>
