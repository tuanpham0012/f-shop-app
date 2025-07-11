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
                        <SelectSearchApi :placeholder="'Tìm kiếm khách hàng...'" :url="`${apiUrl}/customers`" @item-selected="selectCustomer" displayKey="displayName"/>
                        <Feedback :errors="errors?.Name" />
                    </div>
                    <div class="col-span-4">
                        <div class="col-sm-12">
                            <label for="code" class="form-label required">Tên người nhận </label>
                        </div>
                        <div class="input-group">
                            <input type="text" class="form-control" id="code" :value="newOrder.receiverName" />
                        </div>
                        <Feedback :errors="errors?.receiverName" />
                    </div>
                    <div class="col-span-4">
                        <div class="col-sm-12">
                            <label for="code" class="form-label required">SDT nhận hàng </label>
                        </div>
                        <div class="input-group">
                            <input type="text" class="form-control" id="code" :value="newOrder.phoneNumber" />
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>

                    <div class="col-span-4">
                        <div class="col-sm-12">
                            <label for="code" class="form-label required">Hình thức vận chuyển </label>
                        </div>
                        <div class="input-group">
                            <select class="form-select" v-model="newOrder.shippingUnitId">
                                <option :value="null">Chọn hình thức vận chuyển</option>
                                <option v-for="item in shippingUnits" :key="item.id" :value="item.id">{{ item.name }}</option>
                            </select>
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>
                    <div class="col-span-4">
                        <div class="col-sm-12">
                            <label for="code" class="form-label required">Phương thức thanh toán </label>
                        </div>
                        <div class="input-group">
                            <select class="form-select" v-model="newOrder.paymentMethodId">
                                <option :value="null">Chọn hình thức thanh toán</option>
                                <option v-for="item in paymentMethods" :key="item.id" :value="item.id">{{ item.name }}</option>
                            </select>
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>
                    <div class="col-span-8">
                        <div class="col-sm-12">
                            <label for="code" class="form-label required">Địa chỉ nhận hàng </label>
                        </div>
                        <div class="input-group">
                            <input type="text" class="form-control" id="code" :value="newOrder.address" />
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>
                    <div class="col-span-12">
                        <label for="name" class="form-label text-gray-500">Ghi chú cho người bán</label>
                        <textarea cols="" type="text" class="form-control" style="height: 100px" v-model="newOrder.note" placeholder="Nhập vào..."></textarea>
                    </div>
                </div>
                <div class="grid grid-cols-12 gap-3 bg-white p-3 rounded-md mt-2">
                    <div class="col-span-12">
                        <div class="col-span-4">
                            <label for="name" class="form-label">Sản phẩm</label>
                        </div>
                        <InputSearch :placeholder="'Tìm kiếm sản phẩm...'" :url="`${apiUrl}/admin/products/search`" @item-selected="selectProduct" />
                        <Feedback :errors="errors?.Name" />
                    </div>
                    <div class="col-span-12 border-gray-200 min-h-32">
                        <div class="flex flex-col justify-start items-start w-full space-y-1">
                            <div class="flex flex-col justify-start items-start bg-gray-50 py-2 px-4 w-full" v-for="(item, index) in newOrder.orderDetails" :key="index">
                                <div class="flex flex-row justify-start items-center space-x-6 w-full">
                                    <div class="flex w-[120px] aspect-square">
                                        <img class="w-full object-contain" :src="item.imagePath" alt="img" />
                                    </div>
                                    <div class="flex-row flex justify-between items-start w-full space-y-2 pb-2">
                                        <div class="w-full flex flex-col justify-start items-start space-y-1">
                                            <h3 class="text-base font-semibold leading-6 text-gray-800">
                                                {{ item.productName }}
                                            </h3>
                                            <div class="flex justify-start items-start flex-col space-y-2">
                                                <span class="text-gray-400">{{ item.variant }} </span>
                                            </div>
                                        </div>
                                        <div class="flex justify-between space-x-6 items-center w-full">
                                            <p class="text-sm leading-6">
                                                {{ displayPrice(item.unitPrice) }} đ
                                                <span class="text-red-300 line-through" v-if="item.discountAmount && item.discountAmount > 0"> $45.00</span>
                                            </p>
                                            <p class="text-sm leading-6 text-gray-800"><input class="form-control text-right" type="number" v-model="item.quantity" min="1" /></p>
                                            <p class="text-sm font-semibold leading-6 text-gray-800">{{ displayPrice(item.totalAmount) }} đ</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bg-white mt-3 p-3 rounded-md">
                    <div class="gap-2 grid grid-cols-12">
                        <div class="right-summary col-span-12 md:col-span-6 md:col-start-7">
                            <div class="grid grid-cols-5 gap-0 md:gap-2 px-4 pt-3 pb-1">
                                <p class="checkout-titles text-lg col-span-5 py-1 text-gray-600 m-0 flex md:hidden">Chi tiết thanh toán</p>
                                <div class="total-label col-span-3 flex items-center gap-1">
                                    <span>Tổng tiền hàng:</span>
                                    <span class="text-xs text-gray-400">({{ newOrder.orderDetails.length }} sản phẩm)</span>
                                </div>
                                <div class="text-right text-sm col-span-2">{{ displayPrice(newOrder.totalPrice) + " đ" }}</div>
                                <div class="total-label col-span-3">
                                    <span>Phí vận chuyển:</span>
                                </div>
                                <div class="text-right text-sm col-span-2">{{ displayPrice(newOrder.totalPrice) + " đ" }}</div>
                                <div class="total-label col-span-3">
                                    <span>Giảm giá:</span>
                                </div>
                                <div class="text-right text-sm col-span-2">{{ displayPrice(newOrder.totalPrice) + " đ" }}</div>
                                <div class="total-label col-span-3">
                                    <span>Tổng thanh toán:</span>
                                </div>
                                <div class="total-price text-right text-sm font-semibold col-span-2">{{ displayPrice(newOrder.totalPrice) + " đ" }}</div>
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
import { useCommonStore } from "@/stores/common";
import { useCustomerStore } from "@/stores/customer";
import SelectSearch from "@/components/input-form/SelectSearch.vue";
import { apiUrl } from "@/helpers/config";
import { useOrderStore } from "../../stores/order";

const commonStore = useCommonStore();
const customerStore = useCustomerStore();
const orderStore = useOrderStore();

const paymentMethods = computed<any>(() => commonStore.$state.paymentMethods.data);
const shippingUnits = computed<any>(() => commonStore.$state.shippingUnits.data);

const searchCustomers = computed(() => {
    return customerStore.searchCustomer.data ?? [];
});

const emits = defineEmits(["close"]);

const newOrder = reactive({
    id: null,
    code: "",
    paymentMethodId: null,
    shippingUnitId: null,
    address: "",
    phoneNumber: "",
    receiverName: "",
    customerId: 1,
    totalAmount: 0,
    totalPrice: 0,
    note: "",
    orderDetails: [] as any,
});

const errors = ref<any>(null);


const selectCustomer = (item: any) => {
    newOrder.customerId = item.id;
    console.log("Selected customer:", item);
    newOrder.receiverName = item.name;
    newOrder.phoneNumber = item.phone;
    newOrder.address = item.address;
};

const selectProduct = (item: any) => {
    const existingItem = newOrder.orderDetails.findIndex((detail: any) => detail.skuId === item.id);
    if (existingItem !== -1) {
        newOrder.orderDetails[existingItem].quantity++;
        newOrder.orderDetails[existingItem].totalAmount = newOrder.orderDetails[existingItem].unitPrice * newOrder.orderDetails[existingItem].quantity;
        return;
    }
    newOrder.orderDetails.push({
        productId: item.productId,
        productName: item.productName,
        skuId: item.id,
        unitPrice: item.price,
        quantity: 1,
        totalAmount: item.price,
        discountAmount: 0,
        imagePath: item.imagePath,
        variant: item.name,
    });
};

watch(
    () => newOrder.orderDetails,
    (newValue) => {
        newOrder.totalPrice = newValue.reduce((total: any, item: any) => total + item.totalAmount, 0);
    },
    { deep: true }
);

const save = async () => {
    await orderStore
            .create(newOrder)
            .then((res) => {
                successMessage(res.data?.message ?? "Tạo đơn hàng thành công!");
                emits("close", true);
            })
            .catch((err) => {
                console.log(err);
                errors.value = err.response.data.errors;
            });
};

onBeforeMount(async () => {
    await commonStore.getPaymentMethods();
    await commonStore.getShippingUnits();

    await customerStore.getListSearchCustomer({
        page: 1,
        pageSize: 10,
        search: "",
    });
});
</script>
<style lang=""></style>
