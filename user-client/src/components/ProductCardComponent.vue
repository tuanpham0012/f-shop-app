<template lang="">
  <div class="product flex flex-col cursor-pointer" v-if="item">
    <figure
      class="product-media bg-gray-50 flex aspect-square items-center"
    >
      <img :src="item.imageThumb" alt="Product image" class="product-image w-100 h-100 object-contain" />
      <!-- <div class="product-action">
        <a href="#" class="btn-product btn-cart" @click="addToCart(1)"
          ><span>Thêm vào giỏ hàng</span></a
        >
      </div> -->
    </figure>

    <div class="product-body flex flex-col justify-between md:px-3">
      <div>
        <p class="product-title text-sm">
          {{ item.name }}
        </p>
        <p class="product-cat text-xs py-1">
          {{ item.category?.name }}
        </p>
      </div>
      <div>
        <div class="flex justify-between items-center">
          <span class="text-base text-red-500">{{ displayPrice(item.price) }} ᵭ</span>
          <span class="text-xs"> đã bán 1111</span>
        </div>
        <!-- ratings-container -->
        <div class="hidden ">
          <span class="ratings-text text-[0.8rem]"
            ><i class="fa fa-star text-yellow-300"></i> 2</span
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
