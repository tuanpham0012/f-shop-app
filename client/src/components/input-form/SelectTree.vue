<template>
    <option :value="entry[value]">{{geneChar(props.depth) }} {{ entry[display] }}</option>
    <SelectTree v-for="(item, index) in entry.children" :key="index" :entry="item" :value="value" :display="display" :depth="props.depth + 1" />
</template>
<script setup lang="ts">
const props = defineProps({
    entry: {
        type: Object,
        default: () => null
    },
    depth: {
        type: Number,
        default: 0
    },
    value: {
        type: String,
        default: 'id'
    },
    display: {
        type: String,
        default: 'name'
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