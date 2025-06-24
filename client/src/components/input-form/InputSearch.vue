<template>
    <div class="relative" v-click-outside="setFocus(false)">
        <div class="relative">
            <input type="text" class="form-control pe-5" :placeholder="placeholder" v-model="searchQuery" @focus="setFocus(true)" ref="searchInput" />
            <div class="absolute right-4 top-[20%]" v-if="loading">
                <span class="loader"></span>
            </div>
        </div>

        <div class="absolute top-full left-0 right-0 bg-white border border-gray-200 mt-1 rounded-md shadow-lg z-10" v-if="handleFocus">
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
import { ref, onBeforeMount, watch } from "vue";
import { _getList } from "@/helpers/axiosConfig";
import debounce from "lodash.debounce";

const searchQuery = ref("");
const handleFocus = ref(false);
const searchInput = ref<HTMLInputElement | null>(null);
const scrollContainer = ref<HTMLElement | null>(null);
const filteredItems = ref<any[]>([]);
const loading = ref(false);
const page = ref(1);
const selectedItem = ref<any>(null);

const emits = defineEmits(["change-searchQuery", "item-selected", "show-more"]);

const props = defineProps({
    placeholder: {
        type: String,
        default: "Tìm kiếm...",
    },
    delay: {
        type: Number,
        default: 500,
    },
    url: {
        type: String,
        default: "",
    },
    displayKey: {
        type: String,
        default: "name",
    },
    valueKey: {
        type: String,
        default: "id",
    },
    pageSize: {
        type: Number,
        default: 10,
    },
});

const handleScroll = () => {
    const element = scrollContainer.value;
    if (!element) return;

    const isAtBottom = element.scrollTop + element.clientHeight >= element.scrollHeight;

    if (isAtBottom && !loading.value) {
        console.log("Đã cuộn đến cuối! Bắt đầu tải thêm...");
        page.value++;
        getListSearch({
            search: searchQuery.value,
            page: page.value,
            pageSize: props.pageSize,
        });
    }
};

const setFocus = (value: any) => {
    if (!value && searchInput.value === document.activeElement) {
        return;
    }
    handleFocus.value = value;
};

const selectItem = (item: any) => {
    //searchQuery.value = item.name;
    setFocus(false);
    selectedItem.value = item;
    emits("item-selected", item);
};

const getListSearch = async (query: any) => {
    loading.value = true;
    _getList(props.url, query)
        .then((res) => {
            filteredItems.value.push(...res.data.data);
        })
        .catch((err) => {
            console.log(err);
        }).finally(() => {
            loading.value = false;
        });
};

watch(
    () => searchQuery.value,
    debounce((newValue: any, oldValue: any) => {
        setFocus(true);
        filteredItems.value = [];
        page.value = 1;
        getListSearch({
            search: newValue,
            page: page.value,
            pageSize: props.pageSize,
        });
    }, props.delay)
);
onBeforeMount(() => {
    getListSearch({
        search: searchQuery.value,
        page: page.value,
        pageSize: props.pageSize,
    });
});
</script>

<style scoped>
.loader {
    width: 25px;
    height: 25px;
    border: 2px solid #000;
    border-bottom-color: #ff3d00;
    border-radius: 50%;
    display: inline-block;
    box-sizing: border-box;
    animation: rotation 1s linear infinite;
}

@keyframes rotation {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
</style>
