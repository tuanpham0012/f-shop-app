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
            <div class="col-sm-12 h-[70vh] px-4 py-2">
                <div class="row">
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="name" class="form-label required">Tên sản phẩm</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="text" class="form-control" id="name" v-model="product.name" />
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="barCode" class="form-label required">Mã sản phẩm</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="barCode" class="form-control" id="barCode" v-model="product.code" />
                            <span id="basic-icon-default-password" @click="generateCode()" class="input-group-text cursor-pointer hover:bg-gray-200"><i class="fa-solid fa-arrows-rotate"></i></span>
                        </div>
                        <Feedback :errors="errors?.Code" />
                    </div>

                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="barCode" class="form-label required">Barcode</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="barCode" class="form-control" id="barCode" v-model="product.barcode" />
                        </div>
                        <Feedback :errors="errors?.Barcode" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="unit" class="form-label">Đơn vị bán hàng</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="text" class="form-control" id="unit" v-model="product.unitBuy" />
                        </div>
                        <Feedback :errors="errors?.UnitBuy" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="unit" class="form-label">Đơn vị mua hàng</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="text" class="form-control" id="unit" v-model="product.unitSell" />
                        </div>
                        <Feedback :errors="errors?.UnitSell" />
                    </div>

                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="price" class="form-label required">Giá bán sản phẩm</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="text" id="price" class="form-control" v-model="product.price" @keypress="isNumber" :disabled="product.options.length > 0" />
                        </div>
                        <Feedback :errors="errors?.Price" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="price" class="form-label required">Số lượng cảnh báo</label>
                        </div>
                        <div class="input-group input-group-merge">
                            <input type="text" id="price" class="form-control" v-model="product.numberWarning" @keypress="isNumber" />
                        </div>
                        <Feedback :errors="errors?.NumberWarning" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="category" class="form-label">Loại sản phẩm</label>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-11">
                                <select-search placeholder="-- Vui lòng Chọn --" :listData="categories" display="name" keyValue="id" v-model="product.categoryId"></select-search>
                            </div>
                            <div class="col-sm-1 d-flex p-0">
                                <span id="basic-icon-default-password" @click="showCategoryModal = true" class="input-group-text cursor-pointer hover:bg-gray-200"><i class="fa-solid fa-plus"></i></span>
                            </div>
                        </div>
                        <Feedback :errors="errors?.CategoryId" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="brand" class="form-label">Nhãn hiệu</label>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-11">
                                <select-search placeholder="-- Vui lòng Chọn --" :listData="brands" display="name" keyValue="id" v-model="product.brandId"></select-search>
                            </div>
                            <div class="col-sm-1 d-flex p-0">
                                <span id="basic-icon-default-password" @click="showBrandModal = true" class="input-group-text cursor-pointer hover:bg-gray-200"><i class="fa-solid fa-plus"></i></span>
                            </div>
                        </div>

                        <Feedback :errors="errors?.BrandId" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="price" class="form-label required">Thuế sản phẩm</label>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-11">
                                <select-search :firstSelected="true" :listData="taxes" display="name" keyValue="id" v-model="product.taxId"></select-search>
                            </div>
                            <div class="col-sm-1 d-flex p-0">
                                <span id="basic-icon-default-password" @click="showTaxModal = true" class="input-group-text cursor-pointer hover:bg-gray-200"><i class="fa-solid fa-plus"></i></span>
                            </div>
                        </div>

                        <Feedback :errors="errors?.TaxId" />
                    </div>
                    <div class="col-sm-4 mb-3">
                        <div class="col-sm-12">
                            <label for="price" class="form-label required">Đơn vị quy đổi (Đơn vị mua <i class="fa-solid fa-right-long"></i> Đơn vị bán)</label>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <input type="text" id="price" class="form-control" :value="1" disabled @keypress="isNumber" />
                            </div>
                            <div class="col-sm-1 d-flex justify-center items-center">
                                <i class="fa-solid fa-right-long"></i>
                            </div>

                            <div class="col-sm-8">
                                <input type="text" class="form-control" v-model="product.conversionUnit" @keypress="isNumber" />
                            </div>
                        </div>

                        <Feedback :errors="errors?.TaxId" />
                    </div>
                    <div class="col-sm-12 row">
                        <div class="col-sm-3 mb-3">
                            <!-- <div class="col-sm-12">
                <label for="" class="form-label"></label>
              </div> -->
                            <div class="col-sm-12 mt-3">
                                <input class="form-check-input me-2" type="checkbox" id="new_product" v-model="product.isNew" />
                                <label for="new_product" class="form-label">Sản phẩm mới</label>
                            </div>
                            <!-- <div class="col-sm-12">
                            <label for="new_product" class="form-label"
                                >Sản phẩm mới sẽ được hiển thị trong POS</label
                            >
                        </div> -->
                        </div>
                        <div class="col-sm-3 mb-3">
                            <div class="col-sm-12 mt-3">
                                <input class="form-check-input me-2" type="checkbox" id="isFeatured" v-model="product.isFeatured" />
                                <label for="isFeatured" class="form-label">Sản phẩm đặc sắc</label>
                            </div>
                            <!-- <div class="col-sm-12">
                            <label for="outstanding" class="form-label"
                                >Sản phẩm nổi bật sẽ được hiển thị trong
                                POS</label
                            >
                        </div> -->
                        </div>
                        <div class="col-sm-3 mb-3">
                            <div class="col-sm-12 mt-3">
                                <input class="form-check-input me-2" type="checkbox" id="hasVariants" v-model="product.hasVariants" />
                                <label for="hasVariants" class="form-label">Sản phẩm có biến thể</label>
                            </div>
                        </div>
                        <div class="col-sm-3 mb-3">
                            <div class="col-sm-12 mt-3">
                                <input class="form-check-input me-2" type="checkbox" id="hasVariants" v-model="product.soldOut" />
                                <label for="hasVariants" class="form-label">Hết hàng</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 mb-3 mt-3">
                        <label class="form-label mb-3 me-2">Hình ảnh sản phẩm ({{ product.images?.length }} / {{ maxImage }})</label>
                        <button class="btn btn-sm btn-outline-danger ms-2" @click="deleteImage(0, true)">Xoá tất cả</button>
                        <div class="d-flex flex-wrap gap-4 items-center border border-gray-300 p-3 rounded-md">
                            <div class="pr-image box-content" v-for="(item, index) in product.images" :key="index">
                                <img :src="item.path" class="w-[120px] h-[120px] object-contain rounded-2xl" />
                                <div class="middle rounded-2xl">
                                    <div class="text">
                                        <button type="button" class="btn btn-sm btn-info" @click="deleteImage(index)">Xoá ảnh</button>
                                        <!-- <i
                                            class="fa-solid fa-trash fs-4 ms-3 hover:text-red-500 hover:scale-125"
                                            @click="deleteImage(index)"
                                        ></i> -->
                                    </div>
                                </div>
                            </div>
                            <div class="add-file w-[120px] h-[120px] border border-dashed border-red-600 text-red-600 p-1 box-content">
                                <label for="image-files" class="w-100 h-100 d-flex justify-center items-center cursor-pointer">
                                    <i class="fa-solid fa-plus fs-3"></i>
                                </label>
                                <input type="file" id="image-files" @change="productImage" accept="image/png, image/gif, image/jpeg" hidden multiple />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 row mb-3 mt-3">
                        <div class="col-sm-6">
                            <div class="col-sm-12">
                                <label class="form-label mb-3 me-2">Chọn ảnh Thumbnail</label>
                            </div>
                            <div class="col-sm-12">
                                <select-search-user
                                    :firstSelected="true"
                                    :listData="product.images"
                                    placeholder="-- Chọn ảnh --"
                                    src="path"
                                    :preImage="false"
                                    display="fileName"
                                    keyValue="code"
                                    v-model="thumbId"
                                    :search-box="false"
                                    :disabled="product.images.length < 1"
                                ></select-search-user>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <img :src="product.imageThumb" class="max-w-[100%] h-[140px] object-contain rounded-2xl m-0 m-auto" />
                        </div>
                    </div>
                    <div v-if="product.hasVariants">
                        <!-- Thuộc tính sản phẩm -->
                        <div class="col-sm-12 mb-3">
                            <div class="col-sm-12 mb-2">
                                <label for="note" class="form-label me-3">Thuộc tính sản phẩm</label>
                                <button type="button" class="btn btn-sm btn-primary" @click="toggleAddOption">Thêm thuộc tính</button>
                            </div>
                            <div class="col-sm-12 border-1 border-gray-300 rounded-lg mb-2 py-2 px-2 d-flex" v-for="(option, i) in product.options" :key="i" :class="{ disabled: option.isDeleted }">
                                <div class="d-flex col-4 me-4">
                                    <div class="col-sm-7 mb-3 me-2">
                                        <div class="col-sm-12">
                                            <label :for="'option-name-' + i" class="form-label required">Thuộc tính</label>
                                        </div>
                                        <div class="col-sm-12">
                                            <input
                                                type="text"
                                                class="form-control"
                                                :id="'option-name-' + i"
                                                v-model="option.name"
                                                @change="dataChanged(i, 'options')"
                                                :class="{
                                                    'is-invalid': optionErrors.find((x) => x == 'option-' + i),
                                                }"
                                            />
                                        </div>
                                    </div>
                                    <div class="col-sm-5 mb-3">
                                        <div class="col-sm-12">
                                            <label :for="'option-visual-' + i" class="form-label">Loại</label>
                                        </div>
                                        <select class="form-select" v-model="option.visual" :id="'option-visual-' + i" @change="changeOptionType(i)">
                                            <option selected :value="0">Text</option>
                                            <option selected :value="1">Color</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row col-8">
                                    <div class="col-sm-6">
                                        <label class="form-label required">Giá trị</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <label class="form-label">Nhãn</label>
                                    </div>
                                    <div class="d-flex col-sm-12 mb-1" v-for="(value, j) in option.optionValues" :key="j" :class="{ disabled: value.isDeleted }">
                                        <div class="d-flex col-10 me-2 gap-2">
                                            <input
                                                type="text"
                                                class="form-control w-50"
                                                v-model="value.value"
                                                v-if="option.visual == 0"
                                                :id="i + '' + j"
                                                :class="{
                                                    'is-invalid': optionErrors.find((x) => x == `value-${i}${j}`),
                                                }"
                                                @change="dataChanged(i, 'options', j, 'optionValues')"
                                            />
                                            <div v-else class="d-flex align-items-center">
                                                <input type="text" class="form-control me-2" v-model="value.value" disabled @change="dataChanged(i, 'options', j, 'optionValues')" />
                                                <input class="rounded box-content p-1" type="color" v-model="value.value" @change="dataChanged(i, 'options', j, 'optionValues')" />
                                            </div>
                                            <input class="form-control w-50" type="text" v-model="value.label" @change="dataChanged(i, 'options', j, 'optionValues')" />
                                            <input class="form-control" type="file" :id="`optionValueImage-${i}-${j}`" @change="productImage($event, i, j)" hidden />
                                            <!-- <div
                                            v-if="value.image"
                                            class="d-flex justify-center items-center w-[45px] h-[45px] object-contain cursor-pointer"
                                            @click="() => (value.image = '')"
                                        >
                                            <img :src="viewFile(value.image)" />
                                        </div>
                                        <label
                                            v-else
                                            :for="`optionValueImage-${i}-${j}`"
                                            ><div
                                                class="btn btn-icon btn-outline-danger"
                                            >
                                                <i
                                                    class="fa-solid fa-plus fs-6"
                                                ></i></div
                                        ></label> -->
                                        </div>
                                        <div class="col-1">
                                            <button v-if="!value.isDeleted" type="button" class="btn btn-icon btm-sm btn-outline-secondary" @click="toggleDeleteOptionValue(i, j)">
                                                <i class="bx bx-trash"></i>
                                            </button>
                                            <button v-else type="button" class="btn btn-icon btm-sm btn-outline-danger" data-bs-dismiss="modal" @click="toggleRestoreOptionValue(i, j)">
                                                <i class="fa-regular fa-window-restore"></i>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="col-9 me-2 text-center mt-1">
                                        <button type="button" class="btn btn-sm btn-outline-primary" @click="toggleAddOptionValue(i)">
                                            <i class="bx bx-plus-circle"></i>
                                            &nbsp; Thêm mới ({{ option.optionValues.length }}/ {{ maxOptionValue }})
                                        </button>
                                    </div>
                                </div>
                                <div v-if="product.options.filter((x) => x.isDeleted == false).length > 1">
                                    <button v-if="!option.deleted" type="button" class="btn-close text-white p-1" data-bs-dismiss="modal" aria-label="Close" @click="toggleDeleteOption(i)"></button>
                                    <button v-else type="button" class="btn btn-sm w-[1.25rem] h-[1.25rem] p-1" data-bs-dismiss="modal" @click="toggleRestoreOption(i)">
                                        <i class="fa-regular fa-window-restore"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- Tạo biến thể -->
                        <div class="col-sm-12">
                            <button class="btn btn-info" @click="generateSKUs()" v-if="product.options.length > 0">Tạo biến thể</button>

                            <div class="" v-if="product.skus.length > 0">
                                <h4 class="card-header">Danh sách biến thể của sản phẩm</h4>

                                <div class="d-flex align-items-center col-sm-12 mb-3 gap-3">
                                    <div class="col-sm-2">
                                        <label for="password" class="form-label m-0">Thiết lập hàng loạt</label>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="input-group input-group-merge">
                                            <span class="input-group-text">đ</span>
                                            <input type="text" class="form-control" id="name" placeholder="Giá bán" @keypress="isNumber($event)" v-model="fillAll.price" />
                                        </div>
                                    </div>
                                    <!-- <div class="col-sm-2">
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
                                        @keypress="isNumber($event)"
                                    />
                                </div> -->
                                    <div class="col-sm-3">
                                        <input
                                            type="text"
                                            class="form-control"
                                            :class="{
                                                'is-invalid': optionErrors.find((x) => x == 'option-' + i),
                                            }"
                                            v-model="fillAll.barCode"
                                            placeholder="Mã hàng hoá"
                                        />
                                    </div>
                                    <div class="col-sm-2">
                                        <button class="btn btn-info" @click="toggleFillAll()">Thiết lập</button>
                                    </div>
                                </div>
                                <div class="table-responsive text-nowrap table-scroll h-[70vh]">
                                    <table class="table table-responsive text-nowrap table-scroll h-[70vh]">
                                        <thead class="table-light">
                                            <tr>
                                                <th>STT</th>
                                                <th>Phân loại</th>
                                                <th>Giá bán</th>
                                                <th>Mã hàng</th>
                                                <th>Hình ảnh</th>
                                            </tr>
                                        </thead>
                                        <tbody class="table-border-bottom-0">
                                            <tr v-for="(item, index) in product.skus" :key="index">
                                                <td class="max-w-[50px]">
                                                    <strong>{{ index + 1 }}</strong>
                                                </td>
                                                <td class="min-w-[200px]">
                                                    <strong class="list-variants" v-for="(value, i) in item.variants" :key="i">{{ value.optionValue.label }} </strong> -- {{ item.name }}
                                                </td>
                                                <td class="min-w-[200px]">
                                                    <div class="input-group input-group-merge">
                                                        <span class="input-group-text">đ</span>
                                                        <input type="text" class="form-control" id="name" v-model="item.price" @keypress="isNumber($event)" @change="dataChanged(index, 'skus')" />
                                                    </div>
                                                </td>

                                                <td class="min-w-[150px]">
                                                    <input type="text" class="form-control" id="name" v-model="item.barCode" @change="dataChanged(index, 'skus')" required />
                                                </td>
                                                <td class="w-[100%]">
                                                    <div class="row col-12">
                                                        <div class="col-8">
                                                            <select-search-user
                                                                :firstSelected="true"
                                                                :listData="product.images"
                                                                placeholder="-- Chọn ảnh --"
                                                                src="path"
                                                                :preImage="false"
                                                                display="fileName"
                                                                keyValue="code"
                                                                v-model="item.imageCode"
                                                                @update:modelValue="changeSkuImage(index)"
                                                                :search-box="false"
                                                                :disabled="product.images.length < 1"
                                                            ></select-search-user>
                                                        </div>
                                                        <div class="col-4" v-if="item.imagePath">
                                                            <img :src="item.imagePath" class="h-[35px] w-[auto]" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
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
    <TaxModal v-if="showTaxModal" @close="closeTaxModal" />
    <BrandModal v-if="showBrandModal" @close="closeBrandModal" />
    <CategoryModal v-if="showCategoryModal" @close="closeCategoryModal" />
</template>
<script lang="ts" setup>
import { reactive, ref, onBeforeMount, computed, watch } from "vue";
import { useProductStore, useCategoryStore, useBrandStore } from "@/stores/product";
import { useTaxStore } from "@/stores/tax";
import { errorMessage, successMessage, warningMessage } from "@/helpers/toast";
import { v4 as uuidv4 } from "uuid";
import { ResizeEvent } from "vue-advanced-cropper";
import { randomPassword, resizeImage, isNumber } from "@/helpers/helpers";
import TaxModal from "../Tax/TaxModal.vue";
import BrandModal from "../Brand/BrandModal.vue";
import CategoryModal from "../Category/CategoryModal.vue";
// import _ from "lodash";

const props = defineProps({
    id: {
        type: [Number, String as () => string | null],
        required: false,
        default: null,
    },
});

const emits = defineEmits(["close"]);

const productStore = useProductStore();
const brandStore = useBrandStore();
const categoryStore = useCategoryStore();
const taxStore = useTaxStore();

const optionErrors = ref<Array<String>>([]);

const defaultColor = ref("#ffffff");

const editing = ref(false);

const showTaxModal = ref(false);
const showBrandModal = ref(false);
const showCategoryModal = ref(false);

const fillAll = reactive({
    price: 0,
    // stock: 0,
    barCode: "",
});

const thumbId = ref("");

const newProduct = reactive({
    id: null,
    code: "",
    name: "",
    barcode: "",
    price: 0,
    numberWarning: 0,
    unitSell: "",
    unitBuy: "",
    description: "",
    categoryId: null,
    brandId: null,
    taxId: 1,
    options: [],
    skus: [],
    images: [],
    hasVariants: false,
    isNew: false,
    isFeatured: false,
    imageThumb: "",
    conversionUnit: 1,
    soldOut: false,
});

const product = computed<any>(() => productStore.$state.entry ?? newProduct);

const maxOptionValue = ref(30);
const maxImage = ref(30);
const changeVariantCount = ref(0);

const errors = ref<any>(null);

const categories = computed(() => categoryStore.$state.entries.data);
const brands = computed(() => brandStore.$state.entries.data);
const taxes = computed(() => taxStore.$state.entries.data);

const toggleAddOption = () => {
    const option = {
        id: null,
        code: uuidv4(),
        productId: product.value.id,
        name: "",
        visual: 0,
        order: 0,
        isDeleted: false,
        optionValues: [
            {
                code: uuidv4(),
                value: "",
                label: "",
                image: "",
            },
        ],
    };
    product.value.options.push(option);

    editing.value = true;
};

const changeOptionType = (index: any) => {
    dataChanged(index, "options");
    product.value.options[index].optionValues = [
        {
            code: uuidv4(),
            value: product.value.options[index].visual == 1 ? defaultColor.value : "",
            label: "",
            image: "",
        },
    ];
};

const toggleAddOptionValue = (optionIndex: any) => {
    if (product.value.options[optionIndex].optionValues.length >= maxOptionValue.value) {
        return;
    }
    const value = {
        id: null,
        optionId: product.value.options[optionIndex].id,
        code: uuidv4(),
        value: product.value.options[optionIndex].visual == 1 ? defaultColor.value : "",
        label: "",
        image: "",
    };
    product.value.options[optionIndex].optionValues.push(value);
    editing.value = true;
};

const toggleDeleteOption = (index: any) => {
    if (product.value.options[index].id == null) {
        product.value.options.splice(index, 1);
    } else {
        product.value.options[index].isDeleted = true;
    }
    editing.value = true;
};

const toggleRestoreOption = (index: any) => {
    product.value.options[index].isDeleted = false;
    editing.value = true;
};

const toggleRestoreOptionValue = (i: any, j: any) => {
    product.value.options[i].optionValues[j].isDeleted = false;
    editing.value = true;
};

const toggleDeleteOptionValue = (i: any, j: any) => {
    if (product.value.options[i].optionValues[j].id == null) {
        product.value.options[i].optionValues.splice(j, 1);
    } else {
        product.value.options[i].optionValues[j].isDeleted = true;
    }

    editing.value = true;
};

const toggleFillAll = () => {
    product.value.skus.map((sku: any) => {
        sku.price = fillAll.price;
        // sku.stock = fillAll.stock;
        sku.barCode = fillAll.barCode;
        sku.isEdited = true;
        return sku;
    });
};

const generateSKUs = () => {
    if (product.value.options.length < 1) return;
    product.value.options.sort((a: any, b: any) => a.optionValues.length - b.optionValues.length);

    const arrCodeOptionValue = product.value.options.filter((item: any) => !item.isDeleted).map((option: any) => option.optionValues.filter((item: any) => !item.isDeleted).map((x: any) => x.code));

    optionErrors.value = [];
    const arrOptionValueFlat = product.value.options
        .filter((item: any) => !item.isDeleted)
        .map((option: any, i: number) => {
            if (option.name == "") {
                optionErrors.value.push("option-" + i);
            }
            return option.optionValues
                .filter((item: any) => !item.isDeleted)
                .map((value: any, j: number) => {
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

    product.value.skus = generateCombinations(arrCodeOptionValue).map((item: any) => {
        if (typeof item == "string") {
            item = [
                mapOptionValue(arrOptionValueFlat, item),
                // arrOptionValueFlat.find((x: any) => {
                //         return x.code == item;
                //     }),
            ];
        } else {
            item = item.map((i: any) => mapOptionValue(arrOptionValueFlat, i));
        }

        return {
            id: null,
            productId: product.value.id,
            barCode: "",
            price: 0,
            name: skuName(item),
            stock: 0,
            variants: item,
        };
    });
    editing.value = false;
    product.value.newSkus = true;
};

const skuName = (item: any) => {
    let result = "";
    item.forEach((element: any, index: number) => {
        result += element.optionValue.label;
        if (index < item.length - 1) result += " | ";
    });
    console.log(result);

    return result;
};

const mapOptionValue = (arrOptionValue: any, value: any) => {
    const item = arrOptionValue.find((x: any) => {
        return x.code == value;
    });
    if (item) {
        return {
            code: item.code,
            optionValue: item,
        };
    }
    return {
        code: value,
    };
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
                if (typeof subCombination == "string") {
                    return [item, subCombination];
                }

                return [item, ...subCombination];
            });
        }
    });

    return combinations.flat();
};

const productImage = (event: any) => {
    // Reference to the DOM input element
    const { files } = event.target;
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        if (file && !product.value.images.some((img: any) => img.fileName == file.name)) {
            const reader = new FileReader();
            reader.onload = async (e: any) => {
                if (product.value.images.length >= maxImage.value) {
                    warningMessage(`Tải lên tối đa ${maxImage.value} ảnh!`);
                    return;
                }
                let dataImg = await resizeImage(e.target.result, file.type, null, null, 1000);
                let imgId = uuidv4();
                product.value.images.push({
                    id: 0,
                    code: imgId,
                    path: dataImg,
                    fileName: file.name,
                    deleted: false,
                    type: 0,
                    driver: "",
                    extension: file.type,
                });

                if (i == 0 && (thumbId.value == "" || thumbId.value == null) && (product.value.imageThumb == "" || product.value.imageThumb == null)) {
                    thumbId.value = imgId;
                }
            };
            reader.readAsDataURL(file);
        }
    }
    const fileInput = document.getElementById("image-files") as HTMLInputElement;
    fileInput.value = "";
};

const deleteImage = (index: number, isDeleteAll = false) => {
    if (isDeleteAll) {
        product.value.images = [];
    } else {
        product.value.images.splice(index, 1);
    }
    if (product.value.images.length < 1) {
        thumbId.value = "";
    }
};

const generateCode = () => {
    product.value.code = randomPassword(17, false, true);
};

const save = () => {
    if (editing.value) {
        return;
    }

    if (product.value.id == null) {
        productStore
            .create(product.value)
            .then((res) => {
                successMessage(res.data?.message ?? "Thêm mới thành công!");
                emits("close", true);
            })
            .catch((err) => {
                errors.value = err.response.data.errors;
                errorMessage(err.response.data?.message ?? err.response.data?.title);
            });
    } else {
        productStore
            .update(product.value.id, product.value)
            .then((res) => {
                successMessage(res.data?.message ?? "Cập nhật thành công!");
                emits("close", true);
            })
            .catch((err) => {
                errors.value = err.response.data.errors;
                errorMessage(err.response.data?.message ?? err.response.data?.title);
            });
    }
};

const closeTaxModal = (value: any) => {
    showTaxModal.value = false;
    taxStore.getList({ notUse: false });
    product.value.taxId = value.id;
};
const closeBrandModal = (value: any) => {
    showBrandModal.value = false;
    brandStore.getList({ notUse: false });
    product.value.brandId = value.id;
};
const closeCategoryModal = (value: any) => {
    showCategoryModal.value = false;
    categoryStore.getList({ notUse: false });
    product.value.categoryId = value.id;
};

const changeSkuImage = async (index: number) => {
    const ImgCode = product.value.skus[index].imageCode;
    const img = product.value.images.find((x: any) => x.code == ImgCode);
    if (img) {
        product.value.skus[index].imagePath = await resizeImage(img.path, img.extension, null, null, 250);
    }
    dataChanged(index, "skus");
};

const dataChanged = (index: number, type: string, index2 = null, type2 = null) => {
    if (index2 != null && type2 != null) {
        product.value[type][index][type2][index2].isEdited = true;
    }
    product.value[type][index].isEdited = true;
};

watch(
    () => fillAll.price,
    (newVal, oldVal) => {
        fillAll.price = isNaN(Number(newVal)) ? 0 : Number(newVal);
    }
);

watch(
    () => product.value.price,
    (newVal, oldVal) => {
        product.value.price = isNaN(Number(newVal)) ? 0 : Number(newVal);
    }
);

watch(
    () => thumbId.value,
    async (newVal) => {
        const img = product.value.images.find((x: any) => x.code == newVal);
        if (img) {
            product.value.imageThumb = await resizeImage(img.path, img.extension, null, null, 250);
        } else {
            product.value.imageThumb = "";
        }
    }
);

watch(
    () => product.value.numberWarning,
    (newVal, oldVal) => {
        product.value.numberWarning = isNaN(Number(newVal)) ? 0 : Number(newVal);
    }
);

watch(
    () => product.value.skus,
    (newVal, oldVal) => {
        newVal.forEach((value: any, index: any) => {
            product.value.skus[index].price = isNaN(Number(value.price)) ? 0 : Number(value.price);
            product.value.skus[index].stock = isNaN(Number(value.stock)) ? 0 : Number(value.stock);
        });
    },
    { deep: true }
);

watch(
    () => product.value.hasVariants,
    (newVal, oldVal) => {
        if (product.value.id == null) {
            if (!newVal) {
                product.value.options = [];
                product.value.skus = [];
            } else {
                if (product.value.options.length == 0)
                    // && product.value.id == null
                    toggleAddOption();
            }
        } else {
            if (!newVal) {
                product.value.options.forEach((option: any, index: number) => {
                    product.value.options[index].isDeleted = true;
                });
                product.value.newSkus = true;
            } else {
                product.value.newSkus = false;
                product.value.options.forEach((option: any, index: number) => {
                    product.value.options[index].isDeleted = false;
                    if (product.value.options[index].id == null) product.value.newSkus = true;
                });
            }
        }
    }
);

onBeforeMount(async () => {
    categoryStore.getList({ notUse: false });
    brandStore.getList({ notUse: false });
    taxStore.getList({ notUse: false });
    if (props.id) {
        // await productStore.setEntryNull()
        await productStore.show(props.id);
    } else {
        productStore.$state.entry = null;
    }
});
</script>
<style lang="scss" scoped>
.middle {
    display: flex;
    align-items: center;
    justify-content: center;
    color: rgb(255, 255, 255);
    transition: 0.2s ease;
    opacity: 0;
    width: 100%;
    height: 100%;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    -ms-transform: translate(-50%, -50%);
    text-align: center;
    background-color: rgba(110, 110, 110, 0.521);
    cursor: pointer;
}
.pr-image {
    opacity: 1;
    display: block;
    height: auto;
    transition: 0.2s ease;
    backface-visibility: hidden;
    position: relative;
}

.pr-image:hover .image {
    opacity: 0.3;
}

.pr-image:hover .middle {
    opacity: 1;
}

.add-file:hover label {
    transform: scale(1.5);
    transition: 0.1s ease;
}
</style>
