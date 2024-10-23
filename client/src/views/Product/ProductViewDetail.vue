<template lang="">
    <modal
        title="Chi tiết sản phẩm"
        modal-size="modal-xl"
        @close="
            () => {
                emits('close');
            }
        "
    >
        <template #body>
            <div class="row gx-5" v-if="product">
                <div class="col-lg-6">
                    <div
                        class="border rounded-4 mb-3 d-flex justify-content-center"
                    >
                        <a
                            data-fslightbox="mygalley"
                            class="rounded-4"
                            target="_blank"
                            data-type="image"
                            href="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big.webp"
                        >
                            <img
                                style="
                                    max-width: 100%;
                                    max-height: 100vh;
                                    margin: auto;
                                "
                                class="rounded-4 fit"
                                src="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big.webp"
                            />
                        </a>
                    </div>
                    <div class="d-flex justify-content-center mb-3">
                        <a
                            data-fslightbox="mygalley"
                            class="border mx-1 rounded-2 item-thumb"
                            target="_blank"
                            data-type="image"
                            href="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big1.webp"
                        >
                            <img
                                width="60"
                                height="60"
                                class="rounded-2"
                                src="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big1.webp"
                            />
                        </a>
                        <a
                            data-fslightbox="mygalley"
                            class="border mx-1 rounded-2 item-thumb"
                            target="_blank"
                            data-type="image"
                            href="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big2.webp"
                        >
                            <img
                                width="60"
                                height="60"
                                class="rounded-2"
                                src="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big2.webp"
                            />
                        </a>
                        <a
                            data-fslightbox="mygalley"
                            class="border mx-1 rounded-2 item-thumb"
                            target="_blank"
                            data-type="image"
                            href="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big3.webp"
                        >
                            <img
                                width="60"
                                height="60"
                                class="rounded-2"
                                src="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big3.webp"
                            />
                        </a>
                        <a
                            data-fslightbox="mygalley"
                            class="border mx-1 rounded-2 item-thumb"
                            target="_blank"
                            data-type="image"
                            href="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big4.webp"
                        >
                            <img
                                width="60"
                                height="60"
                                class="rounded-2"
                                src="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big4.webp"
                            />
                        </a>
                        <a
                            data-fslightbox="mygalley"
                            class="border mx-1 rounded-2 item-thumb"
                            target="_blank"
                            data-type="image"
                            href="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big.webp"
                        >
                            <img
                                width="60"
                                height="60"
                                class="rounded-2"
                                src="https://mdbcdn.b-cdn.net/img/bootstrap-ecommerce/items/detail1/big.webp"
                            />
                        </a>
                    </div>
                    <!-- thumbs-wrap.// -->
                    <!-- gallery-wrap .end// -->
                </div>
                <div class="col-lg-6">
                    <div class="ps-lg-3">
                        <h4 class="title text-dark">
                            {{ product.name }}
                            <br />
                            {{ product.category?.name }}
                        </h4>
                        <div class="d-flex flex-row my-3">
                            <div class="text-warning mb-1 me-2">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fas fa-star-half-alt"></i>
                                <span class="ms-1"> 4.5 </span>
                            </div>
                            <span class="text-muted"
                                ><i
                                    class="fas fa-shopping-basket fa-sm mx-1"
                                ></i
                                >154 orders</span
                            >
                            <span class="text-success ms-2">In stock</span>
                        </div>

                        <div class="mb-4 bg-gray-100 p-3 rounded-sm">
                            <span class="h5">{{
                                displayPrice(
                                    skuSelect?.price ?? product.price
                                ) + " đ"
                            }}</span>
                        </div>

                        <div class="row mt-5 mb-4">
                            <div
                                class="row col-12 mb-4"
                                v-for="(option, i) in product.options"
                                :key="i"
                            >
                                <div class="col-md-3">
                                    <label class="mb-2"
                                        >{{ option.name }}:</label
                                    >
                                </div>
                                <div class="d-flex gap-1 col-md-9">
                                    <a
                                        class="btn btn- btn-outline-secondary"
                                        v-for="(
                                            value, j
                                        ) in option.optionValues"
                                        :key="j"
                                        :class="{
                                            'btn-outline-danger':
                                                selected.findIndex(
                                                    (x) =>
                                                        x.optionValueId ==
                                                        value.id
                                                ) >= 0,
                                        }"
                                        @click="selectOption(value)"
                                    >
                                        {{ value.label }}
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <table class="table">
                        <thead class="table-light">
                            <tr>
                                <th>STT</th>
                                <th>Thuộc tính</th>
                                <th>Giá bán</th>
                                <th>Số lượng</th>
                                <th>Mã hàng</th>
                            </tr>
                        </thead>
                        <tbody class="table-border-bottom-0">
                            <tr
                                v-for="(item, index) in product.skus"
                                :key="index"
                            >
                                <td>
                                    <strong>{{ index + 1 }}</strong>
                                </td>
                                <td class="max-w-[200px]">
                                    <strong
                                        class="list-variants"
                                        v-for="(value, i) in item.variants"
                                        :key="i"
                                        >{{ value.optionValue.label }}</strong
                                    >
                                </td>
                                <td
                                    class="w-[180px] input-group input-group-merge"
                                >
                                    {{ displayPrice(item.price) + " đ" }}
                                </td>
                                <td class="w-[150px]">
                                    {{ item.stock }}
                                </td>

                                <td>
                                    {{ item.barCode }}
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
import { reactive, ref, onBeforeMount, computed, watch } from "vue";
import { useProductStore } from "@/stores/product";
import { errorMessage } from "@/helpers/toast";
import { isNumber } from "@/helpers/helpers";
import { displayPrice } from "@/services/utils";
const props = defineProps({
    id: {
        type: [Number, String as () => string | null],
        required: false,
        default: null,
    },
});

const emits = defineEmits(['close'])

const productStore = useProductStore();

const selected = ref<any>([]);
const skuSelect = ref<any>(null);

const selectOption = (value: any) => {
    skuSelect.value = null;
    const item = {
        optionId: value.optionId,
        optionValueId: value.id,
    };
    //check item selected
    const index = selected.value.findIndex(
        (x: any) => x.optionId == value.optionId
    );
    let oldValue = null;
    if (index >= 0) {
        oldValue = selected.value[index].optionValueId;
        selected.value.splice(index, 1);
    }
    if (oldValue == value.id) {
        return;
    }

    selected.value.push(item);

    product.value.skus.every((sku: any) => {
        const check = onlyInLeft(sku.variants, selected.value, isSameUser);
        console.log(check);

        if (check.length == 0) {
            skuSelect.value = sku;
            console.log(skuSelect.value);
            return false;
        }
        return true;
    });
};

const isSameUser = (a: any, b: any) =>
    a.optionId === b.optionId && a.optionValueId === b.optionValueId;

// Get items that only occur in the left array,
// using the compareFunction to determine equality.
const onlyInLeft = (left: any, right: any, compareFunction: any) =>
    left.filter(
        (leftValue: any) =>
            !right.some((rightValue: any) =>
                compareFunction(leftValue, rightValue)
            )
    );

const product = computed(() => productStore.$state.entry);

onBeforeMount(async () => {
    await productStore.show(props.id);
});
</script>
<style lang="scss" scoped></style>
