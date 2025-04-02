<template lang="">
  <div class="product flex flex-col cursor-pointer" v-if="item">
    <figure
      class="product-media bg-gray-100 flex aspect-square items-center px-1"
    >
      <img :src="item.imageThumb" alt="Product image" class="product-image" />
      <div class="product-action">
        <a href="#" class="btn-product btn-cart" @click="addToCart(1)"
          ><span>Thêm vào giỏ hàng</span></a
        >
      </div>
    </figure>

    <div class="product-body flex flex-col justify-between">
      <div>
        <p class="product-title text-base">
          {{ item.name }}
        </p>
        <p class="product-cat text-sm">
          {{ item.category?.name }}
        </p>
      </div>
      <div>
        <div class="text-base text-red-500 my-1">
          {{ displayPrice(item.price) }}đ
        </div>
        <div class="ratings-container">
          <span class="ratings-text"
            ><i class="fa fa-star text-sm text-yellow-300"></i> 2</span
          >
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { useRouter } from "vue-router";
import { displayPrice } from "@/services/utils";
import { useCartStore } from "@/stores/cart";

const cartStore = useCartStore();

const props = defineProps({
  item: {
    type: Object,
    default: () => null,
  },
  hideCategory: {
    type: Boolean,
    default: true,
  },
});

const addToCart = async (skuId: number) => {
  await cartStore.addToCart({
    skuId: skuId,
    quantity: 1,
  });
};
</script>
<style lang=""></style>
