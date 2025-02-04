<template>
    <div v-if="props.errors.length > 0">
        <div class="invalid-feedback ms-2" v-for="(item, index) in props.errors" :key="index">
            {{ item }}
        </div>
    </div>
</template>
<script setup>

const props = defineProps({
    errors: {
        type: Array,
        default: []
    }
})
</script>
<style lang="scss" scoped>
.invalid-feedback{
    display: block;
}
</style>
