<template lang="">
    <div class="description" v-html="description"></div>
</template>
<script setup lang="ts">
const props = defineProps({
    description: {
        type: String,
        default: ''
    }
})
</script>
<style lang="scss" scoped>
.description * {
    font-size: 131% !important;
}
</style>