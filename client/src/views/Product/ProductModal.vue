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
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="name" class="form-label required">Tên sản phẩm</label>
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
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="barCode" class="form-label required"
                >Mã sản phẩm</label
              >
            </div>
            <div class="input-group input-group-merge">
              <input
                type="barCode"
                class="form-control"
                id="barCode"
                v-model="product.code"
              />
              <span
                id="basic-icon-default-password"
                @click="generateCode()"
                class="input-group-text cursor-pointer hover:bg-gray-200"
                ><i class="fa-solid fa-arrows-rotate"></i
              ></span>
            </div>
            <Feedback :errors="errors?.Code" />
          </div>

          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="barCode" class="form-label required">Barcode</label>
            </div>
            <div class="input-group input-group-merge">
              <input
                type="barCode"
                class="form-control"
                id="barCode"
                v-model="product.barcode"
              />
            </div>
            <Feedback :errors="errors?.Barcode" />
          </div>
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="unit" class="form-label">Đơn vị bán hàng</label>
            </div>
            <div class="input-group input-group-merge">
              <input
                type="text"
                class="form-control"
                id="unit"
                v-model="product.unitBuy"
              />
            </div>
            <Feedback :errors="errors?.UnitBuy" />
          </div>
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="unit" class="form-label">Đơn vị mua hàng</label>
            </div>
            <div class="input-group input-group-merge">
              <input
                type="text"
                class="form-control"
                id="unit"
                v-model="product.unitSell"
              />
            </div>
            <Feedback :errors="errors?.UnitSell" />
          </div>

          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="price" class="form-label required"
                >Giá bán sản phẩm</label
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
            <Feedback :errors="errors?.Price" />
          </div>
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="price" class="form-label required"
                >Số lượng cảnh báo</label
              >
            </div>
            <div class="input-group input-group-merge">
              <input
                type="text"
                id="price"
                class="form-control"
                v-model="product.numberWarning"
                @keypress="isNumber"
              />
            </div>
            <Feedback :errors="errors?.NumberWarning" />
          </div>
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="category" class="form-label">Loại sản phẩm</label>
            </div>
            <div class="col-sm-12 row">
              <div class="col-sm-11">
                <select-search
                  placeholder="-- Vui lòng Chọn --"
                  :listData="categories"
                  display="name"
                  keyValue="id"
                  v-model="product.categoryId"
                ></select-search>
              </div>
              <div class="col-sm-1 d-flex p-0">
                <span
                  id="basic-icon-default-password"
                  @click="showCategoryModal = true"
                  class="input-group-text cursor-pointer hover:bg-gray-200"
                  ><i class="fa-solid fa-plus"></i
                ></span>
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
                <select-search
                  placeholder="-- Vui lòng Chọn --"
                  :listData="brands"
                  display="name"
                  keyValue="id"
                  v-model="product.brandId"
                ></select-search>
              </div>
              <div class="col-sm-1 d-flex p-0">
                <span
                  id="basic-icon-default-password"
                  @click="showBrandModal = true"
                  class="input-group-text cursor-pointer hover:bg-gray-200"
                  ><i class="fa-solid fa-plus"></i
                ></span>
              </div>
            </div>

            <Feedback :errors="errors?.BrandId" />
          </div>
          <div class="col-sm-4 mb-3">
            <div class="col-sm-12">
              <label for="price" class="form-label required"
                >Thuế sản phẩm</label
              >
            </div>
            <div class="col-sm-12 row">
              <div class="col-sm-11">
                <select-search
                  :firstSelected="true"
                  :listData="taxes"
                  display="name"
                  keyValue="id"
                  v-model="product.taxId"
                ></select-search>
              </div>
              <div class="col-sm-1 d-flex p-0">
                <span
                  id="basic-icon-default-password"
                  @click="showTaxModal = true"
                  class="input-group-text cursor-pointer hover:bg-gray-200"
                  ><i class="fa-solid fa-plus"></i
                ></span>
              </div>
            </div>

            <Feedback :errors="errors?.TaxId" />
          </div>
          <div class="col-sm-8 row">
            <div class="col-sm-4 mb-3">
              <div class="col-sm-12">
                <label for="" class="form-label"></label>
              </div>
              <div class="col-sm-12 mt-3">
                <input
                  class="form-check-input me-2"
                  type="checkbox"
                  id="new_product"
                  v-model="product.isNew"
                />
                <label for="new_product" class="form-label">Sản phẩm mới</label>
              </div>
              <!-- <div class="col-sm-12">
                            <label for="new_product" class="form-label"
                                >Sản phẩm mới sẽ được hiển thị trong POS</label
                            >
                        </div> -->
            </div>
            <div class="col-sm-4 mb-3">
              <div class="col-sm-12">
                <label for="isFeatured" class="form-label"></label>
              </div>
              <div class="col-sm-12 mt-3">
                <input
                  class="form-check-input me-2"
                  type="checkbox"
                  id="isFeatured"
                  v-model="product.isFeatured"
                />
                <label for="isFeatured" class="form-label"
                  >Sản phẩm đặc sắc</label
                >
              </div>
              <!-- <div class="col-sm-12">
                            <label for="outstanding" class="form-label"
                                >Sản phẩm nổi bật sẽ được hiển thị trong
                                POS</label
                            >
                        </div> -->
            </div>
            <div class="col-sm-4 mb-3">
              <div class="col-sm-12">
                <label for="hasVariants" class="form-label"></label>
              </div>
              <div class="col-sm-12 mt-3">
                <input
                  class="form-check-input me-2"
                  type="checkbox"
                  id="hasVariants"
                  v-model="product.hasVariants"
                />
                <label for="hasVariants" class="form-label"
                  >Sản phẩm có biến thể</label
                >
              </div>
              <!-- <div class="col-sm-12">
                            <label for="outstanding" class="form-label"
                                >Sản phẩm nổi bật sẽ được hiển thị trong
                                POS</label
                            >
                        </div> -->
            </div>
          </div>
          <div class="col-sm-12 mb-3 mt-3">
            <label class="form-label mb-3 me-2"
              >Hình ảnh sản phẩm ({{ product.images?.length }} /
              {{ maxImage }})</label
            >
            <button
              class="btn btn-sm btn-outline-danger ms-2"
              @click="deleteImage(0, true)"
            >
              Xoá tất cả
            </button>
            <div
              class="d-flex flex-wrap gap-4 items-center border border-gray-300 p-3 rounded-md"
            >
              <div
                class="pr-image box-content"
                v-for="(item, index) in product.images"
                :key="index"
              >
                <img
                  :src="viewFile(item.path)"
                  class="w-[120px] h-[120px] object-contain rounded-2xl"
                />
                <div class="middle rounded-2xl">
                  <div class="text">
                    <button
                      type="button"
                      class="btn btn-sm btn-info"
                      @click="deleteImage(index)"
                    >
                      Xoá ảnh
                    </button>
                    <!-- <i
                                            class="fa-solid fa-trash fs-4 ms-3 hover:text-red-500 hover:scale-125"
                                            @click="deleteImage(index)"
                                        ></i> -->
                  </div>
                </div>
              </div>
              <div
                class="add-file w-[120px] h-[120px] border border-dashed border-red-600 text-red-600 p-1 box-content"
              >
                <label
                  for="image-files"
                  class="w-100 h-100 d-flex justify-center items-center cursor-pointer"
                >
                  <i class="fa-solid fa-plus fs-3"></i>
                </label>
                <input
                  type="file"
                  id="image-files"
                  @change="productImage"
                  accept="image/png, image/gif, image/jpeg"
                  hidden
                  multiple
                />
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
                  src="path"
                  :preImage="false"
                  display="fileName"
                  keyValue="code"
                  v-model="thumbId"
                ></select-search-user>
              </div>
            </div>
            <div class="col-sm-6">
              <img
                :src="viewFile(product.imageThumb)"
                class="w-[120px] h-[120px] object-contain rounded-2xl m-0 m-auto"
              />
            </div>
          </div>

          <div class="col-sm-12 h-[220px] mb-[80px]">
            <label for="exampleFormControlTextarea1" class="form-label"
              >Thêm mô tả sản phẩm</label
            >
            <QuillEditor
              theme="snow"
              v-model:content="product.description"
              contentType="html"
              placeholder="Nhập các thông tin chi tiết sản phẩm..."
            />
          </div>

          <div v-if="product.hasVariants">
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
                class="col-sm-12 border-1 border-gray-300 rounded-lg mb-2 py-2 px-2 d-flex"
                v-for="(option, i) in product.options"
                :key="i"
                :class="{ disabled: option.isDeleted }"
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
                      <label :for="'option-visual-' + i" class="form-label"
                        >Loại</label
                      >
                    </div>
                    <select
                      class="form-select"
                      v-model="option.visual"
                      :id="'option-visual-' + i"
                      @change="changeOptionType(i)"
                    >
                      <option selected :value="0">Text</option>
                      <option selected :value="1">Color</option>
                    </select>
                  </div>
                </div>
                <div class="row col-6">
                  <div class="col-sm-6">
                    <label class="form-label required">Giá trị</label>
                  </div>
                  <div class="col-sm-6">
                    <label class="form-label">Nhãn</label>
                  </div>
                  <div
                    class="d-flex col-sm-12 mb-1 py-2"
                    v-for="(value, j) in option.optionValues"
                    :key="j"
                    :class="{ disabled: value.isDeleted }"
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
                      <div v-else class="d-flex align-items-center">
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
                      <input
                        class="form-control"
                        type="file"
                        :id="`optionValueImage-${i}-${j}`"
                        @change="productImage($event, i, j)"
                        hidden
                      />
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
                      <button
                        v-if="!value.isDeleted"
                        type="button"
                        class="btn btn-icon btm-sm btn-outline-secondary"
                        @click="toggleDeleteOptionValue(i, j)"
                      >
                        <i class="bx bx-trash"></i>
                      </button>
                      <button
                        v-else
                        type="button"
                        class="btn btn-icon btm-sm btn-outline-danger"
                        data-bs-dismiss="modal"
                        @click="toggleRestoreOptionValue(i, j)"
                      >
                        <i class="fa-regular fa-window-restore"></i>
                      </button>
                    </div>
                  </div>
                  <div class="col-9 me-2 text-center mt-1">
                    <button
                      type="button"
                      class="btn btn-sm btn-outline-primary"
                      @click="toggleAddOptionValue(i)"
                    >
                      <i class="bx bx-plus-circle"></i>
                      &nbsp; Thêm mới ({{ option.optionValues.length }}/
                      {{ maxOptionValue }})
                    </button>
                  </div>
                </div>
                <div
                  v-if="
                    product.options.filter((x) => x.isDeleted == false).length >
                    1
                  "
                >
                  <button
                    v-if="!option.deleted"
                    type="button"
                    class="btn-close text-white p-1"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                    @click="toggleDeleteOption(i)"
                  ></button>
                  <button
                    v-else
                    type="button"
                    class="btn btn-sm w-[1.25rem] h-[1.25rem] p-1"
                    data-bs-dismiss="modal"
                    @click="toggleRestoreOption(i)"
                  >
                    <i class="fa-regular fa-window-restore"></i>
                  </button>
                </div>
              </div>
            </div>
            <!-- Tạo biến thể -->
            <div class="col-sm-12">
              <button
                class="btn btn-info"
                @click="generateSKUs()"
                v-if="product.options.length > 0"
              >
                Tạo biến thể
              </button>

              <div
                class="table-responsive text-nowrap table-scroll h-[70vh]"
                v-if="product.skus.length > 0"
              >
                <h4 class="card-header">Danh sách biến thể của sản phẩm</h4>

                <div class="d-flex align-items-center col-sm-12 mb-3 gap-3">
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
                        'is-invalid': optionErrors.find(
                          (x) => x == 'option-' + i
                        ),
                      }"
                      v-model="fillAll.barCode"
                      placeholder="Mã hàng hoá"
                    />
                  </div>
                  <div class="col-sm-2">
                    <button class="btn btn-info" @click="toggleFillAll()">
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
                      <!-- <th>Số lượng</th> -->
                      <th>Mã hàng</th>
                    </tr>
                  </thead>
                  <tbody class="table-border-bottom-0">
                    <tr v-for="(item, index) in product.skus" :key="index">
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
                      <td class="w-[180px] input-group input-group-merge">
                        <span class="input-group-text">đ</span>
                        <input
                          type="text"
                          class="form-control"
                          id="name"
                          v-model="item.price"
                          @keypress="isNumber($event)"
                        />
                      </td>
                      <!-- <td class="w-[150px]">
                                            <input
                                                type="text"
                                                class="form-control"
                                                id="name"
                                                v-model="item.stock"
                                                @keypress="isNumber($event)"
                                            />
                                        </td> -->

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
      </div>
    </template>
    <template #footer>
      <button class="btn btn-success" :disabled="editing" @click="save()">
        Lưu lại
      </button>
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
import {
  useProductStore,
  useCategoryStore,
  useBrandStore,
} from "@/stores/product";
import { useTaxStore } from "@/stores/tax";
import { errorMessage, successMessage, warningMessage } from "@/helpers/toast";
import { isNumber } from "@/helpers/helpers";
import { v4 as uuidv4 } from "uuid";
import { ResizeEvent } from "vue-advanced-cropper";
import { viewFile, randomPassword } from "@/helpers/helpers";
import TaxModal from "../Tax/TaxModal.vue";
import BrandModal from "../Brand/BrandModal.vue";
import CategoryModal from "../Category/CategoryModal.vue";

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
});

const product = computed(() => productStore.$state.entry ?? newProduct);

const maxOptionValue = ref(10);
const maxImage = ref(10);

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
  if (
    product.value.options[optionIndex].optionValues.length >=
    maxOptionValue.value
  ) {
    return;
  }
  const value = {
    id: null,
    optionId: product.value.options[optionIndex].id,
    code: uuidv4(),
    value:
      product.value.options[optionIndex].visual == 1 ? defaultColor.value : "",
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
    return sku;
  });
};

const generateSKUs = () => {
  if (product.value.options.length < 1) return;
  product.value.options.sort(
    (a: any, b: any) => a.optionValues.length - b.optionValues.length
  );

  const arrCodeOptionValue = product.value.options
    .filter((item: any) => !item.isDeleted)
    .map((option: any) =>
      option.optionValues
        .filter((item: any) => !item.isDeleted)
        .map((x: any) => x.code)
    );

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
  console.log(arrOptionValueFlat);

  if (optionErrors.value.length > 0) {
    errorMessage("Không được để trống các trường thông tin!");
    return;
  }

  product.value.skus = generateCombinations(arrCodeOptionValue).map(
    (item: any) => {
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
        id: uuidv4(),
        productId: product.value.id,
        barCode: "",
        price: 0,
        name: "",
        stock: 0,
        variants: item,
      };
    }
  );
  editing.value = false;
  product.value.newSkus = true;
  // console.log(rs);
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

const productImage = (event: any) => {
  // Reference to the DOM input element
  const { files } = event.target;
  for (let i = 0; i < files.length; i++) {
    const file = files[i];
    if (
      file &&
      !product.value.images.some((img: any) => img.fileName == file.name)
    ) {
      const reader = new FileReader();
      reader.onload = async (e: any) => {
        if (product.value.images.length >= maxImage.value) {
          warningMessage(`Tải lên tối đa ${maxImage.value} ảnh!`);
          return;
        }
        let dataImg = await resizeImage(e.target.result, file.type);
        product.value.images.push({
          id: 0,
          code: uuidv4(),
          path: dataImg,
          fileName: file.name,
          deleted: false,
          type: 0,
          driver: "",
          extension: file.type,
        });
      };
      reader.readAsDataURL(file);
    }
  }
  const fileInput = document.getElementById("image-files") as HTMLInputElement;
  fileInput.value = "";
};

// Hàm resize ảnh
const resizeImage = (
  imageSrc: any,
  type: any,
  targetWidth: any = null,
  targetHeight: any = null,
  maxSize = 500
) => {
  return new Promise((resolve, reject) => {
    const img = new Image();
    img.onload = () => {
      try {
        const canvas = document.createElement("canvas");
        const ctx: any = canvas.getContext("2d");

        if (targetWidth == null || targetHeight == null) {
          targetWidth = img.width;
          targetHeight = img.height;

          if (img.height > img.width) {
            targetHeight = maxSize;
            targetWidth = (img.width / img.height) * targetWidth;
          } else {
            targetWidth = maxSize;
            targetHeight = (img.height / img.width) * targetWidth;
          }
        }
        // Set kích thước của canvas
        canvas.width = targetWidth;
        canvas.height = targetHeight;

        // Vẽ ảnh lên canvas và resize
        ctx.drawImage(img, 0, 0, targetWidth, targetHeight);

        // Chuyển canvas thành base64
        resolve(canvas.toDataURL(type));
      } catch (err) {
        reject(err);
      }
    };
    img.onerror = (error) => {
      reject(error); // Xử lý lỗi nếu không load được ảnh
    };

    img.src = imageSrc;
  });
};

const deleteImage = (index: number, isDeleteAll = false) => {
  if (isDeleteAll) {
    product.value.images = [];
  } else {
    product.value.images.splice(index, 1);
  }
};

const generateCode = () => {
  product.value.code = randomPassword(17, false, true);
};

const save = () => {
  console.log(editing.value);

  if (editing.value) {
    return;
  }

  if (product.value.id == null) {
    productStore
      .create(product.value)
      .then((res) => {
        console.log(res);
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
        console.log(res);
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
  console.log(value);
  brandStore.getList({ notUse: false });
  product.value.brandId = value.id;
};
const closeCategoryModal = (value: any) => {
  showCategoryModal.value = false;
  console.log(value);
  categoryStore.getList({ notUse: false });
  product.value.categoryId = value.id;
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
    product.value.imageThumb = await resizeImage(
      img.path,
      img.extension,
      250,
      250
    );
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
      product.value.skus[index].price = isNaN(Number(value.price))
        ? 0
        : Number(value.price);
      product.value.skus[index].stock = isNaN(Number(value.stock))
        ? 0
        : Number(value.stock);
    });
  },
  { deep: true }
);

watch(
  () => product.value.hasVariants,
  (newVal, oldVal) => {
    if (!newVal) {
      product.value.options = [];
      product.value.skus = [];
    } else {
      if (product.value.options.length == 0) toggleAddOption();
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
