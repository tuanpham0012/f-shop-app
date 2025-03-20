<template lang="">
  <Swiper
    :slides-per-view="slidesPerView"
    :space-between="spaceBetween"
    :navigation="(itemCount > slidesPerView) && navigation"
    :modules="modules"
    :class="ids"
    class="px-7"
    :style="{ '--slide-item': itemCount > slidesPerView ? 'flex-start' : 'center' }"
    :autoplay="autoPlay ? {
      delay: delay,
      disableOnInteraction: false,
    } : false"
    :loop="autoPlay"
  >
    <slot></slot>
  </Swiper>
</template>
<script lang="ts" setup>
import { ref, defineProps } from "vue";
import { Swiper } from "swiper/vue";
import "swiper/swiper-bundle.css";
import "swiper/css";
import "swiper/scss/navigation";
import { Navigation, Autoplay } from "swiper/modules";

const modules = ref([Navigation, Autoplay]);

const props = defineProps({
  itemCount: {
    type: Number,
    default: 1,
  },
  slidesPerView: {
    type: [Number, String],
    default: 1,
  },
  spaceBetween: {
    type: [Number, String],
    default: 50,
  },
  ids: {
    type: String,
    default: "swiper",
  },
  navigation: {
    type: Boolean,
    default: true
  },
  autoPlay:{
    type: Boolean,
    default: false
  },
  delay: {
    type: Number,
    default: 2000
  }
});



</script>
<style lang="scss">

.swiper-custom-navigation {
  position: absolute;
  top: calc(50%);
  width: 100%;
  display: flex;
  justify-content: space-between;
  transform: translateY(-50%);
  z-index: 1; /* Ensure buttons are above the slides */
}

.swiper-button-prev{
  left: var(--swiper-navigation-sides-offset, 0px);
}

.swiper-button-next{
  right: var(--swiper-navigation-sides-offset, 0px);
}

.swiper-button-prev,
.swiper-button-next {
  background: none;
  width: 38px;
  border-radius: 100%;
  height: 38px;
  border: none;
  cursor: pointer;
  padding: 10px;
  color: #333; /* Default color */
  font-size: 24px;
  transition: color 0.3s ease;
  outline: none;
}

/* Change color on hover */
.swiper-button-prev:hover,
.swiper-button-next:hover {
    background: #ebedf0c0;
  color: #007bff;
}

.swiper-button-prev:after,
.swiper-button-next:after {
  font-family: "swiper-icons";
  font-size: 1.8rem;
  font-weight: 400;
}

.swiper-wrapper {
  align-items: center;
  justify-content: var(--slide-item);
}
.swiper-slide{
  height: 100%;
}
</style>
