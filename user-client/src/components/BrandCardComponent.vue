<template lang="">
  <a :class="{ 'cat-block': loadStatus, 'cat-block-error': !loadStatus }">
    <figure>
      <img
        :src="srcImage"
        @error="errorImg($event, index)"
        class="object-contain"
        :style="{ width: width, height: height }"
        loading="lazy"
        v-if="loadStatus"
      />
      <span class="lable-text" :class="`text-[${textLabel}]`" v-else>{{
        label
      }}</span>
    </figure>
  </a>
</template>
<script setup lang="ts">
import { ref } from "vue";

const props = defineProps({
  srcImage: {
    type: String,
    required: true,
  },
  label: {
    type: String,
    required: true,
  },
  width: {
    type: String,
    default: "80px",
  },
  height: {
    type: String,
    default: "40px",
  },
  textLabel: {
    type: String,
    default: "sm",
  },
});

const loadStatus = ref(true);

const errorImg = () => {
  loadStatus.value = false;
};
</script>
<style lang="scss" scoped>
figure {
  display: inline-flex;
  align-items: center;
  align-content: center;
  justify-content: center;
  height: 100%;
  width: 100%;
  padding: 0;
  position: relative;
  flex: 1;
  margin: 0;
  border: 1px solid #b3b3b3;
  border-radius: 0.175rem;
  span {
    color: #6e6e6e;
    font-weight: 500;
    transition: transform 0.35s ease;
    position: relative;
    display: -webkit-box; /* Sử dụng flexbox cho nhiều dòng */
    -webkit-box-orient: vertical; /* Định hướng chiều dọc */
    -webkit-line-clamp: 1; /* Giới hạn số dòng hiển thị */
    overflow: hidden; /* Ẩn phần văn bản vượt ra ngoài */
    line-height: 1.5rem; /* Chiều cao dòng */
    height: 1.5rem; /* Chiều cao tối đa tương ứng với số dòng */
    padding-bottom: 0.2rem;
    margin: 1rem 0 1rem;
    &::after {
      content: "";
      display: block;
      position: absolute;
      bottom: -0.32rem;
      left: 25%;
      width: 100%;
      margin-left: -45%;
      height: 0.2rem;
      border-radius: 50%;
      background-color: rgba(0, 0, 0, 0.3);
      transition: all 0.35s ease;
      filter: blur(3px);
      opacity: 0.4;
    }
  }
  img {
    margin: 0.2rem 0;
  }
  &:hover {
    background-color: var(--bs-primary);
    border: 0;
    outline: 0;
    span {
      color: var(--bs-text-white);
      transform: translateY(-1px);
      &::after {
        opacity: 1;
      }
    }
  }
}

.active {
  figure {
    border-color: var(--bs-primary);
    background-color: var(--bs-primary);
    span {
      color: var(--bs-text-white);
      &::after {
        opacity: 1;
      }
    }
  }
}
</style>
