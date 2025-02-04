<template lang="">
  <li class="item-lead">
    <a href="#" :class="{'sf-with-ul' : entry.children.length > 0}">{{ entry.name }}</a>
    <ul v-if="entry.children.length > 0">
        <CategoryTree v-for="item in entry.children" :entry="item" class="" />
    </ul>
  </li>
</template>
<script lang="ts" setup>

const props = defineProps({
  entry: {
    type: [Object],
    required: false,
    default: null,
  },
});
</script>
<style lang=""></style>
