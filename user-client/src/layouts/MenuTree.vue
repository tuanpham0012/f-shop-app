<template lang="">
  <li v-if="!menu.groupMenu">
    <a
      :href="
        menu.children.length == 0 && menu.url == ''
          ? '/404-not-found'
          : menu.url
      "
      :class="{
        'sf-with-ul': menu.children && menu.children.length > 0,
        'sf-with-ul-children': depth > 1,
      }"
    >
      <span class="d-flex gap-2"><div v-html="menu.icon"></div> {{ menu.title }} </span></a
    >
    <ul v-if="menu.children && menu.children.length > 0">
      <MenuTree
        v-for="(item, index) in menu.children"
        :key="index"
        :menu="item"
        :depth="depth + 1"
      />
    </ul>
  </li>
  <div class="row no-gutters" v-else>
    <div class="col-md-12">
      <li>
        <div
          class="px-[2.5rem] text-red-500 font-normal text-2xl mb-1 uppercase"
        >
          {{ menu.title }}
        </div>
        <MenuTree
          v-for="(item, index) in menu.children"
          :key="index"
          :menu="item"
          :depth="depth + 1"
        />
      </li>
    </div>
  </div>
  <!-- End .megamenu megamenu-md -->
</template>
<script setup lang="ts">
const props = defineProps({
  depth: Number,
  menu: {
    type: Object,
    default: () => null,
  },
});
</script>
<style lang="scss" scoped>
li {
  cursor: pointer;
  > a {
    color: #000000;
    text-decoration: none !important;
    &.sf-with-ul-children {
      font-size: 1.4rem ;
      font-weight: 450;
      > span {
        transition: transform 0.4s ease;
        &:hover {
          transform: translateX(8px);
        }
      }
    }
  }
}
ul {
  min-height: 300px !important;
}
</style>
