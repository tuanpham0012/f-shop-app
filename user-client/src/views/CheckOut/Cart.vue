<template lang="">
  <div class="container-md">
    <nav aria-label="breadcrumb" class="hidden lg:block breadcrumb-nav">
      <div class="container-md">
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="index.html">Sản phẩm</a></li>
          <li class="breadcrumb-item">
            <a href="#"></a>
          </li>
          <li class="breadcrumb-item active" aria-current="page"></li>
        </ol>
      </div>
      <!-- End .container -->
    </nav>
    <div class="page-content">
      <div class="cart">
        <div class="container">
          <div class="grid grid-cols-12 gap-3 p-3 bg-white">
            <div class="col-span-12 lg:col-span-9 p-2">
              <table class="table bg-white table-cart table-mobile">
                <thead>
                  <tr>
                    <th>
                      <input type="checkbox" id="rrreeedd" v-model="checkAll" />
                    </th>
                    <th>Sản phẩm</th>
                    <th class="text-center">Giá bán</th>
                    <th class="text-center">Số lượng</th>
                    <th class="text-center">Thành tiền</th>
                    <th></th>
                  </tr>
                </thead>

                <tbody>
                  <tr v-for="(item, index) in carts" :key="index">
                    <td>
                      <input
                        type="checkbox"
                        :id="item.id"
                        :value="item.id"
                        v-model="selectedProductId"
                      />
                    </td>
                    <td class="product-col py-2">
                      <div class="flex justify-start">
                        <figure
                          class="product-media max-w-[100px] max-h-[100px] aspect-square"
                        >
                          <img
                            :src="item.sku.imagePath"
                            alt="Product image"
                            class="w-100 h-100 object-contain"
                          />
                        </figure>
                        <div class="grid grid-cols-12">
                          <a
                            class="col-span-8 text-neutral-700 text-[1.1rem] hover:text-[var(--bs-primary)]"
                            href="#"
                            >{{ item.product.name }}</a
                          >
                          <div
                            class="col-span-4 text-sm text-gray-500 p-1"
                            v-if="item.sku.variants.length > 0"
                          >
                            <span>Phân Loại Hàng:</span>
                            <div class="variant">
                              <span
                                v-for="(variant, index) in item.sku.variants"
                                :key="index"
                              >
                                {{ variant.optionValue.label }}</span
                              >
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
                          <div
                            class="input-group-prepend flex h-100 aspect-square"
                          >
                            <button
                              class="btn btn-decrement btn-spinner"
                              type="button"
                            >
                              <i class="icon-minus"></i>
                            </button>
                          </div>
                          <input
                            type="text"
                            style="text-align: center"
                            class="form-control"
                            required=""
                            placeholder=""
                            v-model="item.quantity"
                          />
                          <div
                            class="input-group-append flex h-100 aspect-square"
                          >
                            <button
                              class="btn btn-increment btn-spinner"
                              type="button"
                            >
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
                      <button class="btn-remove">
                        <i class="icon-close"></i>
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
              <div class="cart-bottom">
                <div class="cart-discount">
                  <form action="#">
                    <div class="input-group">
                      <input
                        type="text"
                        class="form-control"
                        required=""
                        placeholder="coupon code"
                      />
                      <div class="input-group-append">
                        <button
                          class="btn btn-outline-primary-2 rounded-none"
                          type="submit"
                        >
                          <i class="icon-long-arrow-right"></i>
                        </button>
                      </div>
                    </div>
                  </form>
                </div>

                <a href="#" class="btn btn-outline-dark-2"
                  ><span>UPDATE CART</span><i class="icon-refresh"></i
                ></a>
              </div>
              <!-- End .cart-bottom -->
            </div>
            <!-- End .col-lg-9 -->
            <aside class="col-span-12 lg:col-span-3">
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
                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-cod"
                            name="paymentMethod"
                            value="cod"
                            checked
                          />
                          <label
                            class="flex gap-2 items-center"
                            for="payment-cod"
                          >
                            <span class="custom-radio"></span>
                            <span class="w-100"
                              >Thanh toán khi nhận hàng (COD)</span
                            >
                          </label>
                        </div>

                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-card"
                            name="paymentMethod"
                            value="card"
                          />
                          <label
                            class="flex gap-2 items-center"
                            for="payment-card"
                          >
                            <span class="custom-radio"></span>
                            <span class="">Thẻ tín dụng/Ghi nợ</span>
                          </label>
                        </div>
                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-momo"
                            name="paymentMethod"
                            value="momo"
                          />
                          <label
                            for="payment-momo"
                            class="flex gap-2 items-center"
                          >
                            <span class="custom-radio"></span>
                            Ví MoMo
                          </label>
                        </div>
                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-bank"
                            name="paymentMethod"
                            value="bank"
                            disabled
                          />
                          <label
                            class="flex gap-2 items-center"
                            for="payment-bank"
                          >
                            <span class="custom-radio"></span>
                            <span class="">Chuyển khoản (Tạm khóa)</span>
                          </label>
                        </div>
                      </td>
                    </tr>
                    <tr class="summary-shipping">
                      <td colspan="2">Phương thức thanh toán:</td>
                    </tr>

                    <tr class="summary-shipping-row">
                      <td colspan="2">
                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-cod"
                            name="paymentMethod"
                            value="cod"
                            checked
                          />
                          <label
                            class="flex gap-2 items-center"
                            for="payment-cod"
                          >
                            <span class="custom-radio"></span>
                            <span class="w-100"
                              >Thanh toán khi nhận hàng (COD)</span
                            >
                          </label>
                        </div>

                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-card"
                            name="paymentMethod"
                            value="card"
                          />
                          <label
                            class="flex gap-2 items-center"
                            for="payment-card"
                          >
                            <span class="custom-radio"></span>
                            <span class="">Thẻ tín dụng/Ghi nợ</span>
                          </label>
                        </div>
                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-momo"
                            name="paymentMethod"
                            value="momo"
                          />
                          <label
                            for="payment-momo"
                            class="flex gap-2 items-center"
                          >
                            <span class="custom-radio"></span>
                            Ví MoMo
                          </label>
                        </div>
                        <div class="radio-container">
                          <input
                            type="radio"
                            id="payment-bank"
                            name="paymentMethod"
                            value="bank"
                            disabled
                          />
                          <label
                            class="flex gap-2 items-center"
                            for="payment-bank"
                          >
                            <span class="custom-radio"></span>
                            <span class="">Chuyển khoản (Tạm khóa)</span>
                          </label>
                        </div>
                      </td>
                    </tr>

                    <tr class="summary-shipping">
                      <td colspan="2">Thông tin nhận hàng:</td>
                    </tr>
                    <tr class="summary-shipping">
                      <td colspan="2">+80000000000, số 12 Hà nội</td>
                    </tr>
                    <tr class="summary-subtotal">
                      <td>Giá sản phẩm:</td>
                      <td>{{ displayPrice(totalPrice) + " đ" }}</td>
                    </tr>
                    <tr class="summary-subtotal">
                      <td>Giá vận chuyển:</td>
                      <td>{{ displayPrice(totalPrice) + " đ" }}</td>
                    </tr>
                    <tr class="summary-total">
                      <td>Tổng tiền:</td>
                      <td>$160.00</td>
                    </tr>
                  </tbody>
                </table>
                <div class="flex justify-center">
                  <a
                    href="checkout.html"
                    class="btn btn-outline-primary-2 btn-order btn-block"
                    >PROCEED TO CHECKOUT</a
                  >
                </div>
              </div>
              <!-- End .summary -->

              <a
                href="category.html"
                class="btn btn-outline-dark-2 btn-block mb-3"
                ><span>CONTINUE SHOPPING</span><i class="icon-refresh"></i
              ></a>
            </aside>
            <!-- End .col-lg-3 -->
          </div>
          <!-- End .row -->
        </div>
        <!-- End .container -->
      </div>
      <!-- End .cart -->
    </div>
  </div>
</template>
<script setup lang="ts">
import { onBeforeMount, computed, ref, watch } from "vue";
import { useCartStore } from "@/stores/cart";
import { displayPrice } from "@/services/utils";
const cartStore = useCartStore();

const selectedProducts = ref<any>([]);
const selectedProductId = ref<any>([]);
const checkAll = ref(false);
const totalPrice = ref(0);

watch(
  () => selectedProductId.value,
  (newValue) => {
    // if (selectedProductId.value.length == carts.value.length) {
    //   checkAll.value = true;
    // }

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
    totalPrice.value = selectedProducts.value.reduce(
      (sum: number, item: any) => {
        return sum + item.totalPrice;
      },
      0
    );
  },
  { deep: true }
);
const carts = computed(() => cartStore.$state.carts.data);
onBeforeMount(async () => {
  await cartStore.getList();
});
</script>
<style lang="scss" scoped>
.table td {
  padding-top: 1.5rem;
  padding-bottom: 1.5rem;
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
  -webkit-user-select: none; /* Chống chọn text trên label */
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
  opacity: 0; /* Làm cho nó vô hình */
  cursor: pointer;
  height: 0;
  width: 0;
}
.custom-radio {
  display: inline-block; /* Để có thể đặt width/height */
  position: relative;
  height: 20px; /* Kích thước vòng ngoài */
  min-width: 20px; /* Kích thước vòng ngoài */
  background-color: #eee; /* Màu nền khi chưa chọn */
  border: 1px solid #ccc; /* Viền ngoài */
  border-radius: 50%; /* Tạo hình tròn */
  transition: background-color 0.2s ease, border-color 0.2s ease; /* Hiệu ứng chuyển đổi màu */
}

.radio-container input[type="radio"]:checked ~ label .custom-radio {
  background-color: #fff; /* Màu nền vòng ngoài khi chọn */
  border: 1px solid var(--bs-primary); /* Màu viền khi chọn */
}

/* Tạo dấu chấm tròn bên trong (sử dụng pseudo-element ::after) */
.custom-radio::after {
  content: "";
  position: absolute;
  display: none; /* Mặc định ẩn đi */
  /* Căn giữa dấu chấm */
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

/* Hiển thị và tạo kiểu cho dấu chấm khi radio được CHỌN */
.radio-container input[type="radio"]:checked ~ label .custom-radio::after {
  display: block;
  width: 10px; /* Kích thước dấu chấm */
  height: 10px; /* Kích thước dấu chấm */
  border-radius: 50%;
  background: var(--bs-primary); /* Màu của dấu chấm */
}
.radio-container:hover input[type="radio"]:not(:checked) ~ label .custom-radio {
  background-color: #ddd; /* Đổi màu nền nhẹ khi hover (nếu chưa check) */
}
.radio-container input[type="radio"]:focus + label .custom-radio {
  /* Dùng + label nếu label đứng NGAY SAU input */
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.3); /* Vòng sáng focus */
  outline: none; /* Bỏ outline mặc định nếu dùng box-shadow */
}
.radio-container input[type="radio"]:focus-visible + label .custom-radio {
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.3);
  outline: none;
}

.radio-container input[type="radio"]:disabled ~ label {
  color: #aaa; /* Làm mờ text */
  cursor: not-allowed; /* Đổi con trỏ */
}

.radio-container input[type="radio"]:disabled ~ label .custom-radio {
  background-color: #f0f0f0; /* Màu nền nhạt hơn */
  border-color: #ddd;
  cursor: not-allowed;
}

.radio-container input[type="radio"]:disabled ~ label .custom-radio::after {
  background-color: #ccc; /* Màu dấu chấm nhạt hơn khi disabled */
}
</style>
