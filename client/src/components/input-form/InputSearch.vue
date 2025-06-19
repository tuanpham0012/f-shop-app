<template>
    <div
        class="relative"
        v-click-outside="
            () => {
                setFocus(false);
            }
        "
    >
        <div class="relative">
            <input type="text" class="form-control pe-5" :placeholder="placeholder" v-model="searchQuery" @focus="setFocus(true)" ref="searchInput" />
            <div class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none" role="status" v-if="loading">
                <svg aria-hidden="true" class="w-5 h-5 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                        fill="currentColor"
                    />
                    <path
                        d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                        fill="currentFill"
                    />
                </svg>
                <span class="sr-only">Loading...</span>
            </div>
        </div>

        <div class="absolute top-full left-0 right-0 bg-white border border-gray-200 mt-1 rounded-md shadow-lg z-10" v-if="handleFocus && searchQuery.length > 0">
            <div class="max-h-60 overflow-y-auto" ref="scrollContainer" @scroll="handleScroll">
                <div class="p-2 hover:bg-gray-100 cursor-pointer" v-for="(item, index) in filteredItems" :key="index" @click="selectItem(item)">
                    {{ item.name }}
                </div>
                <div class="p-2 hover:bg-gray-100 cursor-pointer" v-if="filteredItems.length === 0"><i class="fa-solid fa-magnifying-glass"></i> Không tìm thấy kết quả</div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, computed, watch } from "vue";
import debounce from "lodash.debounce";

const searchQuery = ref("");
const items = ref([]);
const handleFocus = ref(false);
const searchInput= ref(null);
const scrollContainer = ref(null);

const emits = defineEmits(["change-searchQuery", "item-selected"]);

const props = defineProps({
    placeholder: {
        type: String,
        default: "Tìm kiếm...",
    },
    filteredItems: {
        type: Array,
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
});

const handleScroll = () => {
  const element = scrollContainer.value;
  if (!element) return;

  const isAtBottom = element.scrollTop + element.clientHeight >= element.scrollHeight;

  if (isAtBottom && !props.loading) {
    console.log('Đã cuộn đến cuối! Bắt đầu tải thêm...');
  }
};

const setFocus = (value) => {
    if(!value && searchInput.value === document.activeElement) {
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
        setFocus(true)
        emits("change-searchQuery", newValue);
    }, props.delay)
);
</script>

<style></style>
