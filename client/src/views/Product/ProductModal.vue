<template lang="">
    <modal
        :title="!props.id ? 'Thêm mới sản phẩm' : 'Cập nhật sản phẩm'"
        modal-size="modal-xl"
        @close="
            () => {
                emits('close');
            }
        "
    >
        <template #body>
            <div class="col-sm-12 h-[70vh]">
                <div class="row">
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="name" class="form-label required"
                                >Tên sản phẩm</label
                            >
                        </div>
                        <div class="input-group input-group-merge">
                            <input
                                type="text"
                                class="form-control"
                                id="name"
                                v-model="product.name"
                            />
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>

                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="barCode" class="form-label required"
                                >Barcode</label
                            >
                        </div>
                        <div class="input-group input-group-merge">
                            <input
                                type="barCode"
                                class="form-control"
                                id="barCode"
                                v-model="product.barCode"
                            />
                        </div>
                        <Feedback :errors="errors?.barCode" />
                    </div>
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="unit" class="form-label">Đơn vị</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input
                                type="text"
                                class="form-control"
                                id="unit"
                                v-model="product.unit"
                            />
                        </div>
                        <Feedback :errors="errors?.unit" />
                    </div>

                    <div class="col-sm-3 mb-3">
                        <div class="col-sm-12">
                            <label for="price" class="form-label required"
                                >Giá bán</label
                            >
                        </div>
                        <div class="input-group input-group-merge">
                            <input
                                type="text"
                                id="price"
                                class="form-control"
                                v-model="product.price"
                                @keypress="isNumber"
                                :disabled="product.options.length > 0"
                            />
                        </div>
                        <Feedback :errors="errors?.price" />
                    </div>
                    <div class="col-sm-3 mb-3">
                        <div class="col-sm-12">
                            <label for="price" class="form-label required"
                                >Số lượng</label
                            >
                        </div>
                        <div class="input-group input-group-merge">
                            <input
                                type="text"
                                id="price"
                                class="form-control"
                                v-model="product.stock"
                                @keypress="isNumber"
                                :disabled="product.options.length > 0"
                            />
                        </div>
                        <Feedback :errors="errors?.stock" />
                    </div>
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="category" class="form-label"
                                >Loại sản phẩm</label
                            >
                        </div>
                        <!-- <select
                            class="form-select"
                            v-model="product.categoryId"
                            id="category"
                        >
                            <option selected :value="0">-- Vui lòng Chọn --</option>
                            <option
                                v-for="(category, index) in categories"
                                :key="index"
                                :value="category.id"
                            >
                                {{ category.name }}
                            </option>
                        </select> -->
                        <select-search
                            placeholder="-- Vui lòng Chọn --"
                            :listData="categories"
                            display="name"
                            keyValue="id"
                            v-model="product.categoryId"
                        ></select-search>
                        <Feedback :errors="errors?.categoryId" />
                    </div>
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="supplier" class="form-label"
                                >Nhà cung cấp</label
                            >
                        </div>
                        <select-search
                            placeholder="-- Vui lòng Chọn --"
                            :listData="suppliers"
                            display="name"
                            keyValue="id"
                            v-model="product.supplierId"
                        ></select-search>
                        <Feedback :errors="errors?.supplierId" />
                    </div>
                    <div class="col-sm-12 mb-3">
                        <label
                            for="exampleFormControlTextarea1"
                            class="form-label"
                            >Mô tả sản phẩm</label
                        >
                        <textarea
                            class="form-control"
                            v-model="product.description"
                            rows="3"
                        ></textarea>
                    </div>

                    <!-- Thuộc tính sản phẩm -->
                    <div class="col-sm-12 mb-3">
                        <div class="col-sm-12 mb-2">
                            <label for="note" class="form-label me-3"
                                >Thuộc tính sản phẩm</label
                            >
                            <button
                                type="button"
                                class="btn btn-sm btn-primary"
                                @click="toggleAddOption"
                            >
                                Thêm thuộc tính
                            </button>
                        </div>
                        <div
                            class="col-sm-12 border-1 border-gray-300 rounded-lg mb-2 py-1 ps-2 d-flex"
                            v-for="(option, i) in product.options"
                            :key="i"
                        >
                            <div class="d-flex col-6">
                                <div class="col-sm-6 mb-3 me-2">
                                    <div class="col-sm-12">
                                        <label
                                            :for="'option-name-' + i"
                                            class="form-label required"
                                            >Thuộc tính</label
                                        >
                                    </div>
                                    <div class="col-sm-12">
                                        <input
                                            type="text"
                                            class="form-control"
                                            :id="'option-name-' + i"
                                            v-model="option.name"
                                            :class="{
                                                'is-invalid': optionErrors.find(
                                                    (x) => x == 'option-' + i
                                                ),
                                            }"
                                        />
                                    </div>
                                </div>
                                <div class="col-sm-4 mb-3">
                                    <div class="col-sm-12">
                                        <label
                                            :for="'option-visual-' + i"
                                            class="form-label"
                                            >Loại</label
                                        >
                                    </div>
                                    <select
                                        class="form-select"
                                        v-model="option.visual"
                                        :id="'option-visual-' + i"
                                        @change="changeOptionType(i)"
                                    >
                                        <option selected :value="0">
                                            Text
                                        </option>
                                        <option selected :value="1">
                                            Color
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row col-6">
                                <div class="col-sm-6">
                                    <label class="form-label required"
                                        >Giá trị</label
                                    >
                                </div>
                                <div class="col-sm-6">
                                    <label class="form-label">Nhãn</label>
                                </div>
                                <div
                                    class="d-flex col-sm-12 mb-1"
                                    v-for="(value, j) in option.optionValues"
                                    :key="j"
                                >
                                    <div class="d-flex col-10 me-2 gap-2">
                                        <input
                                            type="text"
                                            class="form-control w-50"
                                            v-model="value.value"
                                            v-if="option.visual == 0"
                                            :id="i + '' + j"
                                            :class="{
                                                'is-invalid': optionErrors.find(
                                                    (x) => x == `value-${i}${j}`
                                                ),
                                            }"
                                            aria-describedby="validationServer03Feedback"
                                        />
                                        <div
                                            v-else
                                            class="d-flex align-items-center"
                                        >
                                            <input
                                                type="text"
                                                class="form-control me-2"
                                                v-model="value.value"
                                                disabled
                                            />
                                            <input
                                                class="rounded box-content p-1"
                                                type="color"
                                                v-model="value.value"
                                            />
                                        </div>
                                        <input
                                            class="form-control w-50"
                                            type="text"
                                            v-model="value.label"
                                        />
                                    </div>
                                    <div class="col-1">
                                        <button
                                            type="button"
                                            class="btn btn-icon btm-sm btn-outline-secondary"
                                            @click="
                                                toggleDeleteOptionValue(i, j)
                                            "
                                        >
                                            <i class="bx bx-trash"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="col-9 me-2 text-center mt-1">
                                    <button
                                        type="button"
                                        class="btn btn-sm btn-outline-primary"
                                        @click="toggleAddOptionValue(i)"
                                    >
                                        <i class="bx bx-plus-circle"></i> &nbsp;
                                        Thêm mới ({{
                                            option.optionValues.length
                                        }}/ {{ maxOptionValue }})
                                    </button>
                                </div>
                            </div>
                            <button
                                type="button"
                                class="btn-close text-white"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                                @click="toggleDeleteOption(i)"
                            ></button>
                        </div>
                    </div>
                    <!-- Tạo biến thể -->
                    <div class="col-sm-12">
                        <button
                            class="btn btn-secondary"
                            @click="generateSKUs()"
                        >
                            Tạo
                        </button>

                        <div
                            class="table-responsive text-nowrap table-scroll h-[70vh]"
                            v-if="product.skus.length > 0"
                        >
                            <h4 class="card-header">
                                Danh sách biến thể của sản phẩm
                            </h4>

                            <div
                                class="d-flex align-items-center col-sm-12 mb-3 gap-3"
                            >
                                <div class="col-sm-2">
                                    <label for="password" class="form-label m-0"
                                        >Thiết lập hàng loạt</label
                                    >
                                </div>
                                <div class="col-sm-2">
                                    <div class="input-group input-group-merge">
                                        <span class="input-group-text">đ</span>
                                        <input
                                            type="text"
                                            class="form-control"
                                            id="name"
                                            placeholder="Giá bán"
                                            @keypress="isNumber($event)"
                                            v-model="fillAll.price"
                                        />
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <input
                                        type="text"
                                        class="form-control"
                                        :class="{
                                            'is-invalid': optionErrors.find(
                                                (x) => x == 'option-' + i
                                            ),
                                        }"
                                        placeholder="Số lượng"
                                        v-model="fillAll.stock"
                                    />
                                </div>
                                <div class="col-sm-3">
                                    <input
                                        type="text"
                                        class="form-control"
                                        :class="{
                                            'is-invalid': optionErrors.find(
                                                (x) => x == 'option-' + i
                                            ),
                                        }"
                                        v-model="fillAll.barCode"
                                        placeholder="Mã hàng hoá"
                                    />
                                </div>
                                <div class="col-sm-2">
                                    <button
                                        class="btn btn-info"
                                        @click="toggleFillAll()"
                                    >
                                        Thiết lập
                                    </button>
                                </div>
                            </div>
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
                                                v-for="(
                                                    value, i
                                                ) in item.variants"
                                                :key="i"
                                                >{{ value.label }}</strong
                                            >
                                        </td>
                                        <td
                                            class="w-[180px] input-group input-group-merge"
                                        >
                                            <span class="input-group-text"
                                                >đ</span
                                            >
                                            <input
                                                type="text"
                                                class="form-control"
                                                id="name"
                                                v-model="item.price"
                                                @keypress="isNumber($event)"
                                            />
                                        </td>
                                        <td class="w-[150px]">
                                            <input
                                                type="text"
                                                class="form-control"
                                                id="name"
                                                v-model="item.stock"
                                                @keypress="isNumber($event)"
                                            />
                                        </td>

                                        <td class="text-center">
                                            <input
                                                type="text"
                                                class="form-control"
                                                id="name"
                                                v-model="item.barCode"
                                                required
                                            />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </template>
        <template #footer>
            <button class="btn btn-success" :disabled="editing" @click="save()">Lưu lại</button>
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
import { errorMessage, successMessage } from "@/helpers/toast";
import { isNumber } from "@/helpers/helpers";
import { v4 as uuidv4 } from "uuid";
const props = defineProps({
    id: {
        type: [Number, String as () => string | null],
        required: false,
        default: null,
    },
});

const emits = defineEmits(['close'])

const productStore = useProductStore();

const optionErrors = ref<Array<String>>([]);

const defaultColor = ref("#ffffff");

const editing = ref(true)

const fillAll = reactive({
    price: 0,
    stock: 0,
    barCode: "",
});

const newProduct = reactive({
    id: null,
    name: "",
    price: 0,
    stock: 0,
    unit: "",
    description: "",
    alias: "",
    categoryId: null,
    supplierId: null,
    options: [
        // {
        //     productId: null,
        //     code: uuidv4(),
        //     name: "",
        //     visual: 0,
        //     order: 0,
        //     optionValues: [
        //         {
        //             code: uuidv4(),
        //             value: "",
        //             label: "",
        //         },
        //     ],
        // },
    ],
    skus: [],
});

const product = computed( () => productStore.$state.entry ?? newProduct)

const maxOptionValue = ref(10);

const errors = ref<any>(null);

const categories = computed(() => productStore.$state.categories.data);
const suppliers = computed(() => productStore.$state.suppliers.data);

const toggleAddOption = () => {
    const option = {
        code: uuidv4(),
        productId: null,
        name: "",
        visual: 0,
        order: 0,
        optionValues: [
            {
                code: uuidv4(),
                value: "",
                label: "",
            },
        ],
    };
    product.value.options.push(option);
    editing.value = true
};

const changeOptionType = (index: any) => {
    product.value.options[index].optionValues = [
        {
            code: uuidv4(),
            value: product.value.options[index].visual == 1 ? defaultColor.value : "",
            label: "",
        },
    ];
};

const toggleAddOptionValue = (optionIndex: any) => {
    if (
        product.value.options[optionIndex].optionValues.length >= maxOptionValue.value
    ) {
        return;
    }
    const value = {
        code: uuidv4(),
        value:
            product.value.options[optionIndex].visual == 1 ? defaultColor.value : "",
        label: "",
    };
    product.value.options[optionIndex].optionValues.push(value);
    editing.value = true
};

const toggleDeleteOption = (index: any) => {
    product.value.options.splice(index, 1);
    editing.value = true
};
const toggleDeleteOptionValue = (i: any, j: any) => {
    product.value.options[i].optionValues.splice(j, 1);
    editing.value = true
};

const toggleFillAll = () => {
    product.value.skus.map((sku: any) => {
        sku.price = fillAll.price;
        sku.stock = fillAll.stock;
        sku.barCode = fillAll.barCode;
        return sku;
    });
};

const generateSKUs = () => {
    if (product.value.options.length < 1) return;
    product.value.options.sort(
        (a:any, b:any) => a.optionValues.length - b.optionValues.length
    );

    const arr = product.value.options.map((option:any) =>
        option.optionValues.map((x: any) => x.code)
    );
    optionErrors.value = [];
    const arrFlat = product.value.options
        .map((option:any, i:number) => {
            if (option.name == "") {
                optionErrors.value.push("option-" + i);
            }
            return option.optionValues.map((value:any, j:number) => {
                if (value.value == "") {
                    optionErrors.value.push(`value-${i}${j}`);
                }
                if (value.label == "") {
                    value.label = value.value;
                }
                return value;
            });
        })
        .flat();

    if (optionErrors.value.length > 0) {
        errorMessage("Không được để trống các trường thông tin!");
        return;
    }

    product.value.skus = generateCombinations(arr).map((item: any) => {

        if (typeof item == "string") {
            item = [arrFlat.find((x: any) => x.code == item)];
        } else {
            item = item.map((i: any) => arrFlat.find((x: any) => x.code == i));
        }

        return {
            id: null,
            productId: null,
            barCode: "",
            price: 0,
            name: "",
            stock: 0,
            variants: item,
        };
    });
    editing.value = false
    // console.log(rs);
};

const generateCombinations = (arrays: any) => {
    if (arrays.length === 0) {
        return [];
    }

    const [first, ...rest] = arrays;

    const combinations = first.map((item: Array<any>) => {
        if (rest.length === 0) {
            return [item];
        } else {
            return generateCombinations(rest).map((subCombination: any) => {
                console.log();
                if (typeof subCombination == "string") {
                    return [item, subCombination];
                }

                return [item, ...subCombination];
            });
        }
    });

    return combinations.flat();
};

const save = () => {
    if(editing){
        return
    }
    if (product.value.id == null) {
        productStore.create(product).then( res => {
            console.log(res);
            successMessage(res.data?.message ?? 'Thêm mới thành công!');
            emits('close', true)
        });
    }
};

onBeforeMount(async () => {
    await productStore.getListCategory({});
    await productStore.getListSupplier({});
    if(props.id){
        productStore.show(props.id)
    }else{
        productStore.$state.entry = null
    }
});
</script>
<style lang="scss" scoped>
</style>
