<template lang="">
  <div class="product flex flex-col cursor-pointer" v-if="item">
    <figure class="product-media bg-gray-100 flex aspect-square items-center px-2">
      <img :src="item.imageThumb" alt="Product image" class="product-image" />
      <div class="product-action">
        <a href="#" class="btn-product btn-cart"><span>Thêm vào giỏ hàng</span></a>
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
          <div class="ratings">
            <div class="ratings-val" style="width: 20%"></div>
          </div>
          <span class="ratings-text">( 2 Reviews )</span>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { useRouter } from "vue-router";
import { displayPrice } from "@/services/utils";

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
</script>
<style lang=""></style>
