<template>
    <div class="relative" v-click-outside="() => {setFocus(false); console.log('clicked outside');}">
        <input
            type="text"
            class="form-control"
            :placeholder="placeholder"
            v-model="searchQuery"
            @focus="setFocus(true)"
        />
        
        <div class="absolute top-full left-0 right-0 bg-white border border-gray-200 mt-1 rounded-md shadow-lg z-10" v-if="handleFocus">
            <div class="max-h-60 overflow-y-auto">
                <div
                    class="p-2 hover:bg-gray-100 cursor-pointer"
                    v-for="(item, index) in filteredItems"
                    :key="index"
                    @click="selectItem(item)"
                >
                    {{ item.name }}
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup> 
import { ref, computed, watch } from 'vue';
import debounce from "lodash.debounce";

const searchQuery = ref('');
const items = ref([]);

const handleFocus = ref(false);

const emits = defineEmits(['change-searchQuery', 'item-selected']);

const props = defineProps({
    placeholder: {
        type: String,
        default: 'Tìm kiếm...'
    },
    filteredItems: {
        type: Array,
        default: () => []
    }
});

const setFocus = (value) => {
    console.log('setFocus', value);
    handleFocus.value = value;
};
watch(() => searchQuery.value, debounce((newValue) => {
    emits('change-searchQuery', newValue);
}, 300));
</script>

<style>
</style>
