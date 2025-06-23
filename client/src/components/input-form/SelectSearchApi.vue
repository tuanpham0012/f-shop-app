<template>
    <div
        class="relative"
        v-click-outside="
            () => {
                setFocus(false);
            }
        "
    >
        <div class="relative" @click="setFocus(true)">
            <span type="text" class="form-control pe-5" :placeholder="placeholder" @focus="setFocus(true)" ref="searchInput"> {{ selectedItem ? selectedItem[displayKey] : placeholder }}</span>
            <div class="absolute right-4 top-1/4"><i class="fa-solid fa-chevron-down text-xs"></i></div>
            
        </div>
        <div class="absolute top-full left-0 right-0 bg-white border border-gray-200 mt-1 rounded-md shadow-lg z-10" v-if="han">
            <div class="p-2">
                <input type="text" class="form-control pe-5" :placeholder="placeholder" v-model="searchQuery" @focus="setFocus(true)" ref="searchInput" />
            </div>
            <div class="max-h-60 overflow-y-auto p-2" ref="scrollContainer" @scroll="handleScroll">
                <div class="px-1 py-2 hover:bg-gray-100 cursor-pointer" v-for="(item, index) in filteredItems" :key="index" @click="selectItem(item)">
                    {{ item[displayKey] }}
                </div>
                <div class="px-1 py-2 hover:bg-gray-100 cursor-pointer" v-if="filteredItems.length === 0"><i class="fa-solid fa-magnifying-glass"></i> Không tìm thấy kết quả</div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, computed, watch } from "vue";
import debounce from "lodash.debounce";

const searchQuery = ref("");
const handleFocus = ref(false);
const searchInput = ref<HTMLInputElement | null>(null);
const scrollContainer = ref<HTMLElement | null>(null);

const emits = defineEmits(["change-searchQuery", "item-selected", 'show-more']);

const props = defineProps({
    placeholder: {
        type: String,
        default: "Tìm kiếm...",
    },
    selectedItem: {
        type: Object as any,
        default: null,
    },
    filteredItems: {
        type: Array as any,
        default: () => [],
    },
    delay: {
        type: Number,
        default: 500,
    },
    loading: {
        type: Boolean,
        default: false,
    },
    displayKey: {
        type: String,
        default: "name",
    },
    valueKey: {
        type: String,
        default: "id",
    },
});

const handleScroll = () => {
    const element = scrollContainer.value;
    if (!element) return;

    const isAtBottom = element.scrollTop + element.clientHeight >= element.scrollHeight;

    if (isAtBottom && !props.loading) {
        console.log("Đã cuộn đến cuối! Bắt đầu tải thêm...");
        emits("show-more");
    }
};

const setFocus = (value) => {
    if (!value && searchInput.value === document.activeElement) {
        return;
    }
    handleFocus.value = value;
};

const selectItem = (item) => {
    //searchQuery.value = item.name;
    setFocus(false);
    emits("item-selected", item);
};
watch(
    () => searchQuery.value,
    debounce((newValue) => {
        setFocus(true);
        emits("change-searchQuery", newValue);
    }, props.delay)
);
</script>

<style></style>
