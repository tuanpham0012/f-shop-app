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
      <div class="cart grid grid-cols-12 gap-3">
        <div class="col-span-12 bg-white my-2 p-3 rounded-lg">
          <p class="text-base text-gray-700 font-medium mb-1"><i class="fa-solid fa-location-dot"></i> Địa chỉ nhận hàng</p>
          <div class="flex gap-1 flex-wrap ps-4">
            <span class="text-sm font-semibold">{{ `${info?.name} (${info?.phone})` }}</span>
            <span class="checkout-label text-sm">{{ info?.address }} <span class="badge text-bg-primary">Mặc định</span></span>
          </div>
        </div>
        <div class="col-span-12 grid grid-cols-24 gap-3 p-2 pt-0 bg-white rounded-lg">
          <div class="col-span-24 p-2">
            <div class="hidden md:grid grid-cols-24 gap-4 py-3 border-b border-gray-300 font-semibold text-gray-500">
              <div class="col-span-9 col-start-4 text-start">Sản phẩm</div>
              <div class="col-span-3 text-right">Đơn giá</div>
              <div class="col-span-4 text-center">Số lượng</div>
              <div class="col-span-3 text-right">Thành tiền</div>
            </div>
            <div class="grid grid-cols-24 gap-1 md:gap-4 border-b py-2 border-gray-100" v-for="(item, index) in carts" :key="index">
              <div class="col-span-5 md:col-span-2 flex items-start md:items-center justify-center">
                <figure class="max-w-[80px] max-h-[80px] aspect-square">
                  <img :src="item.sku.imagePath" alt="Product image" class="w-100 h-100 object-contain" />
                </figure>
              </div>
              <div class="col-span-16 md:col-span-20 grid grid-cols-16 md:grid-cols-20 gap-0 md:gap-2 ps-1">
                <div class="col-span-16 md:col-span-10 flex items-center">
                  <div class="grid grid-cols-10 gap-2 pt-[4px] w-100">
                    <a class="col-span-16 md:col-span-6 text-neutral-600 text-sm md:text-base hover:text-[var(--bs-primary)] checkout-label" href="#">{{ item.product.name }}</a>
                    <div class="col-span-16 md:col-span-4 text-xs md:text-sm text-gray-400 flex md:inline-block" v-if="item.sku.variants.length > 0">
                      <span>Phân Loại Hàng:</span>
                      <div class="variant">
                        <span v-for="(variant, index) in item.sku.variants" :key="index"> {{ variant.optionValue.label }}</span>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-span-8 md:col-span-3 flex items-center justify-start md:justify-end py-2 text-xs md:text-sm">{{ displayPrice(item.sku.price) + " đ" }}</div>
                <div class="col-span-8 md:col-span-4 flex items-center justify-end md:justify-center md:pe-6">
                  <div class="cart-product-quantity text-sm">
                    x{{ item.quantity }}
                  </div>
                </div>
                <div class="col-span-3 hidden md:flex items-center justify-end">{{ displayPrice(item.totalPrice) + " đ" }}</div>
              </div>
            </div>
          </div>
        </div>

        <div class="col-span-12 md:col-span-6 bg-white p-3 rounded-lg">
          <p class="text-lg text-gray-700 font-medium mb-2">Đơn vị vận chuyển</p>
          <!-- <select-search :listData="shippingUnits" display="name" keyValue="id" v-model="shippingUnitId" :firstSelected="true"></select-search> -->
          <div class="radio-container ps-4" v-for="(item, index) in shippingUnits" :key="index">
            <input type="radio" :id="`shippingUnits-${item.id}`" name="shippingUnits" :value="item.id" v-model="shippingUnitId" />
            <label class="flex gap-2 items-center" :for="`shippingUnits-${item.id}`">
              <span class="custom-radio"></span>
              <span class="w-100 text-sm">{{ item.name }}</span>
            </label>
          </div>
        </div>
        <div class="col-span-12 md:col-span-6 bg-white p-3 rounded-lg">
          <p class="text-lg text-gray-700 font-medium mb-2">Phương thức thanh toán</p>
          <div class="radio-container ps-4" v-for="(item, index) in paymentMethods" :key="index">
            <input type="radio" :id="`paymentMethods-${item.id}`" name="paymentMethod" :value="item.id" v-model="paymentMethodId" />
            <label class="flex gap-2 items-center" :for="`paymentMethods-${item.id}`">
              <span class="custom-radio"></span>
              <span class="w-100 text-sm">{{ item.name }}</span>
            </label>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="checkout-summary my-2" ref="checkoutElm">
    <div class="container-lg">
      <div class="bg-white pb-1 shadow-[0_-15px_25px_-5px_rgba(0,0,0,0.1)] rounded-lg">
        <div class="gap-2 grid grid-cols-12">
          <div class="right-summary col-span-12 md:col-span-6 md:col-start-7">
            <div class="grid grid-cols-5 gap-0 md:gap-2 px-4 py-2">
              <p class="text-lg col-span-5 py-2 text-gray-600 font-semibold m-0 flex md:hidden">Chi tiết thanh toán</p>
              <div class="total-label col-span-3 flex items-center gap-1">
                <span>Tổng tiền hàng:</span>
                <span class="text-xs text-gray-400">({{ carts.length }} sản phẩm)</span>
              </div>
              <div class="text-right text-sm col-span-2">{{ displayPrice(totalPrice) + " đ" }}</div>
              <div class="total-label col-span-3">
                <span>Phí vận chuyển:</span>
              </div>
              <div class="text-right text-sm col-span-2">{{ displayPrice(shippingFee) + " đ" }}</div>
              <div class="total-label col-span-3">
                <span>Giảm giá:</span>
              </div>
              <div class="text-right text-sm col-span-2">{{ displayPrice(discount) + " đ" }}</div>
              <div class="total-label col-span-3">
                <span>Tổng thanh toán:</span>
              </div>
              <div class="total-price text-right font-semibold col-span-2">{{ displayPrice(totalPrice) + " đ" }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="mb-5"></div>
</template>
<script setup lang="ts">
import { onBeforeMount, computed, ref, watch } from "vue";
import { useRoute } from "vue-router";
import { useCartStore } from "@/stores/cart";
import { useCommonStore } from "@/stores/common";
import { useAuthStore } from "@/stores/auth";
import { displayPrice } from "@/services/utils";
const cartStore = useCartStore();
const commonStore = useCommonStore();
const authStore = useAuthStore();
const route = useRoute();
const totalPrice = ref(0);

const shippingUnitId = ref(1);
const shippingFee = ref(0);
const discount = ref(0);
const paymentMethodId = ref(1);

const checkoutElm = ref<any>(null);
const stopElm = ref<any>(0);
const stopPoint = ref<any>(null);

const info = computed(() => authStore.$state.info);

const carts = ref<any>([]);

watch(
  () => cartStore.$state.carts,
  () => {
    const ids = route.params.ids as Array<string>;
    let result: any[] = [];
    totalPrice.value = 0;
    ids.forEach((element) => {
      const item = cartStore.$state.carts.data.find((x: any) => x.id == element);
      if (item) {
        result.push(item);
      }
    });
    result.forEach((item: any) => {
      totalPrice.value += item.totalPrice;
    });
    carts.value = result;
  }
);

const paymentMethods = computed(() => commonStore.$state.paymentMethods.data);
const shippingUnits = computed(() => commonStore.$state.shippingUnits.data);

watch(
  () => carts.value,
  () => {
    setTimeout(() => checkoutScroll(), 500);
  }
);

const checkoutScroll = () => {
  function handleScroll() {
    if (checkoutElm.value == null || stopPoint.value == null) return;

    stopElm.value = stopPoint.value.offsetHeight + stopPoint.value.offsetTop + 130;

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
  position: relative;
  height: 1rem;
  min-width: 1rem;
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
  gap: 1rem; /* Khoảng cách các mục bên trái */
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
  gap: 0.5rem; /* Khoảng cách các mục bên phải */
}
.total-price {
    color: var(--bs-primary); /* Màu cam/đỏ cho giá tiền */
    font-size: 1rem; /* Cỡ chữ lớn hơn */
    font-weight: 500;
  }
  .total-label {
    display: flex;
    gap: 0.1rem;
    font-size: 0.875rem; /* Cỡ chữ nhỏ hơn */
    color: #555; /* Màu xám cho label */
  }
.action-section .buy-button {
  border: 1px solid var(--bs-primary); /* Màu cam Shopee */
  color: var(--bs-primary);
  padding: 0.5rem;
  border-radius: 0.2rem;
  font-size: 0.875rem;
  min-width: 6rem;
  cursor: pointer;
  font-weight: 500;
  transition: transform 0.2s ease; /* Hiệu ứng hover */
}
.action-section .buy-button:hover {
  background-color: var(--bs-primary);
  color: var(--bs-white);
}

/* Căn chỉnh checkbox chuẩn hơn */
input[type="checkbox"] {
  vertical-align: middle;
  margin: 0; /* Reset margin */
}

.checkout-label {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  display: block; /* Hoặc inline-block */
}
</style>
