<template>
    <option :value="menu.id">{{geneChar(props.depth) }} {{ menu.title }} {{ menu.groupMenu ? '(Nhóm)' : '' }}</option>
    <TreeSelect v-for="(item, index) in menu.children" :key="index" :menu="item" :depth="props.depth + 1" />
</template>
<script setup lang="ts">
const props = defineProps({
    menu: {
        type: Object,
        default: () => null
    },
    depth: {
        type: Number,
        default: 0
    }
});

const geneChar = (depth: number) => {
    let char = '';
    for (let i = 0; i < depth; i++) {
        char += '—';
    }
    return char;
}
</script>
<style lang="">
    
</style>