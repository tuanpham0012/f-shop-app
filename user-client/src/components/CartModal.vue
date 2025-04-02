<template lang="">
  <div class="dropdown-menu dropdown-menu-right">
    <div class="dropdown-cart-products">
      <div class="product" v-for="(item, index) in carts" :key="index">
        <div class="product-cart-details">
          <h4 class="product-title text-base">
            <a href="product.html">{{ item.product?.name }}</a>
          </h4>

          <span class="cart-product-info">
            <span class="cart-product-qty">{{ item.quantity }}</span>
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
    <!-- End .cart-product -->

    <div class="dropdown-cart-total">
      <span>Tổng tiền</span>

      <span class="cart-total-price">{{ displayPrice(totalPrice) }}đ</span>
    </div>
    <!-- End .dropdown-cart-total -->

    <div class="dropdown-cart-action">
      <a href="cart.html" class="btn btn-outline-primary">Xem giỏ hàng</a>
      <a href="checkout.html" class="btn btn-outline-primary-2"
        ><span>Thanh toán</span><i class="icon-long-arrow-right"></i
      ></a>
    </div>
    <!-- End .dropdown-cart-total -->
  </div>
</template>
<script setup lang="ts">
import { watch, ref } from "vue";
import { displayPrice } from "@/services/utils";

const totalPrice = ref<number>(0);

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
<style lang=""></style>
