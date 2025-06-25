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
            <div class="grid grid-cols-12 bg-gray-100 p-2 gap-2" v-if="order">
                <div class="col-span-8 grid grid-cols-12 gap-2">
                    <div class="bg-white overflow-hidden shadow rounded-lg border col-span-12">
                        <div class="px-4 py-2 sm:px-6">
                            <div class="flex justify-between items-center">
                                <h3 class="text-lg leading-6 font-medium text-gray-900 my-2">Thông tin người nhận</h3>
                                <!-- <button class="text-sm font-medium text-gray-500">Update</button> -->
                            </div>
                        </div>
                        <div class="border-t border-gray-200 px-4 py-5 sm:p-0">
                            <dl class="sm:divide-y sm:divide-gray-200">
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Người đặt hàng</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.customer?.name }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Người nhận hàng</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.receiverName }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">SDT nhận hàng</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.shippingPhone }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Địa chỉ nhận hàng</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.shippingAddress }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Ngày đặt</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.orderDate }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Ngày nhận</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.orderDate }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Đơn vị vận chuyển</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.shippingUnit.name }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Phương thức thanh toán</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.paymentMethod.name }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Tình trạng</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.textStatus }}</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Ghi chú</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ order.note }}</dd>
                                </div>
                            </dl>
                        </div>
                    </div>
                    <div class="bg-white overflow-hidden shadow rounded-lg border col-span-12">
                        <div class="px-4 py-2 sm:px-6">
                            <div class="flex justify-between items-center">
                                <h3 class="text-lg leading-6 font-medium text-gray-900 my-2">Thông tin đơn hàng</h3>
                            </div>
                        </div>
                        <div class="border-t border-gray-200 px-4 py-5 sm:p-0">
                            <dl class="sm:divide-y sm:divide-gray-200">
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Tổng tiền hàng</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ displayPrice(order.totalPrice) }} đ</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Phí vận chuyển</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ displayPrice(order.shippingFee) }} đ</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Giảm giá</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ displayPrice(order.discountAmount) }} đ</dd>
                                </div>
                                <div class="py-2 sm:pt-4 sm:pb-2 sm:grid sm:grid-cols-3 sm:gap-2 sm:px-6">
                                    <dt class="text-sm font-medium text-gray-500">Tổng thanh toán</dt>
                                    <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ displayPrice(order.totalAmount) }} đ</dd>
                                </div>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="bg-white overflow-hidden shadow rounded-lg border box col-span-4">
                    <div class="px-4 py-5 sm:px-6">
                        <div class="flex justify-between items-center">
                            <h3 class="text-lg leading-6 font-medium text-gray-900">Thông tin giao hàng</h3>
                        </div>
                        <p class="mt-1 max-w-2xl text-sm text-gray-500">You can find the project sites assigned to this contact below.</p>
                    </div>
                    <div class="border-t border-gray-200 px-4 py-5 sm:p-0">
                        <dl class="sm:divide-y sm:divide-gray-200">
                            <div class="py-3 sm:py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                                <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                                    RG 301 <br />
                                    Wahl Ranch
                                </dd>
                                <button class="text-sm font-medium text-gray-500">Remove</button>
                            </div>
                            <div class="py-3 sm:py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                                <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                                    RG 301 <br />
                                    Lagrand Valley
                                </dd>
                                <button class="text-sm font-medium text-gray-500">Remove</button>
                            </div>
                            <div class="py-3 sm:py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                                <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                                    RG 301 <br />
                                    Wahl Ranch
                                </dd>
                                <button class="text-sm font-medium text-gray-500">Remove</button>
                            </div>
                            <div class="py-3 sm:py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                                <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                                    RG 301 <br />
                                    Lagrand Valley
                                </dd>
                                <button class="text-sm font-medium text-gray-500">Remove</button>
                            </div>
                            <div class="py-3 sm:py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                                <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                                    RG 301 <br />
                                    Wahl Ranch
                                </dd>
                                <button class="text-sm font-medium text-gray-500">Remove</button>
                            </div>
                        </dl>
                    </div>
                </div>
                <div class="bg-white overflow-hidden shadow rounded-lg border box col-span-12">
                    <div class="px-4 py-5 sm:px-6">
                        <div class="flex justify-between items-center">
                            <h3 class="text-lg leading-6 font-medium text-gray-900">Chi tiết đơn hàng</h3>
                        </div>
                        <!-- <p class="mt-1 max-w-2xl text-sm text-gray-500">You can find the project sites assigned to this contact below.</p> -->
                    </div>
                    <div class="border-t border-gray-200">
                        <div class="flex flex-col justify-start items-start w-full space-y-1">
                            <div class="flex flex-col justify-start items-start bg-gray-50 py-3 px-6 w-full" v-for="(item, index) in order.orderDetails" :key="index">
                                <div class="flex flex-row justify-start items-center space-x-6 w-full">
                                    <div class="flex w-[130px] aspect-square">
                                        <img class="w-full object-contain" :src="item.sku.imagePath" alt="img" />
                                    </div>
                                    <div class="flex-row flex justify-between items-start w-full space-y-2 pb-2">
                                        <div class="w-full flex flex-col justify-start items-start space-y-3">
                                            <h3 class="text-base font-semibold leading-6 text-gray-800">{{ item.productName }}</h3>
                                            <div class="flex justify-start items-start flex-col space-y-1">
                                                <p class="grid grid-cols-3 w-full text-sm leading-none text-gray-800 space-x-1" v-for="(variant, index) in item.sku.variants" :key="index">
                                                    <span class="text-gray-400">{{ variant.optionName }}: </span> <span class="col-span-2">{{ variant.valueName }}</span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="flex justify-between space-x-6 items-start w-full">
                                            <p class="text-sm leading-6">{{ displayPrice(item.unitPrice) }}đ <span class="text-red-300 line-through" v-if="item.discountAmount && item.discountAmount > 0"> $45.00</span></p>
                                            <p class="text-sm leading-6 text-gray-800">x{{ item.quantity }}</p>
                                            <p class="text-sm font-semibold leading-6 text-gray-800">{{ displayPrice(item.totalAmount) }}đ</p>
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
import { displayPrice } from "@/services/utils";
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

const order = computed<any>(() => (props.id && orderStore.$state.entry ? orderStore.$state.entry : null));

const errors = ref<any>(null);

onBeforeMount(() => {
    if (props.id) {
        orderStore.show(props.id);
    }
});
</script>
<style lang="scss" scoped></style>
