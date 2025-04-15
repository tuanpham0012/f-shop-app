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
      <div class="cart mt-1">
        <div class="grid grid-cols-24 gap-3 p-3 pt-0 bg-white">
          <div class="col-span-24 p-2">
            <div class="hidden md:grid grid-cols-24 gap-4 py-3 border-b border-gray-300 font-semibold text-gray-500">
              <div class="col-span-1"><input type="checkbox" class="form-check-input" v-model="checkAll" /></div>
              <div class="col-span-10 col-start-4 text-start">Sản phẩm</div>
              <div class="col-span-3 text-right">Đơn giá</div>
              <div class="col-span-4 text-center">Số lượng</div>
              <div class="col-span-3 text-right">Thành tiền</div>
              <div class="col-span-1 text-center"></div>
            </div>
            <div class="grid grid-cols-24 gap-1 md:gap-4 border-b py-4 border-gray-100" v-for="(item, index) in carts" :key="index">
              <div class="col-span-2 md:col-span-1 flex items-center"><input type="checkbox" class="form-check-input" :id="item.id" :value="item.id" v-model="selectedProductId" /></div>
              <div class="col-span-5 md:col-span-2 flex items-baseline justify-center">
                <figure class="w-[80px] max-w-[100%] h-[80px] max-h-[100%] aspect-square">
                  <img :src="item.sku.imagePath" alt="Product image" class="w-100 h-100 object-contain" />
                </figure>
              </div>
              <div class="col-span-16 md:col-span-20 grid grid-cols-16 md:grid-cols-20 gap-1 md:gap-4 ps-1">
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
                <div class="col-span-8 md:col-span-4 flex items-center justify-center">
                  <div class="cart-product-quantity">
                    <div class="flex border">
                      <div class="input-group-prepend bg-white z-10">
                        <button class="p-[.5rem] flex aspect-square btn-decrement text-sm" type="button" :disabled="isPending" @click="updateCart(item, 2)">
                          <i class="fa-solid fa-minus"></i>
                        </button>
                      </div>
                      <input type="text" style="text-align: center" class="w-100 max-w-[60px] py-[.3rem] text-sm" required="" placeholder="" v-model="item.quantity" @change="updateCart(item, 0)" />
                      <div class="input-group-append">
                        <button class="btn-increment p-[.5rem] flex aspect-square z-10 bg-white text-sm" type="button" :disabled="isPending" @click="updateCart(item, 1)">
                          <i class="fa-solid fa-plus"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-span-3 hidden md:flex items-center justify-end">{{ displayPrice(item.totalPrice) + " đ" }}</div>
              </div>
              <div class="col-span-1 flex justify-end relative">
                <button class="btn-remove absolute right-[-1.5rem] top-5" @click="cartStore.deleteCart(item.id)">
                  <i class="icon-close text-lg"></i>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <div id="stop-point"></div> -->
  <div class="checkout-summary my-2" ref="checkoutElm">
    <div class="container-lg">
      <div class="bg-white pb-1 shadow-[0_-15px_25px_-5px_rgba(0,0,0,0.1)]">
        <!-- Voucher Row -->
        <div class="flex justify-between px-3 py-1 md:py-3 voucher-section">
          <div class="left-content">
            <span class="icon-voucher" title="Shopee Voucher"></span>
            <span>Voucher</span>
          </div>
          <div class="right-content">
            <a href="#">Chọn hoặc nhập mã</a>
          </div>
        </div>

        <hr class="divider" />

        <!-- Shopee Xu Row -->
        <div class="hidden justify-between px-3 py-1 shopee-xu-section">
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
        <div class="flex justify-between px-3 py-1 md:py-3 gap-2 action-section">
          <div class="left-actions">
            <div class="flex items-center gap-0">
              <input type="checkbox" class="form-check-input" id="select-all-items" name="select-all-items" v-model="checkAll" />
              <label for="select-all-items" class="checkout-label text-sm">Chọn Tất Cả ({{ carts.length }})</label>
            </div>

            <a class="hidden md:block" @click="deleteMultiple()">Xóa sản phẩm đã chọn</a>
          </div>
          <div class="right-summary flex-wrap justify-end">
            <span class="total-label"
              >Tổng cộng <span class="hidden md:block">({{ selectedProductId.length }} Sản phẩm)</span><span class="ms-3 total-price">{{ displayPrice(totalPrice) + "đ" }}</span></span
            >
            <button class="btn buy-button" :class="{ disabled: selectedProductId.length < 1 }" @click="toggleCheckout()"><span class="">Thanh toán</span></button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="mb-5"></div>
</template>
<script setup lang="ts">
import { onBeforeMount, computed, ref, watch } from "vue";
import { useRouter } from "vue-router";
import { useCartStore } from "@/stores/cart";
import { displayPrice } from "@/services/utils";
const cartStore = useCartStore();
const router = useRouter();
const selectedProducts = ref<any>([]);
const selectedProductId = ref<any>([]);
const checkAll = ref(false);
const totalPrice = ref(0);

const checkoutElm = ref<any>(null);
const stopElm = ref<any>(0);
const stopPoint = ref<any>(null);

const isPending = computed(() => {
  return cartStore.$state.pending;
});

watch(
  () => selectedProductId.value,
  (newValue) => {
    totalPrice.value = 0;
    selectedProductId.value.forEach((value: any) => {
      const item: any = carts.value.find((x: any) => x.id == value);
      if (item) {
        totalPrice.value += item.totalPrice;
      }
    });
  },
  { deep: true }
);

watch(
  () => cartStore.$state.carts.data,
  (newValue) => {
    totalPrice.value = 0;
    selectedProductId.value.forEach((value: any) => {
      const item: any = newValue.find((x: any) => x.id == value);
      if (item) {
        totalPrice.value += item.totalPrice;
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

const carts = computed(() => cartStore.$state.carts.data);

const updateCart = async (item: any, type: any) => {
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

const toggleCheckout = () => {
  router.push({
    name: "checkout",
    params: {
      ids: selectedProductId.value,
    },
  });
};

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
.action-section {
  .total-price {
    color: var(--bs-primary); /* Màu cam/đỏ cho giá tiền */
    font-size: 0.875rem; /* Cỡ chữ lớn hơn */
    font-weight: 500;
  }
  .total-label {
    display: flex;
    gap: 0.1rem;
    font-size: 0.875rem; /* Cỡ chữ nhỏ hơn */
    color: #555; /* Màu xám cho label */
  }
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
