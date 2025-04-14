<template lang="">
  <div class="dropdown-menu dropdown-menu-right">
    <div class="dropdown-cart-products mb-1">
      <div class="flex items-center pe-7 py-2 relative" v-for="(item, index) in carts.slice(0, limit)" :key="index">
        <div class="product-cart-details">
          <p class="text-sm checkout-label text-gray-700">{{ item.product?.name }}</p>

          <span class="cart-product-info text-sm">
            <span class="cart-product-qty ms-1">{{ item.quantity }}</span>
            x {{ displayPrice(item.sku?.price) }}đ
          </span>
        </div>
        <!-- End .product-cart-details -->

        <figure class="product-image-container me-2">
          <a href="product.html" class="product-image">
            <img
              :src="item.product?.imageThumb"
              alt="product"
              class="object-contain"
            />
          </a>
        </figure>
        <a href="#" class="btn-remove" title="Remove Product"
          ><i class="icon-close"></i
        ></a>
      </div>
    </div>

    <!-- <div class="dropdown-cart-total">
      <span>Tổng tiền</span>

      <span class="cart-total-price">{{ displayPrice(totalPrice) }}đ</span>
    </div> -->

    <div class="dropdown-cart-action">
      <!-- <a href="cart.html" class="btn btn-outline-primary">Xem giỏ hàng</a> -->
      <span class="text-sm">{{ carts.length - limit}} thêm vào giỏ</span>
      <router-link :to="{name:'Cart'}" class="btn btn-outline-primary-2"
        ><span>Thanh toán</span><i class="icon-long-arrow-right"></i
      ></router-link>
    </div>
  </div>
</template>
<script setup lang="ts">
import { watch, ref } from "vue";
import { displayPrice } from "@/services/utils";

const totalPrice = ref<number>(0);

const limit = ref(5);

const props = defineProps({
  carts: {
    type: Array,
    default: [],
  },
});
watch(
  () => props.carts,
  (newValue) => {
    totalPrice.value = newValue.reduce((sum: number, item: any) => {
      // Trả về tổng mới cho lần lặp tiếp theo
      return sum + item.totalPrice;
    }, 0);
  },
  { deep: true }
);
</script>
<style lang="scss" scoped>
.checkout-label {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  display: block; /* Hoặc inline-block */
}
</style>
