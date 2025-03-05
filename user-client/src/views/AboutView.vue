<script setup lang="ts">
import { computed, onBeforeMount, onMounted, ref } from "vue";
import { useBrandStore } from "@/stores/brand";
import { viewFile } from "@/helpers/helpers";

const brandStore = useBrandStore();

const carouselOptions = ref({
  nav: true,
  dots: false,
  margin: 10,
  loop: false,
  responsive: {
    "0": {
      items: 2,
    },
    "420": {
      items: 3,
    },
    "600": {
      items: 4,
    },
    "900": {
      items: 5,
    },
    "1024": {
      items: 6,
    },
  },
});

const brands = computed(() => brandStore.$state.brands.data);

onBeforeMount(async () => {
  await brandStore.getList();
});
</script>

<template>
  <div class="container my-3">
    <div class="heading mb-3">
      <h5 class="text-center text-[2.6rem] font-medium">Thương hiệu</h5>
    </div>
    <carousel>
      <a href="#" class="brand">
                            <img src="../assets/images/brands/1.png" alt="Brand Name">
                        </a>

                        <a href="#" class="brand">
                            <img src="../assets/images/brands/2.png" alt="Brand Name">
                        </a>

                        <a href="#" class="brand">
                            <img src="../assets/images/brands/3.png" alt="Brand Name">
                        </a>

                        <a href="#" class="brand">
                            <img src="../assets/images/brands/4.png" alt="Brand Name">
                        </a>

                        <a href="#" class="brand">
                            <img src="../assets/images/brands/5.png" alt="Brand Name">
                        </a>

                        <a href="#" class="brand">
                            <img src="../assets/images/brands/6.png" alt="Brand Name">
                        </a>

                        <a href="#" class="brand">
                            <img src="../assets/images/brands/7.png" alt="Brand Name">
                        </a>
    </carousel>
    <!-- End .owl-carousel -->
  </div>
</template>
