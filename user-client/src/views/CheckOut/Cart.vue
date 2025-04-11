<template lang="">
  <div class="container-lg" ref="stopPoint">
    <nav aria-label="breadcrumb" class="hidden lg:block m-2 breadcrumb-nav">
      <ol class="breadcrumb">
        <li class="breadcrumb-item">
          <a href="index.html">Sản phẩm</a>
        </li>
        <li class="breadcrumb-item"><a href="#"></a></li>
        <li class="breadcrumb-item active" aria-current="page"></li>
      </ol>
    </nav>
    <div class="page-content">
      <div class="cart">
        <div class="grid grid-cols-12 gap-3 p-3 pt-0 bg-white">
          <div class="col-span-12 p-2">
            <div class="bg-white">
              <div class="hidden md:grid grid-cols-24 gap-4 py-3 border-b border-gray-300 font-semibold text-gray-500">
                <div class="col-span-1"><input type="checkbox" class="form-checkbox me-3" v-model="checkAll" /></div>
                <div class="col-span-12">Sản phẩm</div>
                <div class="col-span-3 text-right">Đơn giá</div>
                <div class="col-span-4 text-center">Số lượng</div>
                <div class="col-span-3 text-right">Thành tiền</div>
                <div class="col-span-1 text-center"></div>
              </div>
              <div class="grid grid-cols-24 gap-2 border-b py-3 border-gray-200" v-for="(item, index) in carts" :key="index">
                <div class="col-span-1 flex items-center"><input type="checkbox" :id="item.id" :value="item.id" v-model="selectedProductId" /></div>
                <div class="col-span-3 flex items-center justify-center">
                  <figure class="max-w-[80px] max-h-[80px] aspect-square m-0 me-3">
                    <img :src="item.sku.imagePath" alt="Product image" class="w-100 h-100 object-contain" />
                  </figure>
                </div>
                <div class="col-span-20 grid grid-cols-20 gap-4">
                  <div class="col-span-20 md:col-span-9 flex items-center">
                    <div class="flex justify-start w-full">
                      <div class="grid grid-cols-20 gap-1 pt-[4px] justify-between w-full">
                        <a class="col-span-20 md:col-span-8 text-neutral-600 text-base hover:text-[var(--bs-primary)]" href="#">{{ item.product.name }}</a>
                        <div class="col-span-20 md:col-span-4 text-sm text-gray-400 p-1 flex bg-gray-200 md:bg-white" v-if="item.sku.variants.length > 0">
                          <span>Phân Loại Hàng:</span>
                          <div class="variant">
                            <span v-for="(variant, index) in item.sku.variants" :key="index"> {{ variant.optionValue.label }}</span>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-span-3 flex items-center justify-end">{{ displayPrice(item.sku.price) + " đ" }}</div>
                  <div class="col-span-4 flex items-center">
                    <div class="cart-product-quantity">
                      <div class="input-group input-spinner">
                        <div class="input-group-prepend flex h-100 aspect-square">
                          <button class="btn btn-decrement btn-spinner border" type="button" :disabled="isPending" @click="updateCart(item, 2)">
                            <i class="icon-minus"></i>
                          </button>
                        </div>
                        <input type="text" style="text-align: center" class="form-control" required="" placeholder="" v-model="item.quantity" @change="updateCart(item, 0)" />
                        <div class="input-group-append flex h-100 aspect-square">
                          <button class="btn btn-increment btn-spinner border" type="button" :disabled="isPending" @click="updateCart(item, 1)">
                            <i class="icon-plus"></i>
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-span-3 flex items-center justify-end">{{ displayPrice(item.totalPrice) + " đ" }}</div>
                  <div class="col-span-1 flex items-center justify-end">
                    <button class="btn-remove" @click="cartStore.deleteCart(item.id)">
                      <i class="icon-close"></i>
                    </button>
                  </div>
                </div>
              </div>
            </div>

            <!-- <table class="table bg-white table-cart table-mobile">
              <thead>
                <tr>
                  <th>
                  </th>
                  <th>Sản phẩm</th>
                  <th class="text-center">Đơn giá</th>
                  <th class="text-center">Số lượng</th>
                  <th class="text-center">Thành tiền</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item, index) in carts" :key="index">
                  <td>
                    <input type="checkbox" :id="item.id" :value="item.id" v-model="selectedProductId" />
                  </td>
                  <td class="product-col py-2">
                    <div class="flex justify-start">
                      <figure class="flex items-center max-w-[80px] max-h-[80px] aspect-square m-0 me-3">
                        <img :src="item.sku.imagePath" alt="Product image" class="w-100 h-100 object-contain" />
                      </figure>
                      <div class="grid grid-cols-12 gap-1 pt-[4px] justify-between w-full">
                        <a class="col-span-8 text-neutral-600 text-sm hover:text-[var(--bs-primary)]" href="#">{{ item.product.name }}</a>
                        <div class="col-span-4 text-sm text-gray-400 p-1" v-if="item.sku.variants.length > 0">
                          <span>Phân Loại Hàng:</span>
                          <div class="variant">
                            <span v-for="(variant, index) in item.sku.variants" :key="index"> {{ variant.optionValue.label }}</span>
                          </div>
                        </div>
                      </div>
                    </div>
                  </td>
                  <td class="price-col text-right">
                    {{ displayPrice(item.sku.price) + " đ" }}
                  </td>
                  <td class="quantity-col">
                    <div class="cart-product-quantity">
                      <div class="input-group input-spinner">
                        <div class="input-group-prepend flex h-100 aspect-square">
                          <button class="btn btn-decrement btn-spinner border" type="button" :disabled="isPending" @click="updateCart(item, 2)">
                            <i class="icon-minus"></i>
                          </button>
                        </div>
                        <input type="text" style="text-align: center" class="form-control" required="" placeholder="" v-model="item.quantity" @change="updateCart(item, 0)" />
                        <div class="input-group-append flex h-100 aspect-square">
                          <button class="btn btn-increment btn-spinner border" type="button" :disabled="isPending" @click="updateCart(item, 1)">
                            <i class="icon-plus"></i>
                          </button>
                        </div>
                      </div>
                    </div>
                  </td>
                  <td class="total-col text-right">
                    {{ displayPrice(item.totalPrice) + " đ" }}
                  </td>
                  <td class="remove-col">
                    <button class="btn-remove" @click="cartStore.deleteCart(item.id)">
                      <i class="icon-close"></i>
                    </button>
                  </td>
                </tr>
              </tbody>
            </table> -->
          </div>
          <!-- End .col-lg-9 -->
          <aside class="col-span-12 lg:col-span-3" v-if="false">
            <div class="summary summary-cart">
              <p class="summary-title">Cart Total</p>
              <!-- End .summary-title -->
              <table class="table table-summary">
                <tbody>
                  <!-- End .summary-subtotal -->
                  <tr class="summary-shipping">
                    <td colspan="2">Đơn vị vận chuyển:</td>
                  </tr>
                  <tr class="summary-shipping-row">
                    <td colspan="2">
                      <select-search :listData="shippingUnits" display="name" keyValue="id" v-model="shippingUnitId" :firstSelected="true"></select-search>
                    </td>
                  </tr>
                  <tr class="summary-shipping">
                    <td colspan="2">Phương thức thanh toán:</td>
                  </tr>
                  <tr class="summary-shipping-row">
                    <td colspan="2">
                      <div class="radio-container" v-for="(item, index) in paymentMethods" :key="index">
                        <input type="radio" :id="`paymentMethods-${item.id}`" name="paymentMethod" :value="item.id" checked />
                        <label class="flex gap-2 items-center" :for="`paymentMethods-${item.id}`">
                          <span class="custom-radio"></span>
                          <span class="w-100">{{ item.name }}</span>
                        </label>
                      </div>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="text-center">
                      <div class="input-group">
                        <input type="text" class="form-control" required="" placeholder="Mã giảm giá" />
                        <div class="input-group-append">
                          <button class="btn btn-outline-primary-2 rounded-none" type="submit">
                            <i class="icon-long-arrow-right"></i>
                          </button>
                        </div>
                      </div>
                    </td>
                  </tr>
                  <tr class="summary-shipping">
                    <td colspan="2">Thông tin nhận hàng:</td>
                  </tr>
                  <tr class="summary-shipping">
                    <td colspan="2">+80000000000, số 12 Hà nội\ ?</td>
                  </tr>
                  <tr class="summary-subtotal">
                    <td>Giá sản phẩm:</td>
                    <td>
                      {{ displayPrice(totalPrice) + " đ" }}
                    </td>
                  </tr>
                  <tr class="summary-subtotal">
                    <td>Giá vận chuyển:</td>
                    <td>
                      {{ displayPrice(totalPrice) + " đ" }}
                    </td>
                  </tr>
                  <tr class="summary-total">
                    <td>Tổng tiền:</td>
                    <td>$160.00</td>
                  </tr>
                </tbody>
              </table>
              <div class="flex justify-center">
                <a href="checkout.html" class="btn btn-outline-primary-2 btn-order btn-block">Thanh toán</a>
              </div>
            </div>
            <a href="category.html" class="btn btn-outline-dark-2 btn-block mb-3"><span>CONTINUE SHOPPING</span><i class="icon-refresh"></i></a>
          </aside>
        </div>
      </div>
    </div>
  </div>
  <!-- <div id="stop-point"></div> -->
  <div class="checkout-summary my-2" ref="checkoutElm">
    <div class="container-lg">
      <div class="bg-white pb-1 shadow-[0_-15px_25px_-5px_rgba(0,0,0,0.1)]">
        <!-- Voucher Row -->
        <div class="flex justify-between p-3 voucher-section">
          <div class="left-content">
            <span class="icon-voucher" title="Shopee Voucher"></span>
            <span>Shopee Voucher</span>
          </div>
          <div class="right-content">
            <a href="#">Chọn hoặc nhập mã</a>
          </div>
        </div>

        <hr class="divider" />

        <!-- Shopee Xu Row -->
        <div class="flex justify-between p-3 shopee-xu-section">
          <div class="left-content">
            <span class="xu-checkbox-placeholder"></span>
            <!-- Placeholder for disabled checkbox -->
            <span class="icon-shopee-coin">S</span>
            <span>Shopee Xu</span>
            <span>Bạn chưa chọn sản phẩm</span>
            <span class="info-icon" title="Thông tin Shopee Xu">?</span>
          </div>
          <div class="right-content">
            <span>-₫0</span>
          </div>
        </div>

        <hr class="divider" />

        <!-- Action Row -->
        <div class="flex justify-between p-3 action-section">
          <div class="left-actions">
            <input type="checkbox" id="select-all-items" name="select-all-items" v-model="checkAll" />
            <label for="select-all-items">Chọn Tất Cả ({{ carts.length }})</label>
            <a @click="deleteMultiple()">Xóa sản phẩm đã chọn</a>
          </div>
          <div class="right-summary">
            <span class="total-label">Tổng cộng ({{ selectedProducts.length }} Sản phẩm):</span>
            <span class="total-price">{{ displayPrice(totalPrice) + " đ" }}</span>
            <button class="buy-button">Mua Hàng</button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="mb-10"></div>
</template>
<script setup lang="ts">
import { onBeforeMount, computed, ref, watch } from "vue";
import { useCartStore } from "@/stores/cart";
import { useCommonStore } from "@/stores/common";
import { displayPrice } from "@/services/utils";
const cartStore = useCartStore();
const commonStore = useCommonStore();
const selectedProducts = ref<any>([]);
const selectedProductId = ref<any>([]);
const checkAll = ref(false);
const totalPrice = ref(0);

const shippingUnitId = ref(1);

const checkoutElm = ref<any>(null);
const stopElm = ref<any>(0);
const stopPoint = ref<any>(null);

const isPending = computed(() => {
  return cartStore.$state.pending;
});

watch(
  () => selectedProductId.value,
  (newValue) => {
    selectedProducts.value = [];
    selectedProductId.value.forEach((value: any) => {
      const item = carts.value.find((x: any) => x.id == value);
      if (item) {
        selectedProducts.value.push(item);
      }
    });
  },
  { deep: true }
);

watch(
  () => checkAll.value,
  (newValue) => {
    if (newValue && selectedProductId.value.length <= carts.value.length) {
      carts.value.forEach((v: any) => {
        selectedProductId.value.push(v.id);
      });
    } else {
      selectedProductId.value = [];
    }
  }
);

watch(
  () => selectedProducts.value,
  () => {
    totalPrice.value = selectedProducts.value.reduce((sum: number, item: any) => {
      return sum + item.totalPrice;
    }, 0);
  },
  { deep: true }
);

const carts = computed(() => cartStore.$state.carts.data);
const paymentMethods = computed(() => commonStore.$state.paymentMethods.data);
const shippingUnits = computed(() => commonStore.$state.shippingUnits.data);

const updateCart = async (item: any, type: any) => {
  console.log(item);

  const data = {
    id: item.id,
    quantity: type == 0 ? item.quantity : 1,
    type: type,
  };
  await cartStore.updateCart(item.id, data);
};

watch(
  () => carts.value,
  () => {
    setTimeout(() => checkoutScroll(), 500);
  }
);

const deleteMultiple = () => {
  cartStore.deleteCarts({
    ids: selectedProductId.value,
  });
  selectedProductId.value = [];
};

const checkoutScroll = () => {
  console.log(stopPoint.value);

  function handleScroll() {
    if (checkoutElm.value == null || stopPoint.value == null) return;

    stopElm.value = stopPoint.value.offsetHeight + stopPoint.value.offsetTop + 165;

    const windowHeight = window.innerHeight;
    const scrollY = window.scrollY || document.documentElement.scrollTop;

    const viewportBottom = scrollY + windowHeight;

    if (viewportBottom < stopElm.value) {
      checkoutElm.value.classList.add("is-fixed-bottom");
    } else {
      checkoutElm.value.classList.remove("is-fixed-bottom");
    }
  }
  window.addEventListener("scroll", handleScroll);
  window.addEventListener("resize", handleScroll);

  handleScroll();
};
onBeforeMount(async () => {
  await cartStore.getList();
  await commonStore.getPaymentMethods();
  await commonStore.getShippingUnits();
  checkoutScroll();
});
</script>
<style lang="scss" scoped>
.table {
  td {
    padding-top: 1.25rem;
    padding-bottom: 1.25rem;
    // color: #686868;
  }
  th {
    color: #777777;
  }
}

.btn {
  --bs-btn-border-radius: 0;
}

.variant span:not(:last-child) {
  &::after {
    content: ", ";
  }
}

/* Container cho mỗi cặp input/label */
.radio-container {
  display: inline-block;
  position: relative;
  cursor: pointer;
  font-size: 1rem;
  -webkit-user-select: none;
  /* Chống chọn text trên label */
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
  text-align: left;
  margin-bottom: 0.6rem;
  width: 100%;
}

/* --- 1. Ẩn input radio gốc --- */
.radio-container input[type="radio"] {
  position: absolute;
  opacity: 0;
  /* Làm cho nó vô hình */
  cursor: pointer;
  height: 0;
  width: 0;
}

.custom-radio {
  display: inline-block;
  height: 20px;
  min-width: 20px;
  background-color: #eee;
  border: 1px solid #ccc;
  border-radius: 50%;
  transition: background-color 0.2s ease, border-color 0.2s ease;
}

.radio-container input[type="radio"]:checked ~ label .custom-radio {
  background-color: #fff;
  border: 1px solid var(--bs-primary);
}

.custom-radio::after {
  content: "";
  position: absolute;
  display: none;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.radio-container input[type="radio"]:checked ~ label .custom-radio::after {
  display: block;
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: var(--bs-primary);
}

.radio-container:hover input[type="radio"]:not(:checked) ~ label .custom-radio {
  background-color: #ddd;
  /* Đổi màu nền nhẹ khi hover (nếu chưa check) */
}

.radio-container input[type="radio"]:focus + label .custom-radio {
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.3);
  outline: none;
}

.radio-container input[type="radio"]:focus-visible + label .custom-radio {
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.3);
  outline: none;
}

.radio-container input[type="radio"]:disabled ~ label {
  color: #aaa;
  cursor: not-allowed;
}

.radio-container input[type="radio"]:disabled ~ label .custom-radio {
  background-color: #f0f0f0;
  /* Màu nền nhạt hơn */
  border-color: #ddd;
  cursor: not-allowed;
}

.radio-container input[type="radio"]:disabled ~ label .custom-radio::after {
  background-color: #ccc;
}

.is-fixed-bottom {
  position: fixed; /* Cố định vị trí */
  bottom: -10px; /* Cách đỉnh viewport 0px */
  z-index: 1000; /* Để nằm trên các phần tử khác */
}

.checkout-summary {
  border-radius: 3px; /* Bo góc nhẹ */
  box-sizing: border-box;
  width: 100%; /* Chiếm toàn bộ chiều rộng */
}

.divider {
  border: none;
  border-top: 1px dashed #e8e8e8; /* Đường kẻ ngang đứt nét */
  margin: 0 20px; /* Thu hẹp đường kẻ */
}
.voucher-section .left-content {
  display: flex;
  align-items: center;
  gap: 8px; /* Khoảng cách giữa icon và text */
}
.voucher-section .icon-voucher {
  display: inline-block;
  width: 18px;
  height: 14px;
  background-color: #ffefe8; /* Màu nền icon voucher */
  border: 1px solid #ee4d2d; /* Viền màu cam */
  vertical-align: middle;
  position: relative; /* Để tạo dấu răng cưa nếu muốn */
}

.voucher-section .right-content a {
  color: #05a; /* Màu xanh dương cho link */
  text-decoration: none;
}
.voucher-section .right-content a:hover {
  text-decoration: underline;
}

/* Shopee Xu Row */
.shopee-xu-section {
  color: #757575; /* Màu chữ xám */
}
.shopee-xu-section .left-content {
  display: flex;
  align-items: center;
  gap: 8px;
}
.shopee-xu-section .xu-checkbox-placeholder {
  display: inline-block;
  width: 16px;
  height: 16px;
  background-color: #f8f8f8; /* Màu nền giả checkbox */
  border: 1px solid #dcdcdc; /* Viền giả checkbox */
  vertical-align: middle;
}
.shopee-xu-section .icon-shopee-coin {
  display: inline-flex; /* Để căn giữa chữ S */
  align-items: center;
  justify-content: center;
  width: 16px;
  height: 16px;
  background-color: #f69113; /* Màu vàng cam của xu */
  color: white;
  border-radius: 50%;
  font-weight: bold;
  font-size: 10px;
  vertical-align: middle;
}
.shopee-xu-section .info-icon {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 14px;
  height: 14px;
  border: 1px solid #bbb;
  color: #bbb;
  border-radius: 50%;
  font-size: 10px;
  cursor: help; /* Con trỏ hình dấu hỏi */
  vertical-align: middle;
}

/* Action Row */
.action-section .left-actions {
  display: flex;
  align-items: center;
  gap: 15px; /* Khoảng cách các mục bên trái */
}
.action-section .left-actions label {
  margin-left: 5px; /* Khoảng cách giữa checkbox và text */
  cursor: pointer;
}
.action-section .left-actions a {
  color: #555;
  text-decoration: none;
  cursor: pointer;
}
.action-section .left-actions a:hover {
  color: var(--bs-primary);
}
.action-section .left-actions .save-liked {
  color: var(--bs-primary); /* Màu cam cho link Lưu */
}

.action-section .right-summary {
  display: flex;
  align-items: center;
  gap: 10px; /* Khoảng cách các mục bên phải */
}
.action-section .total-label {
  color: #222;
}
.action-section .total-price {
  color: var(--bs-primary); /* Màu cam/đỏ cho giá tiền */
  font-size: 20px; /* Cỡ chữ lớn hơn */
  font-weight: 500;
}
.action-section .buy-button {
  background-color: var(--bs-primary); /* Màu cam Shopee */
  color: white;
  border: none;
  padding: 10px 25px;
  border-radius: 2px;
  font-size: 14px;
  cursor: pointer;
  font-weight: 500;
  transition: transform 0.2s ease; /* Hiệu ứng hover */
}
.action-section .buy-button:hover {
  transform: translateY(-2px); /* Màu cam đậm hơn khi hover */
}

/* Căn chỉnh checkbox chuẩn hơn */
input[type="checkbox"] {
  vertical-align: middle;
  margin: 0; /* Reset margin */
}
</style>
